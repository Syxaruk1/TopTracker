using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;

namespace Task_tracker.Domain.Reposotories
{
    public class TaskRepository
    {
        private static IDictionary<string, Models.Task> Tasks = new Dictionary<string, Models.Task>();

        public void AddTask(Models.Task task)
        { Tasks[task.Title] = task; }

        public Models.Task? GetByTitle(string title)
        {
            try
            { return Tasks[title]; }
            catch
            { return null; }
        }
    }
}
