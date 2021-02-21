using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleGenerator.Server.Models;

namespace ScheduleGenerator.Server.Shared.EntityConfiguration
{
    public class ScheduleItemsConfiguration : IEntityTypeConfiguration<ScheduleItem>
    {
        public void Configure(EntityTypeBuilder<ScheduleItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Subject)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(i => i.RoomNumber)
                .HasMaxLength(10);

            builder.Property(i => i.Lecturer)
                .HasMaxLength(50);

            builder.Property(i => i.DayOfWeek)
                .IsRequired();

            builder.Property(i => i.StartTime)
                .IsRequired();

            builder.Property(i => i.EndTime)
                .IsRequired();

            builder.Property(i => i.TypeOfClasses)
                .IsRequired();

            builder.Property(i => i.Color)
                .IsRequired();
        }
    }
}
