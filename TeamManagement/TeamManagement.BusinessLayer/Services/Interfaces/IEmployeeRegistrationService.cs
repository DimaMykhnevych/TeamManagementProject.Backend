using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface IEmployeeRegistrationService
    {
        Task<AppUser> RegisterEmployee(EmployeeRegistrationRequest employee, string currentUserName);
        Task<IEnumerable<AppUser>> GetAllEmployeesExceptCeo(string currentUserName);
        Task<IEnumerable<AppUser>> GetEmployees(string currentUserName);
        Task<AppUser> UpdateEmployee(EmployeeUpdateRequest employeeToUpdate);
        Task<bool> DeleteEmployee(string id);
    }
}
