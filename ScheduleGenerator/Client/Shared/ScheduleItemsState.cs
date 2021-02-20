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
          
            ScheduleItems[item.DayOfWeek].Sort((x, y) => TimeSpan.Compare(x.StartTime.TimeOfDay, y.StartTime.TimeOfDay));
            
            NotifyStateChanged();
        }

        public void UpdateScheduleItem(ScheduleItemDto item, WeekDay oldWeekDay)
        {
            if (item.DayOfWeek == oldWeekDay)
            {
                var index = ScheduleItems[item.DayOfWeek].FindIndex(i => i.Id == item.Id);

                if (index > -1)
                    ScheduleItems[item.DayOfWeek][index] = item;
            }
            else
            {
                var index = ScheduleItems[oldWeekDay].FindIndex(i => i.Id == item.Id);

                if (index > -1)
                {
                    ScheduleItems[oldWeekDay].RemoveAt(index);
                    ScheduleItems[item.DayOfWeek].Add(item);
                    ScheduleItems[oldWeekDay].Sort((x, y) => TimeSpan.Compare(x.StartTime.TimeOfDay, y.StartTime.TimeOfDay));
                }
            }

            ScheduleItems[item.DayOfWeek].Sort((x, y) => TimeSpan.Compare(x.StartTime.TimeOfDay, y.StartTime.TimeOfDay));

            NotifyStateChanged();
        }

        public void DeleteScheduleItem(ScheduleItemDto item)
        {
            var scheduleItemToDelete = ScheduleItems[item.DayOfWeek].Single(i => i.Id == item.Id);

            ScheduleItems[item.DayOfWeek].Remove(scheduleItemToDelete);

            NotifyStateChanged();
        }

        public void Clear()
        {
            ScheduleItems = new Dictionary<WeekDay, List<ScheduleItemDto>>();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnScheduleItemModified?.Invoke();

    }
}
