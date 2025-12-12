using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;
using Task_tracker.Domain.Reposotories;

namespace Task_tracker.Domain.Services
{
    public class TaskService(TaskRepository taskRepository)
    {
        public void AddNewTask(string title)
        {
            var task = new Models.Task()
            {
                Title = title
            };
            taskRepository.AddTask(task);
        }
    }
}
