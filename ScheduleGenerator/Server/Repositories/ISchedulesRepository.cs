using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGenerator.Server.Models;

namespace ScheduleGenerator.Server.Repositories
{
    public interface ISchedulesRepository
    {
        Task<Schedule> GetScheduleAsync(int userId, int scheduleId);
        Task<IEnumerable<Schedule>> GetSchedulesAsync(int userId);
        Task<bool> ScheduleExistsAsync(int userId, int scheduleId);
        Task<bool> ScheduleExistsAsync(int userId, string name);
        void UpdateSchedule(Schedule schedule);

    }
}
