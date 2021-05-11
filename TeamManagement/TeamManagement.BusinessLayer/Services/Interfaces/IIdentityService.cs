using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Options;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface IIdentityService
    {
        ChallengeResultData Authenticate(string callbackUrl);
        Task<bool> IsDomainAllowedAsync();
        Task<bool> TryLoginAsync();
        Task<bool> RegisterAsync();
        string GetRedirectUrl();
        Task<AppUser> GetAppUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<AppUser> GetAppUserAsync(Guid Id);
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityResult> AddToRoleAsync(Guid userId, string roleName);
        Task<bool> HasRole(string email, string role);
        Task<List<AppUser>> GetAllUsersAsync();
    }
}
