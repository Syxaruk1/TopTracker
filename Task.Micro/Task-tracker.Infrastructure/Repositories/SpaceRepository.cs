namespace Task_tracker.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;

public class SpaceRepository
    {
    private static IDictionary<string, Space> Spaces = new Dictionary<string, Space>();

    public void AddSpace(Space space)
    { 
        Spaces[space.Title] = space; 
    }

    public Space? GetByTitle(string title)
    {
        try
        { 
            return Spaces[title]; 
        }
        catch 
        { 
            return null; 
        }
    }
}