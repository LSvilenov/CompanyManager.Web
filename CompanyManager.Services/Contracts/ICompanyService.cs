using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CompanyManager.Common.Constants;
using CompanyManager.Services.Models;

namespace CompanyManager.Services.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyListingServiceModel>> GetAllAsync(
            string name,
            int page = 1,
            int pageSize = ServicesConstants.DefaultPageSize);

        Task<int> AddAsync(string name, DateTime creationDate);

        Task EditAsync(int id, string name, DateTime creationDate);

        Task DeleteAsync(int id);

        Task<CompanyDetailsServiceModel> GetByIdAsync(int companyId);

        Task<int> GetTotalAsync(string name);

        Task<bool> ExistsAsync(int id);
    }
}
