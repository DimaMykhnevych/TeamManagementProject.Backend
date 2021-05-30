using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.Controllers
{
    [ApiController]
    [Authorize(Roles = "CEO, Administrator")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRegistrationService _employeeRegistrationService;

        public EmployeesController(IEmployeeRegistrationService employeeRegistrationService)
        {
            _employeeRegistrationService = employeeRegistrationService;
        }

        [HttpPost(ApiRoutes.Employee.BaseWithVersion)]
        public async Task<IActionResult> RegisterEmployee(EmployeeRegistrationRequest registrationRequest)
        {
            AppUser employee = await _employeeRegistrationService.RegisterEmployee(registrationRequest);
            return Ok(employee);
        }

        [HttpGet(ApiRoutes.Employee.BaseWithVersion)]
        public async Task<IActionResult> GetEmployees()
        {
            IEnumerable<AppUser> employees = await _employeeRegistrationService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet(ApiRoutes.Employee.AllEmployees)]
        public async Task<IActionResult> GetAllEmployeesExceptCeo()
        {
            IEnumerable<AppUser> employees = await _employeeRegistrationService.GetAllEmployeesExceptCeo();
            return Ok(employees);
        }

        [HttpPut(ApiRoutes.Employee.BaseWithVersion)]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeUpdateRequest employee)
        {
            AppUser user = await _employeeRegistrationService.UpdateEmployee(employee);
            return Ok(user);
        }

        [HttpDelete(ApiRoutes.Employee.Delete)]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            bool res = await _employeeRegistrationService.DeleteEmployee(id);
            if (res)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
