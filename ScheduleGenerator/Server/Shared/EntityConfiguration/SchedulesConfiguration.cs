using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleGenerator.Server.Models;

namespace ScheduleGenerator.Server.Shared.EntityConfiguration
{
    public class SchedulesConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(s => s.DateOfCreation)
                .IsRequired();

            builder.HasMany(s => s.ScheduleItems)
                .WithOne(i => i.Schedule)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
