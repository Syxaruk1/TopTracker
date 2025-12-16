using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_tracker.Domain.Models;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.UserName).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(30).IsRequired();
        builder.Property(x => x.Password).HasMaxLength(50).IsRequired();
    }
}