using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_tracker.Domain.Models;

namespace Task_tracker.Infrastructure.ConfigurationEntites;

internal class SpaceConfig : IEntityTypeConfiguration<Space>
{
    public void Configure(EntityTypeBuilder<Space> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(120).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(1200);
        builder.Property(x => x.OwnerId).IsRequired();
    }
}