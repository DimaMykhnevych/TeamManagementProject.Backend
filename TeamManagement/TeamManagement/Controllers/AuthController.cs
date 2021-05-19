using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.Authorization;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Domain.Models.Auth;

namespace TeamManagement.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BaseAuthorizationService _authorizationService;

        public AuthController(BaseAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Login([FromBody] AuthSignInModel model)
        {
            JWTTokenStatusResult result = await _authorizationService.GenerateTokenAsync(model);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.Auth.UserInfo)]
        public async Task<IActionResult> GetUserInfo()
        {
            string currentUserName = User.Identity.Name;
            UserAuthInfo userInfo = await _authorizationService.GetUserInfoAsync(currentUserName);

            if (userInfo == null)
            {
                return NotFound();
            }

            return Ok(userInfo);
        }
    }
}
