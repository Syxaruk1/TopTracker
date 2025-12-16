using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_tracker.Application.Services;

namespace Task_tracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController(TeamService teamService) : Controller
    {
        [HttpPost]
        [Authorize]
        public IActionResult AddNewTeam(string title, string description, int teamSize)
        {
            teamService.AddNewTeam(title, description, teamSize);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateTeam(int teamId, string title, string description)
        {
            teamService.UpdateTeam(teamService.GetById(teamId), title, description);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteTeam(int teamId)
        {
            teamService.DeleteTeam(teamId);
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
