using ScheduleGenerator.Shared.Enums;
using System;

namespace ScheduleGenerator.Shared.Dto
{
    public abstract class ScheduleItemForManipulationDto
    {
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
        public WeekDay DayOfWeek { get; set; }
        /// <summary>
        /// The start time of classes
        /// </summary>
        //[Range(typeof(TimeSpan), "7:00", "20:45",
        //    ErrorMessage = "The StartTime field must be between {1} and {2}")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// The end time of classes
        /// </summary>
        //[Range(typeof(TimeSpan), "7:15", "21:00",
        //    ErrorMessage = "The EndTime field must be between {1} and {2}")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// The type of classes
        /// </summary>
        public TypeOfClasses TypeOfClasses { get; set; }
    }
}
