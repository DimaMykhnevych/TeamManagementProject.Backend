using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Domain.Models.Auth;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> CreateUserAsync(UserCreateRequest userModel, string role);
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<SignInResult> LoginAsync(AuthSignInModel loginUserRequest);
        Task LogOut();
    }
}
