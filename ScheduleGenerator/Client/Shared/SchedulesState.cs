using System;
using System.Collections.Generic;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Shared
{
    public class SchedulesState
    {
        public IList<ScheduleDto> Schedules = new List<ScheduleDto>();

        public event Action OnScheduleAdded;

        public void AddSchedule(ScheduleDto schedule)
        {
            Schedules.Add(schedule);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnScheduleAdded?.Invoke();

    }
}
