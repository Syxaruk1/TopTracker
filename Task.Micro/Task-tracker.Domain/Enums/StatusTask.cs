namespace Task_tracker.Domain.Enums;

/// <summary>
/// Success - задача выполнена
/// Failure - задача не выполнена
/// Overdue - задача просроченна, статус будет ставится автоматически если установлены дата начала и дата конца
/// Deffered - задача отложенна
/// Progress - задача в процессе выполнения
/// Waiting - задача ожидает ( дефолтный статус для каждой новой созданной задачи )
/// </summary>
public enum StatusTask
{
    Success,
    Failure,
    Overdue,
    Deferred,
    Progress,
    Waiting
}