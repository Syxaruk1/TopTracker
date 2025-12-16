using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;
using Task_tracker.Infrastructure.Database;

namespace Task_tracker.Infrastructure.Repositories;

public class ProjectRepository
{
    private TaskContext Context { get; set; }
    public ProjectRepository(TaskContext taskContext)
    {
        Context = taskContext;
    }

    public void Add(Project project)
    {
        Context.Projects.Add(project);
        Context.SaveChanges();
    }

    public void Update(Project project, string title, string description)
    {
        var newproject = Context.Projects.Find(project.Id);
        newproject.Title = title;
        newproject.Description = description;
        Context.Projects.Update(newproject);
        Context.SaveChanges();
    }

    public void Update(Project project, IEnumerable<Project> projects)
    {
        var newproject = Context.Projects.Find(project.Id);
        newproject.Projects = projects;
        Context.Projects.Update(newproject);
        Context.SaveChanges();
    }

    public void Update(Project project, IEnumerable<Domain.Models.Task> tasks)
    {
        var newproject = Context.Projects.Find(project.Id);
        newproject.Tasks = tasks;
        Context.Projects.Update(newproject);
        Context.SaveChanges();
    }

    public void Delete(Project project)
    {
        Context.Projects.Remove(project);
        Context.SaveChanges();
    }

    public Project? GetById(int id)
    {
        try
        {
            return Context.Projects.Find(id);
        }
        catch
        {
            return null;
        }
    }
}
