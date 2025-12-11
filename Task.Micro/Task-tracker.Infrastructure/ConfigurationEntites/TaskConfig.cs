using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Task_tracker.Domain.Enums;
using Task = Task_tracker.Domain.Models.Task;

namespace Task_tracker.Infrastructure.ConfigurationEntites;

internal class TaskConfig : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(60).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(1200);
        builder.Property(x => x.Status).HasConversion(new EnumToStringConverter<StatusTask>()).HasMaxLength(60);
    }
}