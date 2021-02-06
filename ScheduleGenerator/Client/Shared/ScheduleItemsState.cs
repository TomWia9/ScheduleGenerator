using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Shared
{
    public class ScheduleItemsState
    {
        public List<ScheduleItemDto> ScheduleItems = new();

        public event Action OnScheduleItemModified;
        public void AddScheduleItem(ScheduleItemDto item)
        {
            ScheduleItems.Add(item);
            NotifyStateChanged();
        }

        public void UpdateScheduleItem(ScheduleItemDto item)
        {
            var index = ScheduleItems.FindIndex(i => i.Id == item.Id);

            if (index > -1)
                ScheduleItems[index] = item;

            NotifyStateChanged();
        }

        public void DeleteScheduleItem(int id)
        {
            var scheduleItemToDelete = ScheduleItems.Single(i => i.Id == id);

            ScheduleItems.Remove(scheduleItemToDelete);

            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnScheduleItemModified?.Invoke();

    }
}
