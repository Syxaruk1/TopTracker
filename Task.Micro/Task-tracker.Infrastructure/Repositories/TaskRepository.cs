using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Enums;
using Task_tracker.Domain.Models;

namespace Task_tracker.Infrastructure.Repositories
{
    public class TaskRepository
    {
        private static IDictionary<string, Domain.Models.Task> Tasks = new Dictionary<string, Domain.Models.Task>();
        
        public void AddTask(Domain.Models.Task task)
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
}
