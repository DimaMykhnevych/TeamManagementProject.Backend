using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        private readonly IUserService _userService;

        public AuthController(BaseAuthorizationService authorizationService, IUserService userService)
        {
            _authorizationService = authorizationService;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Login([FromBody] AuthSignInModel model)
        {
            var user = await _userService.LoginAsync(model);
            var t = User.Identity;
            return Ok(user);
        }

        [HttpGet(ApiRoutes.Auth.Logout)]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogOut();
            return Ok();
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
