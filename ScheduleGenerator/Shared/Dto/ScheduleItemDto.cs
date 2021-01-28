using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleGenerator.Shared.Enums;

namespace ScheduleGenerator.Shared.Dto
{
    /// <summary>
    /// Schedule item with Id, Subject, RoomNumber, Lecturer, DayOfWeek, StartTime, EndTime, TypeOfClasses, Color and ScheduleId fields
    /// </summary>
    public class ScheduleItemDto
    {
        /// <summary>
        /// The id of schedule item
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The subject of classes
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The room number
        /// </summary>
        public string RoomNumber { get; set; }
        /// <summary>
        /// The Lecturer name
        /// </summary>
        public string Lecturer { get; set; }
        /// <summary>
        /// The day of week
        /// </summary>
        public DayOfWeek DayOfWeek { get; set; }
        /// <summary>
        /// The start time of classes
        /// </summary>
        public TimeSpan StartTime { get; set; }
        /// <summary>
        /// The end time of classes
        /// </summary>
        public TimeSpan EndTime { get; set; }
        /// <summary>
        /// The type of classes
        /// </summary>
        public TypeOfClasses TypeOfClasses { get; set; }
        /// <summary>
        /// The color of schedule item
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// The id of schedule which this item belongs
        /// </summary>
        public int ScheduleId { get; set; }
    }
}
