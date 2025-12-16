namespace Task_tracker.Infrastructure.Repositories;

using System.Threading.Tasks;
using Task_tracker.Domain.Models;
using Task_tracker.Infrastructure.Database;

public class SpaceRepository
{
    private TaskContext Context { get; set; }
    public SpaceRepository(TaskContext taskContext)
    {
        Context = taskContext;
    }

    public void Add(Space space)
    {
        Context.Spaces.Add(space);
        Context.SaveChanges();
    }

    public void Update(Space space, string title, string description)
    {
        var newspace = Context.Spaces.Find(space.Id);
        newspace.Title = title;
        newspace.Description = description;
        Context.Spaces.Update(newspace);
        Context.SaveChanges();
    }
    public void Update(Space space, IEnumerable<Domain.Models.Task> tasks)
    {
        var newspace = Context.Spaces.Find(space.Id);
        newspace.Tasks = tasks;
        Context.Spaces.Update(newspace);
        Context.SaveChanges();
    }
    public void UpdateMembers(Space space, User user)
    {
        var newspace = Context.Spaces.Find(space.Id);
        newspace.UserIds.Append(user.Id);
        Context.Spaces.Update(newspace);
        Context.SaveChanges();
    }

    public void UpdateOwner(Space space, User user)
    {
        var newspace = Context.Spaces.Find(space.Id);
        newspace.OwnerId = user.Id;
        Context.Spaces.Update(newspace);
        Context.SaveChanges();
    }

    public void Delete(Space space)
    {
        Context.Spaces.Remove(space);
        Context.SaveChanges();
    }

    public Space? GetById(int id)
    {
        try
        {
            return Context.Spaces.Find(id);
        }
        catch
        {
            return null;
        }
    }
}