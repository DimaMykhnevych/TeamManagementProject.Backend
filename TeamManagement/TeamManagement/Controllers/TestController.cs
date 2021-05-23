using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeamManagement.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("test")]
        public IActionResult Test()
        {
            string cookieValue = @"test";
            HttpContext.Response.Cookies.Append("test", cookieValue,
              options: new Microsoft.AspNetCore.Http.CookieOptions()
              {
                  Secure = true,
                  HttpOnly = true,
                  SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None
              });
            return Ok("Test!");
        }
    }
}
