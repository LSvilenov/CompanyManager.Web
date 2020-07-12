using System.Threading.Tasks;
using System.Collections.Generic;

using CompanyManager.Services.Models;

namespace CompanyManager.Services.Contracts
{
    public interface IGeoService
    {
        Task<IEnumerable<Nomenclature>> GetCityesAsync();
    }
}
