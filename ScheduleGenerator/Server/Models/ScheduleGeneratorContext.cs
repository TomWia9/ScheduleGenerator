using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScheduleGenerator.Server.Shared;

namespace ScheduleGenerator.Server.Models
{
    public class ScheduleGeneratorContext : DbContext
    {
        public ScheduleGeneratorContext(DbContextOptions<ScheduleGeneratorContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
        }
    }
}
