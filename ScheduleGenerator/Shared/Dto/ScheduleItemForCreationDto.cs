using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Shared.Dto
{
    public class ScheduleItemForCreationDto : ScheduleItemForManipulationDto
    {
        public ScheduleItemForCreationDto()
        {
            StartTime = DateTime.Parse("2015-05-16T07:00:00");
            EndTime = DateTime.Parse("2015-05-16T08:00:00");
            DayOfWeek = DayOfWeek.Monday;
        }
    }
}
