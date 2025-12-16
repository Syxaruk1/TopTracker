using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Task_tracker.Application.Services;

namespace Task_tracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController(TaskService taskService, AccountService accountService) : Controller
    {
        [HttpPost]
        [Authorize]
        public IActionResult AddTask(string title, string description)
        {
            taskService.AddNewTask(title, description);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public IActionResult UpdateTask(int id, string title, string description)
        {
            var task = taskService.GetTaskById(id);
            taskService.UpdateTask(task, title, description);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public IActionResult UpdateTask(int id, int userId)
        {
            var task = taskService.GetTaskById(id);
            var user = accountService.GetById(userId);
            taskService.AddUserToTask(task, user);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteTask(int taskId)
        {
            taskService.DeleteTask(taskId);
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
