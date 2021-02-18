using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleGenerator.Shared.Dto;
using ScheduleGenerator.Shared.Enums;

namespace ScheduleGenerator.Client.Shared
{
    public class ScheduleItemsState
    {
        public Dictionary<WeekDay, List<ScheduleItemDto>> ScheduleItems = new();

        public event Action OnScheduleItemModified;
        public void AddScheduleItem(ScheduleItemDto item)
        {
            ScheduleItems[item.DayOfWeek].Add(item);
            NotifyStateChanged();
        }

        public void UpdateScheduleItem(ScheduleItemDto item)
        {
            var index = ScheduleItems[item.DayOfWeek].FindIndex(i => i.Id == item.Id);

            if (index > -1)
                ScheduleItems[item.DayOfWeek][index] = item;

            NotifyStateChanged();
        }

        public void DeleteScheduleItem(ScheduleItemDto item)
        {
            var scheduleItemToDelete = ScheduleItems[item.DayOfWeek].Single(i => i.Id == item.Id);

            ScheduleItems[item.DayOfWeek].Remove(scheduleItemToDelete);

            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnScheduleItemModified?.Invoke();

    }
}
