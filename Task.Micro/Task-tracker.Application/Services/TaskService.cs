namespace Task_tracker.Application.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Infrastructure.Repositories;

public class TaskService(TaskRepository taskRepository)
{
    public void AddNewTask(string title)
    {
        var task = new Domain.Models.Task()
        {
            Title = title
        };
        taskRepository.AddTask(task);
    }
    // Перегрузка ниже для какого-нибудь "расширенного режима" добавления задачи, в котором сразу нужно будет настроить всё необходимое
    // при изначальном подходе указывается только название задачи, а далее всё остальное можно будет настроить вручную
    public void AddNewTask(string title, string description, DateTime startDate, DateTime endDate, Guid designatedUserId)
    {
        var task = new Domain.Models.Task()
        {
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            DesignatedUserId = designatedUserId
        };
        taskRepository.AddTask(task);
    }
}