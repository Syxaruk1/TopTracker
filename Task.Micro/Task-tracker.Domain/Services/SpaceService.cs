using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;
using Task_tracker.Domain.Repositories;

namespace Task_tracker.Domain.Services
{
    public class SpaceService(SpaceRepository spaceRepository)
    {
        public void AddNewSpace(string title, Guid ownerid)
        {
            var space = new Space()
            {
                Title = title,
                OwnerId = ownerid
            };
            spaceRepository.AddSpace(space);
        }

        public void AddTaskToSpace(Space space, Models.Task task)
        {
            spaceRepository.AddTaskToSpace(space, task);
        }
    }
}
