using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface IEmployeeRegistrationService
    {
        Task<AppUser> RegisterEmployee(EmployeeRegistrationRequest employee);
    }
}
