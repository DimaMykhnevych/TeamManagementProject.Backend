using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Factories.AuthTokenFactory;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Domain.Models.Auth;

namespace TeamManagement.BusinessLayer.Services
{
    public class AppUserAuthorizationService : BaseAuthorizationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserAuthorizationService(
            IAuthTokenFactory tokenFactory,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
            : base(tokenFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public override async Task<IEnumerable<Claim>> GetUserClaimsAsync(AuthSignInModel model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return new List<Claim> { };
            }

            return new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.Role, user.Poision)
            };
        }

        public async override Task<bool> VerifyUserAsync(AuthSignInModel model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return false;
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            return result.Succeeded;
        }

        public async override Task<UserAuthInfo> GetUserInfoAsync(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
          
            UserAuthInfo info = new UserAuthInfo
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Poision = user.Poision
            };

            return info;
        }
    }
}
