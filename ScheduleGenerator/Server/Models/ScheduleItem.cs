using ScheduleGenerator.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleGenerator.Server.Models
{
    public class ScheduleItem
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string RoomNumber { get; set; }
        public string Lecturer { get; set; }
        [Range(0, 6)]
        public WeekDay DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Range(0, 5)]
        public TypeOfClasses TypeOfClasses { get; set; }
        [Range(0, 5)]
        public Color Color { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
