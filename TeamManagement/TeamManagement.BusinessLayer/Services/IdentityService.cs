using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Options;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AuthOptions _authOptions;
        private readonly RoleManager<IdentityRole> _roleManager;

        private ExternalLoginInfo _externalLoginInfo;

        public IdentityService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            _authOptions = configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();
        }

        private async Task<ExternalLoginInfo> GetInfoAsync() =>
            _externalLoginInfo ??= await _signInManager.GetExternalLoginInfoAsync();

        public ChallengeResultData Authenticate(string redirectUrl)
        {
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                _authOptions.ProviderName, redirectUrl);
            return new ChallengeResultData { ProviderName = _authOptions.ProviderName, AuthenticationProperties = properties };
        }

        public async Task<bool> TryLoginAsync()
        {
            var information = await GetInfoAsync();
            if (information == null)
            {
                return false;
            }

            var signinResult = await _signInManager
                .ExternalLoginSignInAsync(information.LoginProvider, information.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            return signinResult.Succeeded;
        }

        public async Task<bool> RegisterAsync()
        {
            var information = await GetInfoAsync();
            string[] names = information.Principal.Identity.Name.Split(' ');
            string email = information.Principal.FindFirstValue(ClaimTypes.Email);

            var user = new AppUser
            {
                UserName = email,
                Email = email,
                FirstName = names[0],
                LastName = names[1]
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, information);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, information.LoginProvider);
                    return true;
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            return false;
        }

        public async Task<bool> IsDomainAllowedAsync()
        {
            var information = await GetInfoAsync();
            if (information != null && information.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                string email = information.Principal.FindFirstValue(ClaimTypes.Email);
                string[] emailParts = email.Trim().Split('@');
                bool res = _authOptions.WhiteListedDomains.Contains(emailParts[1]);
                return emailParts.Length == 2 &&
                    _authOptions.WhiteListedDomains.Contains(emailParts[1]);
            }
            return false;
        }

        public string GetRedirectUrl() => _authOptions.ReturnURL;

        public async Task<AppUser> GetAppUserAsync(ClaimsPrincipal claimsPrincipal) => await _userManager.GetUserAsync(claimsPrincipal);

        public async Task<AppUser> GetAppUserAsync(Guid Id) => await _userManager.FindByIdAsync(Id.ToString());

        public async Task<Team> GetAppUserTeam(ClaimsPrincipal claimsPrincipal) => (await _userManager.Users.Include(user => user.Team)
                                                                                                              .SingleOrDefaultAsync(user => user.Id == GetAppUserAsync(claimsPrincipal).Result.Id)).Team;
        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            var role = new IdentityRole();
            role.Name = roleName;
            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        public async Task<IdentityResult> AddToRoleAsync(Guid userId, string roleName)
        {
            var user = await GetAppUserAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError[] {
                    new IdentityError { Description = "User was not found in database" }
                });
            }

            if (!(await _roleManager.RoleExistsAsync(roleName)))
            {
                var roleCreateResult = await CreateRoleAsync(roleName);

                if (!roleCreateResult.Succeeded)
                {
                    return roleCreateResult;
                }
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result;
        }

        public async Task<bool> HasRole(string email, string role) => await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(email), role);

        public Task<List<AppUser>> GetAllUsersAsync() => _userManager.Users.ToListAsync();
    }
}
