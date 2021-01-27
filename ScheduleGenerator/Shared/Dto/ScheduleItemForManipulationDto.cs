using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Shared.Dto
{
    public abstract class ScheduleItemForManipulationDto
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(60, ErrorMessage = "The Subject field may contain at most 60 characters.")]
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

        [Required(AllowEmptyStrings = false)]
        public DateTime StartTime { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public DateTime EndTime { get; set; }
        //public TypeOfSubject TypeOfSubject { get; set; }
    }
}
