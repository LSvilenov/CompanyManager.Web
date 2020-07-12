using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CompanyManager.Common.Constants;
using CompanyManager.Data;
using CompanyManager.Data.Models;
using CompanyManager.Services.Contracts;
using CompanyManager.Services.Models;

using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext dbContext;

        public CompanyService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CompanyListingServiceModel>> GetAllAsync(
            string name,
            int page = 1,
            int pageSize = ServicesConstants.DefaultPageSize)
        {
            var query = dbContext.Companies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name) && name.Length >= ServicesConstants.MinimalSearchLength)
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            var companies = await query
                .OrderByDescending(c => c.CreationDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CompanyListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreationDate = c.CreationDate.Date,
                    OfficesCount = c.Offices.Count
                })
                .ToListAsync();

            return companies;
        }

        public async Task<CompanyDetailsServiceModel> GetByIdAsync(int companyId)
        {
            var company = await dbContext
                .Companies
                .Where(c => c.Id == companyId)
                .Select(c => new CompanyDetailsServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreationDate = c.CreationDate,
                    //Offices = c.Offices.Select(o => new OfficeListingServiceModel
                    //{
                    //    Id = o.Id,
                    //    City = o.City.Name,
                    //    Country = o.City.Country.Name,
                    //    Address = $"{o.Address.Street} {o.Address.StreetNumber}",
                    //    EmployeesCount = o.OfficeEmployees.Count
                    //})
                })
                .FirstOrDefaultAsync();

            return company;
        }

        public async Task<int> AddAsync(string name, DateTime creationDate)
        {
            // I am not sure if the creation date is the creation of the db entry or the establishment of the company.
            Company company = new Company
            {
                Name = name,
                //CreationDate = DateTime.UtcNow,
                CreationDate = creationDate.Date
            };

            dbContext.Companies.Add(company);

            await dbContext.SaveChangesAsync();

            return company.Id;
        }

        public async Task EditAsync(int id, string name, DateTime creationDate)
        {
            var company = await dbContext
                .Companies
                .FindAsync(id);

            company.Name = name;
            company.CreationDate = creationDate;

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Company company = await dbContext
                .Companies
                .FindAsync(id);

            dbContext.Companies.Remove(company);

            await dbContext.SaveChangesAsync();
        }

        public async Task<int> GetTotalAsync(string name)
        {
            var query = dbContext.Companies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name) && name.Length >= ServicesConstants.MinimalSearchLength)
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            return await query.CountAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await dbContext.Companies.AnyAsync(c => c.Id == id);
    }
}
