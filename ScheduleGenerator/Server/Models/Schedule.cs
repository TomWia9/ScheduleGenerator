using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleGenerator.Server.Models
{
    public class Schedule
    {
        public Schedule()
        {
            DateOfCreation = DateTime.Now;
        }

        public int  Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public IEnumerable<ScheduleItem> ScheduleItems { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
