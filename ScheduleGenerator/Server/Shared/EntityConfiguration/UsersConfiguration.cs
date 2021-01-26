using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleGenerator.Server.Models;

namespace ScheduleGenerator.Server.Shared.EntityConfiguration
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Password)
                .HasMaxLength(64);

            builder.HasMany(u => u.Schedules)
                .WithOne(s => s.User)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
