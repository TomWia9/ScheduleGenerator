using ScheduleGenerator.Shared.Dto;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScheduleGenerator.Client.Services
{
    public interface ISchedulesService
    {
        Task<HttpResponseMessage> GetSchedulesAsync();

        Task<HttpResponseMessage> CreateScheduleAsync(ScheduleForCreationDto schedule);
        Task<HttpResponseMessage> UpdateScheduleAsync(int scheduleId, ScheduleForUpdateDto schedule);
        Task<HttpResponseMessage> DeleteScheduleAsync(int scheduleId);
    }
}
