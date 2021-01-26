using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Shared.Dto
{
    public abstract class ScheduleItemForManipulationDto
    {
        public string Subject { get; set; }
        public string RoomNumber { get; set; }
        public string Lecturer { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public TypeOfSubject TypeOfSubject { get; set; }
    }
}
