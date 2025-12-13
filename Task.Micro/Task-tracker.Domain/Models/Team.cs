namespace Task_tracker.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Team : Entity
{
    public IEnumerable<User> Users { get; set; } = [];
    int TeamSize { get; set; } // CountUsers, но по-другому
}
