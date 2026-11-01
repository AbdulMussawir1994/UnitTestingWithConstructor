using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService service) : ControllerBase
    {
        [HttpGet("GetAllEmployees")]
        public ActionResult GetEmployees()
        {
            var response = service.GetEmployees();

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("GetById/{id:int}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            if (id == 0)
                return BadRequest();

            var response = service.GetEmployeeById(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}
