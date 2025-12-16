using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Task_tracker.Application.Services;
using Task_tracker.Domain.Models;

namespace Task_tracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpaceController(SpaceService spaceService, TaskService taskService, AccountService accountService) : Controller
    {
        [HttpPost]
        [Authorize]
        public IActionResult AddSpace(Guid ownerId, string title, string description)
        {
            spaceService.AddNewSpace(title, description, ownerId);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public IActionResult AddTaskToSpace(int spaceId, int taskId)
        {
            spaceService.AddTaskToSpace(spaceService.GetById(spaceId), taskService.GetTaskById(taskId));
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public IActionResult UpdateSpace(int spaceId, string title, string description)
        {
            spaceService.UpdateSpace(spaceService.GetById(spaceId), title, description);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public IActionResult ChangeOwnerOfSpace(int spaceId, int userId)
        {
            spaceService.ChangeOwnerOfSpace(spaceService.GetById(spaceId), accountService.GetById(userId));
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public IActionResult AddMemberToSpace(int spaceId, int userId)
        {
            spaceService.AddMemberToSpace(spaceService.GetById(spaceId), accountService.GetById(userId));
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteSpace(int spaceId)
        {
            spaceService.DeleteSpace(spaceId);
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
