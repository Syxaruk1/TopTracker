using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_tracker.Application.Services;
using Task_tracker.Domain.Models;

namespace Task_tracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController(ProjectService projectService, TaskService taskService) : Controller
    {
        [HttpPost]
        [Authorize]
        public IActionResult AddProject(string title, string description)
        {
            projectService.AddNewProject(title, description);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateProject(int projectId, string title, string description)
        {
            projectService.UpdateProject(projectService.GetById(projectId), title, description);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public IActionResult AddProjectToProject(int mainProjectId, int subProjectId)
        {
            projectService.AddProjectToProject(projectService.GetById(mainProjectId), projectService.GetById(subProjectId));
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public IActionResult AddTaskToProject(int projectId, int taskId)
        {
            projectService.AddTaskToProject(projectService.GetById(projectId), taskService.GetTaskById(taskId));
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteProject(int projectId)
        {
            projectService.DeleteProject(projectId);
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
