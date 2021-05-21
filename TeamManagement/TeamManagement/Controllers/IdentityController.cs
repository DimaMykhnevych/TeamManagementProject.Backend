using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.Authorization;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;
using TeamManagement.Contracts.v1.Responses;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public IdentityController(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        [HttpPost(ApiRoutes.Identity.Register)]
        public IActionResult Authenticate(string returnUrl)
        {
            var callbackUrl = $"{Request.Scheme}://{Request.Host.Value}/{ApiRoutes.Identity.Callback}" +
                $"?returnUrl={returnUrl}";
            var challengeResultData = _identityService.Authenticate(callbackUrl);
            return new ChallengeResult(challengeResultData.ProviderName, challengeResultData.AuthenticationProperties);
        }

        [HttpGet(ApiRoutes.Identity.Callback)]
        public async Task<IActionResult> AuthenticateCallbackAsync(string returnUrl, string remoteError = null)
        {
            if (remoteError != null)
            {
                return BadRequest(new { message = $"Error from external provider: {remoteError}" });
            }

            if (!await _identityService.IsDomainAllowedAsync())
            {
                return BadRequest(new { message = "Your domain isn't allowed to access this website." });
            }

            var alterCookieUrl = $"{Request.Scheme}://{Request.Host.Value}" +
                $"/{ApiRoutes.Identity.AlterCookieCallback}" +
                $"?returnUrl={returnUrl}";
            if (await _identityService.TryLoginAsync())
            {
                return Redirect(alterCookieUrl);
            }
            else
            {
                if (await _identityService.RegisterAsync())
                {
                    return Redirect(alterCookieUrl);
                }
            }
            return BadRequest(new { message = "Registration failed." });
        }

        /// <summary>
        ///     This endpoint gets requested right after the login or registration & login
        ///     process was performed. The user agent is redirected here, and ASP.NET Core
        ///     Identity cookie gets updated to use SameSite=None.
        /// </summary>
        /// <param name="returnUrl"> 
        ///     The URL to redirect the user agent after successful cookie update. 
        /// </param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Identity.AlterCookieCallback)]
        public IActionResult AlterCookieCallback(string returnUrl)
        {
            const string AUTH_COOKIE_NAME = ".AspNetCore.Identity.Application";

            if (!HttpContext.Request.Cookies.ContainsKey(AUTH_COOKIE_NAME))
            {
                return BadRequest(new { message = "Registration failed." });
            }
            string cookieValue = HttpContext.Request.Cookies[AUTH_COOKIE_NAME];

            HttpContext.Response.Cookies.Delete(AUTH_COOKIE_NAME);
            HttpContext.Response.Cookies.Append(AUTH_COOKIE_NAME, cookieValue,
                options: new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Secure = true,
                    HttpOnly = true,
                    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None
                });

            returnUrl ??= _identityService.GetRedirectUrl();
            return Redirect(returnUrl);
        }



        [HttpPut(ApiRoutes.Identity.MakeAdmin)]
        [RequireRoles("Administrator")]
        public async Task<IActionResult> MakeAdmin(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id was null");
            }

            var result = await _identityService.AddToRoleAsync(id, "Administrator");

            if (result.Succeeded)
            {
                return Ok();
            }

            var errors = result.Errors.Select(error => error.Description);
            return StatusCode(500, errors);
        }

        [HttpGet(ApiRoutes.Identity.GetUsers)]
        [RequireRoles("Administrator")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _identityService.GetAllUsersAsync();
            var usersResponse = _mapper.Map<List<AppUser>, List<GetUserResponse>>(users);
            return Ok(usersResponse);
        }

        [HttpGet(ApiRoutes.Identity.GetUser)]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var user = await _identityService.GetAppUserAsync(this.User);
            var userResponse = _mapper.Map<AppUser, GetUserResponse>(user);

            if (userResponse != null)
            {
                return Ok(userResponse);
            }

            return NotFound();
        }

        [HttpGet(ApiRoutes.Identity.GetTeamMembers)]
        [Authorize]
        public async Task<IActionResult> GetTeamMembers()
        {
            var userTeam = await _identityService.GetAppUserTeam(this.User);

            var currUser = await _identityService.GetAppUserAsync(this.User);

            if (userTeam == null)
            {
                return NotFound();
            }

            userTeam.Members.Remove(currUser);

            var userResponse = _mapper.Map<List<AppUser>, List<TeamMembersResponse>>(userTeam.Members);

            if (userResponse != null)
            {
                return Ok(userResponse);
            }

            return NotFound();
        }
    }
}
