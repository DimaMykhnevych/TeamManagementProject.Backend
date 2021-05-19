using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.Controllers
{
    [ApiController]
    [Authorize(Roles = "CEO")]
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
    }
}
