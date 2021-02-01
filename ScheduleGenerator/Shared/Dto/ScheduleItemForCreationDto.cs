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
            StartTime = TimeSpan.Parse("07:00");
            EndTime = TimeSpan.Parse("08:00");
        }
    }
}
