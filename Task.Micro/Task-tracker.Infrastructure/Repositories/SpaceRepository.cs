namespace Task_tracker.Infrastructure.Repositories;
using Task_tracker.Domain.Models;

public class SpaceRepository
{
    private static IDictionary<string, Space> Spaces = new Dictionary<string, Space>();

    public void Add(Space space)
    {
        Spaces[space.Title] = space;
    }

    public Space? GetByTitle(string title)
    {
        try
        {
            return Spaces[title];
        }
        catch
        {
            return null;
        }
    }
}