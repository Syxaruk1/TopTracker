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
    public void AddNewSpace(string title, string desc, Guid ownerid)
    {
        var space = new Space(ownerid, title, desc);
        spaceRepository.Add(space);
    }
    public void UpdateSpace(Space space, string title, string description)
    {
        spaceRepository.Update(space, title, description);
    }
    public void DeleteSpace(int id)
    {
        spaceRepository.Delete(spaceRepository.GetById(id));
    }
    public void AddMemberToSpace(Space space, User user)
    {
        spaceRepository.UpdateMembers(space, user);
    }
    public void ChangeOwnerOfSpace(Space space, User user)
    {
        spaceRepository.UpdateOwner(space, user);
    }
    public void AddTaskToSpace(Space space, Domain.Models.Task task)
    {
        IEnumerable<Domain.Models.Task> tasks = space.Tasks;
        tasks.Append(task);
        spaceRepository.Update(space, tasks);
    }
    public Space? GetById(int id)
    {
        return spaceRepository.GetById(id);
    }
}
