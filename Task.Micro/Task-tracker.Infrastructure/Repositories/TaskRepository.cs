namespace Task_tracker.Infrastructure.Repositories;

public class TaskRepository
{
    private static IDictionary<string, Domain.Models.Task> Tasks = new Dictionary<string, Domain.Models.Task>();

    public void Add(Domain.Models.Task task)
    {
        Tasks[task.Title] = task;
    }

    public Domain.Models.Task? GetByTitle(string title)
    {
        try
        {
            return Tasks[title];
        }
        catch
        {
            return null;
        }
    }
}