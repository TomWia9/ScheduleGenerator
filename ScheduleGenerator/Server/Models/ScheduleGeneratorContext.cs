using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ScheduleGenerator.Server.Models
{
    public class ScheduleGeneratorContext : DbContext
    {
        public ScheduleGeneratorContext(DbContextOptions<ScheduleGeneratorContext> options) : base(options)
        {
        }
        
        
        
    }
}
