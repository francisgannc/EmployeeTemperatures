using EmployeeTemperatures.Contracts.Request;
using EmployeeTemperatures.Contracts.Response;
using EmployeeTemperatures.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeTemperatures.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        [HttpGet("api/v1/employee/all")]
        public async Task<IActionResult> GetAll()
        {
            var employess = await employeeService.GetEmployeesAsync();

            if (employess == null)
            {
                return NotFound();
            }

            return Ok(employess);
        }

        [HttpPost("api/v1/employee/create")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest employeeRequest)
        {
            if (employeeRequest != null)
            {
                var response = await employeeService.CreateEmployeeAsync(employeeRequest);

                if (response.Success)
                {
                    return Ok(response.EmployeeRecord);
                }

                return base.BadRequest(new EmployeeResponse
                {
                    Errors = response.Errors
                });
            }

            return base.BadRequest(new EmployeeResponse
            {
                Errors = new[] { "Something went wrong" }
            }); ;

        }

        [HttpPut("api/v1/employee/update/{employeeNumber}")]
        public async Task<IActionResult> Update([FromRoute] string employeeNumber, [FromBody] UpdateEmployeeRequest employeeRequest)
        {
            if (employeeNumber != null && employeeRequest != null)
            {
                var response = await employeeService.UpdateEmployeeAsync(employeeNumber, employeeRequest);

                if (response.Success)
                {
                    return Ok(response.EmployeeRecord);
                }

                return base.BadRequest(new EmployeeResponse
                {
                    Errors = response.Errors
                });
            }

            return base.BadRequest(new EmployeeResponse
            {
                Errors = new[] { "Something went wrong" }
            }); ;

        }

        [HttpPost("api/v1/employee/search")]
        public async Task<IActionResult> Search([FromBody] SearchEmployeeRequest employeeRequest)
        {
            if(employeeRequest != null )
            {
                var response = await employeeService.SearchEmployees(employeeRequest);

                return Ok(response.EmployeeRecords);
            }

            return base.BadRequest(new SearchEmployeeResponse
            {
                Errors = new[] { "Something went wrong" }
            }); ;
        }

    }
}
