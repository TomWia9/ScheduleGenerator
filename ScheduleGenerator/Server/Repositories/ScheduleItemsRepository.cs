using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScheduleGenerator.Server.Models;

namespace ScheduleGenerator.Server.Repositories
{
    public class ScheduleItemsRepository : IScheduleItemsRepository
    {
        private readonly ScheduleGeneratorContext _context;

        public ScheduleItemsRepository(ScheduleGeneratorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ScheduleItem>> GetScheduleItemsAsync(int scheduleId)
        {
            return await _context.ScheduleItems.Where(i => i.ScheduleId == scheduleId).ToListAsync();
        }

        public async Task<ScheduleItem> GetScheduleItemAsync(int scheduleId, int scheduleItemId)
        {
            return await _context.ScheduleItems.FirstOrDefaultAsync(i => i.Id == scheduleItemId && i.ScheduleId == scheduleId);
        }

        //scheduleItemId is null by default because when new scheduleItem is created then id is unknown yet
        public async Task<bool> DatesConflictAsync(int scheduleId, DayOfWeek dayOfWeek, DateTime startTime, DateTime endTime, int? scheduleItemId = null)
        {
            if (!await _context.ScheduleItems.Where(i => i.ScheduleId == scheduleId && i.Id != scheduleItemId && i.DayOfWeek == dayOfWeek)
                .AnyAsync(i => !(endTime.TimeOfDay < i.StartTime.TimeOfDay || startTime.TimeOfDay > i.EndTime.TimeOfDay)))
                return false;

            return true;
        }

        public void UpdateScheduleItem(ScheduleItem scheduleItem)
        {
            //no code in this implementation
        }

        public bool AreDatesCorrect(DateTime start, DateTime end)
        {
            return start.TimeOfDay < end.TimeOfDay;
        }
    }
}
