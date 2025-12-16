using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;
using Task_tracker.Infrastructure.Repositories;

namespace Task_tracker.Application.Services;

public class TeamService(TeamRepository teamRepository)
{
    public void AddNewTeam(string title, string description, int teamSize)
    {
        var team = new Team(title, description, teamSize);
        teamRepository.Add(team);
    }
    
    public void UpdateTeam(Team team, string title, string description)
    {
        teamRepository.Update(team, title, description);
    }

    public void DeleteTeam(int id)
    {
        teamRepository.Delete(teamRepository.GetById(id));
    }

    public Team? GetById(int id)
    {
        return teamRepository.GetById(id);
    }
}
