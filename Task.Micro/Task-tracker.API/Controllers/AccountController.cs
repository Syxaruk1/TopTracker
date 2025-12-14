using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_tracker.Application.Services;
using Task_tracker.Domain.Models;

namespace Task_tracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(AccountService accountService) : Controller
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody]User request)
        {
            accountService.Register(request.UserName, request.Email, request.Password);
            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User request)
        {
            var token = accountService.Login(request.Email, request.Password);

            HttpContext.Response.Cookies.Append("cook-ies", token);

            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
