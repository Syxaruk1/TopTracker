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
            return Ok(token);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
