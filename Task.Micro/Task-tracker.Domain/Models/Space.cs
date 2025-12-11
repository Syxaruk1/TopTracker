namespace Task_tracker.Domain.Models;

/// <summary>
/// [Сущность пространство]
/// Рассматривается как отдельный проект, который содержит в себе список задач
/// Title - название пространства
/// Description - Описание пространства ( может не быть )
/// OwnerId - id владельца пространства
/// Tasks - список задач которые относятся к этому пространству
/// UserIds - список id юзеров которые имеют доступ к этому пространству
/// </summary>
public class Space : Entity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid OwnerId { get; set; }
    public IEnumerable<Task> Tasks { get; set; } = [];
    public IEnumerable<Guid> UserIds { get; set; } = [];
}