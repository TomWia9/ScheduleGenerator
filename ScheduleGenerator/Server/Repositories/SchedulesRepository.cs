using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScheduleGenerator.Server.Models;

namespace ScheduleGenerator.Server.Repositories
{
    public class SchedulesRepository : ISchedulesRepository
    {
        private readonly ScheduleGeneratorContext _context;

        public SchedulesRepository(ScheduleGeneratorContext context)
        {
            _context = context;
        }

        public async Task<Schedule> GetScheduleAsync(int userId, int scheduleId)
        {
            return await _context.Schedules.FirstOrDefaultAsync(s => s.UserId == userId && s.Id == scheduleId);
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesAsync(int userId)
        {
            return await _context.Schedules.Where(s => s.UserId == userId).ToListAsync();
        }

        public async Task<bool> ScheduleExistsAsync(int userId, int scheduleId)
        {
            return await _context.Schedules.AnyAsync(s => s.UserId == userId && s.Id == scheduleId);
        }

        public async Task<bool> ScheduleExistsAsync(int userId, string name)
        {
            return await _context.Schedules.AnyAsync(s => s.UserId == userId && s.Name == name);
        }

        public void UpdateSchedule(Schedule schedule)
        {
            //no code in this implementation
        }
    }
}
