namespace Task_tracker.Application.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;
using Task_tracker.Infrastructure.Repositories;

public class SpaceService(SpaceRepository spaceRepository)
{
    public void AddNewSpace(string title, Guid ownerid)
    {
        var space = new Space()
        {
            Title = title,
            OwnerId = ownerid
        };
        spaceRepository.Add(space);
    }
    public void AddNewSpace(string title, string desc, Guid ownerid)
    {
        var space = new Space()
        {
            Title = title,
            Description = desc,
            OwnerId = ownerid
        };
        spaceRepository.Add(space);
    }
}
