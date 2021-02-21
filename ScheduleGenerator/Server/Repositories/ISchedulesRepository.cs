using ScheduleGenerator.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
