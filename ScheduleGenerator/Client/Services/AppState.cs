using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Services
{
    public class AppState
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
