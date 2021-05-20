using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> CreateUserAsync(UserCreateRequest userModel, string role);
        Task<IEnumerable<AppUser>> GetAllUsers();
    }
}
