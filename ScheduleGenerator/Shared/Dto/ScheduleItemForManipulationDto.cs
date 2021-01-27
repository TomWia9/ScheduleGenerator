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
        [Required(AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "The Subject field may contain at most 60 characters.")]
        [MinLength(3, ErrorMessage = "The Subject field must contain at least 3 characters.")]
        public string Subject { get; set; }
        
        [MaxLength(10, ErrorMessage = "The RoomNumber field may contain at most 10 characters.")]
        [MinLength(1, ErrorMessage = "The RoomNumber field must contain at least 1 characters.")]
        public string RoomNumber { get; set; }
        
        [MaxLength(50, ErrorMessage = "The Lecturer field may contain at most 50 characters.")]
        [MinLength(3, ErrorMessage = "The Lecturer field must contain at least 3 characters.")]
        public string Lecturer { get; set; }

        [Required(AllowEmptyStrings = false)]
        public DayOfWeek DayOfWeek { get; set; }

        [Range(typeof(TimeSpan), "7:00", "20:45",
            ErrorMessage = "The StartTime field must be between {1} and {2}")]
        [Required(AllowEmptyStrings = false)]
        public TimeSpan StartTime { get; set; }

        [Range(typeof(TimeSpan), "7:15", "21:00",
            ErrorMessage = "The EndTime field must be between {1} and {2}")]
        [Required(AllowEmptyStrings = false)]
        public TimeSpan EndTime { get; set; }

        [Required(AllowEmptyStrings = false)]
        public TypeOfClasses TypeOfClasses { get; set; }
    }
}
