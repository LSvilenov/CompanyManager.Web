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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeesService;
        private readonly IOfficeService officeService;

        public EmployeesController(IEmployeeService employeesService, IOfficeService officeService)
        {
            this.employeesService = employeesService;
            this.officeService = officeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsynct(
            string search,
            int page = 1,
            int pageSize = ServicesConstants.DefaultPageSize)
        {
            var employees = new EmployeeListingViewModel
            {
                Employees = await employeesService.GetAllAsync(search, page, pageSize),
                Total = await employeesService.GetTotalAsync(search),
                Page = page
            };

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            EmployeeDetailsServiceModel employee =
                await employeesService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> CreateAsync([FromBody] EmployeeCreateModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeCreateServiceModel employeeToCreate = new EmployeeCreateServiceModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                ExperienceLevel = employee.ExperienceLevel,
                StartingDate = employee.StartingDate,
                VacationDays = employee.VacationDays,
                Image = employee.Image,
                OfficeIds = employee.OfficeIds
            };

            //TODO: Fix the image problem.
            //var file = Request.Form.Files[0];

            int createdEmployeeId = await employeesService
                .AddAsync(employeeToCreate);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdEmployeeId });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditAsync(int id, [FromBody] EmployeeCreateModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await employeesService.ExistsAsync(id))
            {
                return NotFound();
            }

            EmployeeCreateServiceModel employeeToCreate = new EmployeeCreateServiceModel
            {
                Id = id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                ExperienceLevel = employee.ExperienceLevel,
                StartingDate = employee.StartingDate,
                VacationDays = employee.VacationDays,
                Image = employee.Image
            };

            await employeesService.EditAsync(employeeToCreate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await employeesService.ExistsAsync(id))
            {
                return NotFound();
            }

            await employeesService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet]
        [Route("{id}/offices")]
        public async Task<ActionResult> GetByCompanyAsync(int id)
        {
            var offices = await officeService
                .SearchAsync(new SearchCriteria { EmployeeId = id });

            return Ok(offices);
        }
    }
}
