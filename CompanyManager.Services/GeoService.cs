using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CompanyManager.Data;
using CompanyManager.Services.Contracts;
using CompanyManager.Services.Models;

using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Services
{
    public class GeoService : IGeoService
    {
        private readonly ApplicationDbContext dbContext;

        public GeoService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Nomenclature>> GetCityesAsync()
        {
            return await dbContext
                .Cities
                .Select(c => new Nomenclature { Id = c.Id, Name = c.Name })
                .ToListAsync();
        }
    }
}
