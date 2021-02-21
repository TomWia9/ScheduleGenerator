using Microsoft.EntityFrameworkCore;
using ScheduleGenerator.Server.Shared.EntityConfiguration;

namespace ScheduleGenerator.Server.Models
{
    public class ScheduleGeneratorContext : DbContext
    {
        public ScheduleGeneratorContext(DbContextOptions<ScheduleGeneratorContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleItem> ScheduleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new SchedulesConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleItemsConfiguration());
        }
    }
}
