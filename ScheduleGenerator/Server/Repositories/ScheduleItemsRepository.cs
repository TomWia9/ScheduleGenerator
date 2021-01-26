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

        public void UpdateScheduleItem(ScheduleItem scheduleItem)
        {
            //no code in this implementation
        }
    }
}
