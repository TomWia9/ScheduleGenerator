using ScheduleGenerator.Shared.Enums;
using System;

namespace ScheduleGenerator.Shared.Dto
{
    public class ScheduleItemForCreationDto : ScheduleItemForManipulationDto
    {
        public ScheduleItemForCreationDto()
        {
            StartTime = DateTime.Parse("2015-05-16T07:00:00");
            EndTime = DateTime.Parse("2015-05-16T08:00:00");
            DayOfWeek = WeekDay.Monday;
        }
    }
}
