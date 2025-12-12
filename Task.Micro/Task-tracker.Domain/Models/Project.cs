
namespace Task_tracker.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Project : Entity
{
    public IEnumerable<Project> Projects { get; set; } = [];
    public IEnumerable<Task> Tasks { get; set; } = [];
}
