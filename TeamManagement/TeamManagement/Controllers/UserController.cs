using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;

namespace TeamManagement.Controllers
{
    [ApiController]
    [Authorize(Roles = "CEO")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(ApiRoutes.User.BaseWithVersion)]
        public async Task<IActionResult> GetUsers()
        {
            var users =  await _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
