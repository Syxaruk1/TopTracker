using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Task_tracker.Domain.Models;

public class AccountContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }

    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
