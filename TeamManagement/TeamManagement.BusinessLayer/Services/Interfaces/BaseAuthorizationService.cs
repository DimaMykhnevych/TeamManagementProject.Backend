using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Factories.AuthTokenFactory;
using TeamManagement.DataLayer.Domain.Models.Auth;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public abstract class BaseAuthorizationService
    {
        private readonly IAuthTokenFactory _tokenFactory;
        public BaseAuthorizationService(IAuthTokenFactory tokenFactory)
        {
            _tokenFactory = tokenFactory;
        }
        public async Task<JWTTokenStatusResult> GenerateTokenAsync(AuthSignInModel model)
        {
            bool status = await VerifyUserAsync(model);
            if (!status)
            {
                return new JWTTokenStatusResult() { Token = null, IsAuthorized = false };
            }

            IEnumerable<Claim> claims = await GetUserClaimsAsync(model);
            JwtSecurityToken token = _tokenFactory.CreateToken(model.UserName.ToString(), claims);
            UserAuthInfo info = await GetUserInfoAsync(model.UserName);

            return new JWTTokenStatusResult()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                IsAuthorized = true,
                UserInfo = info,
            };
        }

        public abstract Task<IEnumerable<Claim>> GetUserClaimsAsync(AuthSignInModel model);
        public abstract Task<UserAuthInfo> GetUserInfoAsync(string userName);
        public abstract Task<bool> VerifyUserAsync(AuthSignInModel model);
    }
}
