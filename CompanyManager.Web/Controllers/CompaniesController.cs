using System.Threading.Tasks;

using CompanyManager.Common.Constants;
using CompanyManager.Services.Contracts;
using CompanyManager.Services.Models;
using CompanyManager.Web.Models;

using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly IOfficeService officeService;

        public CompaniesController(ICompanyService companyService, IOfficeService officeService)
        {
            this.companyService = companyService;
            this.officeService = officeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync(string name, int page = 1, int pageSize = ServicesConstants.DefaultPageSize)
        {
            // TODO: Add documentation
            var companies = new CompanyListingViewModel
            {
                Companies = await companyService.GetAllAsync(name, page, pageSize),
                Total = await companyService.GetTotalAsync(name),
                Page = page
            };

            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            CompanyDetailsServiceModel company = await companyService
                .GetByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CompanyCreateModel company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int createdCompanyId =
                await companyService.AddAsync(company.Name, company.CreationDate);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdCompanyId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, [FromBody] CompanyCreateModel company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (!await companyService.ExistsAsync(id))
            {
                return NotFound();
            }

            await companyService
                .EditAsync(id, company.Name, company.CreationDate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await companyService.ExistsAsync(id))
            {
                return NotFound();
            }

            await companyService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet]
        [Route("{id}/offices")]
        public async Task<ActionResult> GetByCompanyAsync(int id)
        {
            var offices = await officeService
                .SearchAsync(new SearchCriteria { CompanyId = id });

            return Ok(offices);
        }
    }
}
