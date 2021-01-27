using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGenerator.Server.Models;

namespace ScheduleGenerator.Server.Repositories
{
    public interface IScheduleItemsRepository
    {
        Task<IEnumerable<ScheduleItem>> GetScheduleItemsAsync(int scheduleId);
        Task<ScheduleItem> GetScheduleItemAsync(int scheduleId, int scheduleItemId);
        //Task<bool> DatesConflictAsync(int scheduleId, int scheduleItemId, DayOfWeek dayOfWeek, DateTime startTime, DateTime endTime)
        void UpdateScheduleItem(ScheduleItem scheduleItem);
    }
}
