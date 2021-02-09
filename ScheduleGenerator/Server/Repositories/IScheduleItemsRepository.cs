using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Shared.Enums;

namespace ScheduleGenerator.Server.Repositories
{
    public interface IScheduleItemsRepository
    {
        Task<IEnumerable<ScheduleItem>> GetScheduleItemsAsync(int scheduleId);
        Task<ScheduleItem> GetScheduleItemAsync(int scheduleId, int scheduleItemId);
        Task<bool> DatesConflictAsync(int scheduleId, WeekDay dayOfWeek, DateTime startTime, DateTime endTime, int? scheduleItemId = null);
        void UpdateScheduleItem(ScheduleItem scheduleItem); 
        Color GetScheduleItemColor(TypeOfClasses typeOfClasses);
    }
}
