using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleGenerator.Shared.Enums;

namespace ScheduleGenerator.Shared.Dto
{
    public class ScheduleItemDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string RoomNumber { get; set; }
        public string Lecturer { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TypeOfClasses TypeOfClasses { get; set; }
        public Color Color { get; set; }
        public int ScheduleId { get; set; }
    }
}
