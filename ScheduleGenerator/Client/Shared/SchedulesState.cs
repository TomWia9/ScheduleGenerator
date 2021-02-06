using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Shared
{
    public class SchedulesState
    {
        public IList<ScheduleDto> Schedules = new List<ScheduleDto>();

        public event Action OnScheduleModified;

        public void AddSchedule(ScheduleDto schedule)
        {
            Schedules.Add(schedule);
            NotifyStateChanged();
        }

        public void DeleteSchedule(int id)
        {
            var scheduleToDelete = Schedules.Single(s => s.Id == id);

            Schedules.Remove(scheduleToDelete);

            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnScheduleModified?.Invoke();

    }
}
