using CompanyManager.Common.Constants;
using CompanyManager.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeListingServiceModel>> GetAllAsync(
            string search,
            int page = 1,
            int pageSize = ServicesConstants.DefaultPageSize);

        Task<EmployeeDetailsServiceModel> GetByIdAsync(int id);

        Task<int> AddAsync(EmployeeCreateServiceModel employee);

        Task<bool> ExistsAsync(int id);

        Task DeleteAsync(int id);

        Task<int> GetTotalAsync(string search);

        Task EditAsync(EmployeeCreateServiceModel employee);

        Task<IEnumerable<EmployeeListingServiceModel>> SearchAsync(SearchCriteria searchCriteria);
    }
}
