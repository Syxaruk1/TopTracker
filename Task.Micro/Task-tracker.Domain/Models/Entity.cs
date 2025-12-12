namespace Task_tracker.Domain.Models;

public class Entity
{
    public virtual Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}