using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;
using Task_tracker.Infrastructure.Database;

namespace Task_tracker.Infrastructure.Repositories;

public class TeamRepository
{
    private TaskContext Context { get; set; }
    public TeamRepository(TaskContext taskContext)
    {
        Context = taskContext;
    }

    public void Add(Team team)
    {
        Context.Teams.Add(team);
    }

    public void Update(Team team, string title, string description)
    {
        var newteam = Context.Teams.Find(team.Id);
        newteam.Title = title;
        newteam.Description = description;
        Context.Teams.Update(newteam);
        Context.SaveChanges();
    }

    public void Delete(Team team)
    {
        Context.Teams.Remove(team);
        Context.SaveChanges();
    }

    public Team? GetById(int id)
    {
        try
        {
            return Context.Teams.Find(id);
        }
        catch 
        { 
            return null; 
        }
    }
}
