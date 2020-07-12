using System.Threading.Tasks;

using CompanyManager.Common.Constants;
using CompanyManager.Data.Models;
using CompanyManager.Services.Contracts;
using CompanyManager.Services.Models;
using CompanyManager.Web.Models;

using Microsoft.AspNetCore.Mvc;


namespace CompanyManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeService officeService;
        private readonly IEmployeeService employeeService;

        public OfficesController(IOfficeService officeService, IEmployeeService employeeService)
        {
            this.officeService = officeService;
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync(
            string search, 
            int page = 1, 
            int pageSize = ServicesConstants.DefaultPageSize)
        {
            var offices = new OfficeListingViewModel
            {
                Offices = await officeService.GetAllAsync(search, page, pageSize),
                Total = await officeService.GetTotalAsync(search),
                Page = page
            };

            return Ok(offices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            OfficeDetailsServiceModel office =
                await officeService.GetByIdAsync(id);

            if (office == null)
            {
                return NotFound();
            }

            return Ok(office);
        }

        [HttpGet]
        [Route("city/{cityId}")]
        public async Task<ActionResult> GetByCityAsync(int cityId)
        {
            var offices = await officeService
                .SearchAsync(new SearchCriteria { CityId = cityId });

            return Ok(offices);
        }

        [HttpGet]
        [Route("country/{countryId}")]
        public async Task<ActionResult> GetByCountryAsync(int countryId)
        {
            var offices = await officeService
                .SearchAsync(new SearchCriteria { CountryId = countryId });

            return Ok(offices);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] OfficeCreateModel office)
        {
            // TODO: Add documents. Add validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Address address = new Address
            {
                Street = office.Street,
                StreetNumber = office.StreetNumber
            };

            int createdOfficeId = await officeService
                .AddAsync(address, office.CityId, office.CompanyId, office.IsHeadQuarters);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdOfficeId });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditAsync(int id, [FromBody] OfficeCreateModel office)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (!await officeService.ExistsAsync(id))
            {
                return NotFound();
            }

            var officeToEdit = new OfficeCreateServiceModel
            {
                Id = id,
                Address = new Address { Street = office.Street, StreetNumber = office.StreetNumber },
                CityId = office.CityId,
                CompanyId = office.CompanyId,
                IsHeadQuarters = office.IsHeadQuarters
            };

            await officeService
                .EditAsync(officeToEdit);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (!await officeService.ExistsAsync(id))
            {
                return NotFound();
            }

            await officeService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet]
        [Route("{id}/employees")]
        public async Task<ActionResult> GetByCompanyAsync(int id)
        {
            var offices = await employeeService
                .SearchAsync(new SearchCriteria { OfficeID = id });

            return Ok(offices);
        }
    }
}
