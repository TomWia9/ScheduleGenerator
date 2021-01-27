using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .HasMaxLength(40);

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
        }
    }
}
