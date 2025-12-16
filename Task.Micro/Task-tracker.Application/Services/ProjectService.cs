using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;
using Task_tracker.Infrastructure.Repositories;

namespace Task_tracker.Application.Services;

public class ProjectService(ProjectRepository projectRepository)
{
    public void AddNewProject(string title, string description)
    {
        var project = new Project(title, description);
        projectRepository.Add(project);
    }

    public void DeleteProject(int id)
    {
        projectRepository.Delete(projectRepository.GetById(id));
    }

    public void UpdateProject(Project project, string title, string description)
    {
        projectRepository.Update(project, title, description);
    }

    public void AddProjectToProject(Project mainProject, Project subProject)
    {
        IEnumerable<Project> projects = mainProject.Projects;
        projects.Append(subProject);
        projectRepository.Update(mainProject, projects);
    }

    public void AddTaskToProject(Project mainProject, Domain.Models.Task task)
    {
        IEnumerable<Domain.Models.Task> tasks = mainProject.Tasks;
        tasks.Append(task);
        projectRepository.Update(mainProject, tasks);
    }

    public Project? GetById(int id)
    {
        return projectRepository.GetById(id);
    }
}
