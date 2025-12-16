using Task_tracker.Domain.Enums;

namespace Task_tracker.Domain.Models;

/// <summary>
/// [Сущность задача]
/// Title - Название задачи
/// Description - Описание задачи ( может не быть )
/// StartDate - Дата начала задачи ( может не быть )
/// EndDate - Дата завершения задачи ( может не быть )
/// Status - Enum тип
/// DesignatedUserId - назначенный пользователь для выполнения задачи ( может не быть )
/// </summary>
public class Task : Entity
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public StatusTask Status { get; set; } = StatusTask.Waiting;
    public Guid? DesignatedUserId { get; set; }

    public Task()
    { }
    public Task(string title, string description)
    {
        Title = title;
        Description = description;
    }
}