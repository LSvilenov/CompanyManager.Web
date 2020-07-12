using System.Collections.Generic;
using System.Threading.Tasks;

using CompanyManager.Services.Contracts;
using CompanyManager.Services.Models;

using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly IGeoService geoService;

        public GeoController(IGeoService geoService)
        {
            this.geoService = geoService;
        }

        [HttpGet("cities")]
        public async Task<ActionResult> GetCitiesAsync()
        {
            IEnumerable<Nomenclature> cities =
                await geoService.GetCityesAsync();

            return Ok(cities);
        }
    }
}
