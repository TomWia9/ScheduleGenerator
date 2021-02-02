using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleGenerator.Shared.Enums;

namespace ScheduleGenerator.Shared.Dto
{
    public abstract class ScheduleItemForManipulationDto
    {
        /// <summary>
        /// The subject of classes
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "The Subject field may contain at most 60 characters.")]
        [MinLength(3, ErrorMessage = "The Subject field must contain at least 3 characters.")]
        public string Subject { get; set; }
        /// <summary>
        /// The room number
        /// </summary>
        [MaxLength(10, ErrorMessage = "The RoomNumber field may contain at most 10 characters.")]
        [MinLength(1, ErrorMessage = "The RoomNumber field must contain at least 1 characters.")]
        public string RoomNumber { get; set; }
        /// <summary>
        /// The Lecturer name
        /// </summary>
        [MaxLength(50, ErrorMessage = "The Lecturer field may contain at most 50 characters.")]
        [MinLength(3, ErrorMessage = "The Lecturer field must contain at least 3 characters.")]
        public string Lecturer { get; set; }
        /// <summary>
        /// The day of week
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Range(0, 6)]
        public DayOfWeek DayOfWeek { get; set; }
        /// <summary>
        /// The start time of classes
        /// </summary>
        [Range(typeof(TimeSpan), "7:00", "20:45",
            ErrorMessage = "The StartTime field must be between {1} and {2}")]
        [Required(AllowEmptyStrings = false)]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// The end time of classes
        /// </summary>
        [Range(typeof(TimeSpan), "7:15", "21:00",
            ErrorMessage = "The EndTime field must be between {1} and {2}")]
        [Required(AllowEmptyStrings = false)]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// The type of classes
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Range(0, 6)]
        public TypeOfClasses TypeOfClasses { get; set; }
    }
}
