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
    public class OfficeService : IOfficeService
    {
        private readonly ApplicationDbContext dbContext;

        public OfficeService(ApplicationDbContext dbContext)
        {
            //TODO: Extract base class for all services.
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<OfficeListingServiceModel>> GetAllAsync(
            string search,
            int page = 1,
            int pageSize = ServicesConstants.DefaultPageSize)
        {
            var query =
                dbContext.Offices.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search) && search.Length >= ServicesConstants.MinimalSearchLength)
            {
                query = query.Where(o => o.City.Name.Contains(search)
                || o.City.Country.Name.Contains(search)
                || o.Address.Street.Contains(search));
            }

            query = query
                .OrderBy(o => o.CompanyId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var offices = await GetOfficesAsync(query);

            return offices;
        }

        public async Task<IEnumerable<OfficeListingServiceModel>> SearchAsync(SearchCriteria searchCriteria)
        {
            var query = dbContext.Offices.AsQueryable();

            if (searchCriteria.CityId > 0)
            {
                query = query.Where(o => o.CityId == searchCriteria.CityId);
            }

            if (searchCriteria.CompanyId > 0)
            {
                query = query.Where(o => o.CompanyId == searchCriteria.CompanyId);
            }

            if (searchCriteria.CountryId > 0)
            {
                query = query.Where(o => o.City.CountryId == searchCriteria.CountryId);
            }

            if (searchCriteria.EmployeeId > 0)
            {
                query = query.Where(o => o.OfficeEmployees.Any(x => x.EmployeeId == searchCriteria.EmployeeId));
            }

            IEnumerable<OfficeListingServiceModel> offices =
                await GetOfficesAsync(query);

            return offices;
        }

        public async Task<OfficeDetailsServiceModel> GetByIdAsync(int id)
        {
            OfficeDetailsServiceModel office = await dbContext
                .Offices
                .Where(o => o.Id == id)
                .Select(o => new OfficeDetailsServiceModel
                {
                    Id = o.Id,
                    Street = o.Address.Street,
                    StreetNumber = o.Address.StreetNumber,
                    CityId = o.City.Id,
                    City = o.City.Name,
                    Country = o.City.Country.Name,
                    CompanyId = o.CompanyId,
                    CompanyName = o.Company.Name,
                    IsHeadQuarters = o.IsHeadQuarters,
                    //Employees = o.OfficeEmployees.Select(e => new EmployeeListingServiceModel
                    //{
                    //    Id = e.EmployeeId,
                    //    FirstName = e.Employee.FirstName,
                    //    LastName = e.Employee.LastName,
                    //    StartingDate = e.Employee.StartingDate.Date,
                    //    ExperienceLevel = e.Employee.ExperienceLevel.ToString()
                    //})
                })
                .FirstOrDefaultAsync();

            return office;
        }

        public async Task<int> AddAsync(Address address, int cityId, int companyId, bool isHeadQuarters)
        {
            if (isHeadQuarters == true)
            {
                Office currentHeadquarters = await GetHeadquarters(companyId);

                if (currentHeadquarters != null)
                {
                    // Probably there should be only one headquarters. Exception could be thrown too.
                    currentHeadquarters.IsHeadQuarters = false;
                }
            }

            Office office = new Office
            {
                Address = address,
                CityId = cityId,
                CompanyId = companyId,
                IsHeadQuarters = isHeadQuarters
            };

            dbContext.Offices.Add(office);

            await dbContext.SaveChangesAsync();

            return office.Id;
        }

        public async Task EditAsync(OfficeCreateServiceModel officeDto)
        {
            Office office = await dbContext
                .Offices
                .FindAsync(officeDto.Id);

            if (office == null)
            {
                return;
            }

            if (officeDto.IsHeadQuarters == true)
            {
                var currentHeadquarters = await GetHeadquarters(officeDto.CompanyId);

                if (currentHeadquarters != null)
                {
                    // Probably there should be only one headquarters. Exception could be thrown too.
                    currentHeadquarters.IsHeadQuarters = false;
                }
            }

            office.Address = officeDto.Address;
            office.CompanyId = officeDto.CompanyId;
            office.CityId = officeDto.CityId;
            office.IsHeadQuarters = officeDto.IsHeadQuarters;

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Office office = await dbContext
                .Offices
                .FindAsync(id);

            dbContext.Offices.Remove(office);

            await dbContext.SaveChangesAsync();
        }

        public async Task<int> GetTotalAsync(string search)
        {
            var query = dbContext.Offices.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search) && search.Length >= ServicesConstants.MinimalSearchLength)
            {
                query = query.Where(o => o.City.Name.Contains(search)
                || o.City.Country.Name.Contains(search)
                || o.Address.Street.Contains(search));
            }

            return await query.CountAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await dbContext.Offices.AnyAsync(c => c.Id == id);

        private async Task<Office> GetHeadquarters(int companyId)
            => await dbContext
                    .Offices
                    .FirstOrDefaultAsync(o => o.CompanyId == companyId && o.IsHeadQuarters == true);

        private async Task<IEnumerable<OfficeListingServiceModel>> GetOfficesAsync(IQueryable<Office> query)
        {
            var offices = await query
                .Select(o => new OfficeListingServiceModel
                {
                    Id = o.Id,
                    City = o.City.Name,
                    Street = o.Address.Street,
                    StreetNumber = o.Address.StreetNumber,
                    EmployeesCount = o.OfficeEmployees.Count,
                    IsHeadQuarters = o.IsHeadQuarters
                })
                .ToListAsync();

            return offices;
        }
    }
}
