using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleGenerator.Server.Models
{
    public class ScheduleItem
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string RoomNumber { get; set; }
        public string Lecturer { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        //public TypeOfSubject TypeOfSubject { get; set; }
        //public Color Color { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }


    }
}
