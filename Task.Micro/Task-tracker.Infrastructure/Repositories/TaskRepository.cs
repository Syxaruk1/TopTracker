using Task_tracker.Domain.Models;
using Task_tracker.Infrastructure.Database;

namespace Task_tracker.Infrastructure.Repositories;

public class TaskRepository
{
    private TaskContext Context { get; set; }
    public TaskRepository(TaskContext taskContext)
    { 
        Context = taskContext; 
    }

    public void Add(Domain.Models.Task task)
    {
        Context.Tasks.Add(task);
        Context.SaveChanges();
    }

    public void Update(Domain.Models.Task task, string title, string description)
    {
        var newtask = Context.Tasks.Find(task.Id);
        newtask.Title = title;
        newtask.Description = description;
        Context.Tasks.Update(newtask);
        Context.SaveChanges();
    }

    public void Update(Domain.Models.Task task, User user)
    {
        var newtask = Context.Tasks.Find(task.Id);
        newtask.DesignatedUserId = user.Id;
        Context.Tasks.Update(newtask);
        Context.SaveChanges();
    }

    public void Delete(Domain.Models.Task task)
    {
        Context.Tasks.Remove(task);
        Context.SaveChanges();
    }

    public Domain.Models.Task? GetById(int id)
    {
        try
        {
            
            return Context.Tasks.Find(id);
        }
        catch
        {
            return null;
        }
    }
}