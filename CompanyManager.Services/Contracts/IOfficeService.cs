using System.Collections.Generic;
using System.Threading.Tasks;

using CompanyManager.Common.Constants;
using CompanyManager.Data.Models;
using CompanyManager.Services.Models;

namespace CompanyManager.Services.Contracts
{
    public interface IOfficeService
    {
        Task<IEnumerable<OfficeListingServiceModel>> GetAllAsync(
            string search,
            int page = 1,
            int pageSize = ServicesConstants.DefaultPageSize);

        Task<OfficeDetailsServiceModel> GetByIdAsync(int id);

        Task<int> AddAsync(Address address, int cityId, int companyId, bool isHeadQuarters);

        Task<bool> ExistsAsync(int id);

        Task DeleteAsync(int id);

        Task<int> GetTotalAsync(string search);

        Task EditAsync(OfficeCreateServiceModel office);

        Task<IEnumerable<OfficeListingServiceModel>> SearchAsync(SearchCriteria searchCriteria);
    }
}
