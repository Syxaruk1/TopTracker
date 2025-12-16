using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Task_tracker.Domain.Models;
using Task = Task_tracker.Domain.Models.Task;

namespace Task_tracker.Infrastructure.Database;

public class TaskContext : DbContext
{
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Team> Teams { get; set; }
    public virtual DbSet<Space> Spaces { get; set; }
    public virtual DbSet<Task> Tasks { get; set; }

    public TaskContext(DbContextOptions<TaskContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}