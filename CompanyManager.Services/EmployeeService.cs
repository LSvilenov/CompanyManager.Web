using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using CompanyManager.Common.Constants;
using CompanyManager.Data;
using CompanyManager.Data.Models;
using CompanyManager.Services.Contracts;
using CompanyManager.Services.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;

namespace CompanyManager.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IFileService fileService;

        public EmployeeService(ApplicationDbContext dbContext, IFileService fileService)
        {
            this.dbContext = dbContext;
            this.fileService = fileService;
        }

        public async Task<IEnumerable<EmployeeListingServiceModel>> GetAllAsync(
            string search,
            int page = 1,
            int pageSize = ServicesConstants.DefaultPageSize)
        {
            var query =
                dbContext.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search) && search.Length >= ServicesConstants.MinimalSearchLength)
            {
                query = query.Where(e => e.FirstName.Contains(search)
                || e.LastName.Contains(search));
            }

            query = query
                .OrderByDescending(e => e.StartingDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            IEnumerable<EmployeeListingServiceModel> employees =
                await GetEmployessAsync(query);

            return employees;
        }

        public async Task<IEnumerable<EmployeeListingServiceModel>> SearchAsync(SearchCriteria searchCriteria)
        {
            var query = dbContext.Employees.AsQueryable();

            if (searchCriteria.OfficeID > 0)
            {
                query = query
                    .Where(e => e.OfficeEmployees.Any(oe => oe.OfficeId == searchCriteria.OfficeID));
            }

            IEnumerable<EmployeeListingServiceModel> employees =
                await GetEmployessAsync(query);

            return employees;
        }

        public async Task<EmployeeDetailsServiceModel> GetByIdAsync(int id)
        {
            EmployeeDetailsServiceModel employee = await dbContext
                .Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDetailsServiceModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Salary = e.Salary,
                    ExperienceLevel = e.ExperienceLevel.ToString(),
                    StartingDate = e.StartingDate,
                    VacationDays = e.VacationDays,
                    Image = e.Image
                })
                .FirstOrDefaultAsync();

            return employee;
        }

        public async Task DeleteAsync(int id)
        {
            Employee employee = await dbContext
                .Employees
                .FindAsync(id);

            dbContext.Employees.Remove(employee);

            await dbContext.SaveChangesAsync();

        }

        public async Task<int> AddAsync(EmployeeCreateServiceModel employeeDto)
        {
            //string imagePath = await fileService.SaveFileToFolderAsync(employeeDto.Image, "Resources", "Images");

            Employee employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Salary = employeeDto.Salary,
                StartingDate = employeeDto.StartingDate,
                VacationDays = employeeDto.VacationDays,
                ExperienceLevel = employeeDto.ExperienceLevel,
                //Image = imagePath,
            };

            foreach (var officeId in employeeDto.OfficeIds)
            {
                employee.OfficeEmployees.Add(new OfficeEmployee { OfficeId = officeId });
            }

          

            dbContext.Employees.Add(employee);

            await dbContext.SaveChangesAsync();

            return employee.Id;
        }

        public async Task EditAsync(EmployeeCreateServiceModel employeeDto)
        {
            Employee employee = await dbContext
                .Employees
                .FindAsync(employeeDto.Id);

            if (employee == null)
            {
                return;
            }

            string imagePath = await fileService.SaveFileToFolderAsync(employeeDto.Image, "Resources", "Images");

            employee.FirstName = employeeDto.FirstName;
            employeeDto.LastName = employeeDto.LastName;
            employee.Salary = employeeDto.Salary;
            employee.ExperienceLevel = employeeDto.ExperienceLevel;
            employee.StartingDate = employeeDto.StartingDate;
            employee.VacationDays = employeeDto.VacationDays;
            employee.Image = imagePath;

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await dbContext.Employees.AnyAsync(c => c.Id == id);

        public async Task<int> GetTotalAsync(string search)
        {
            var query = dbContext.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search) && search.Length >= ServicesConstants.MinimalSearchLength)
            {
                query = query.Where(e => e.FirstName.Contains(search)
                || e.LastName.Contains(search));
            }

            return await query.CountAsync();
        }

        private async Task<IEnumerable<EmployeeListingServiceModel>> GetEmployessAsync(IQueryable<Employee> query)
        {
            var employees = await query
                .Select(e => new EmployeeListingServiceModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    StartingDate = e.StartingDate.Date,
                    ExperienceLevel = e.ExperienceLevel.ToString()
                })
                .ToListAsync();

            return employees;
        }
    }
}
