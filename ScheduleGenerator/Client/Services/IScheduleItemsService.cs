using ScheduleGenerator.Shared.Dto;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScheduleGenerator.Client.Services
{
    public interface IScheduleItemsService
    {
        Task<HttpResponseMessage> GetScheduleItemsAsync(int scheduleId);
        Task<HttpResponseMessage> GetScheduleItemAsync(int scheduleId, int scheduleItemId);
        Task<HttpResponseMessage> CreateScheduleItemAsync(int scheduleId, ScheduleItemForCreationDto scheduleItem);
        Task<HttpResponseMessage> UpdateScheduleItemAsync(int scheduleId, int scheduleItemId, ScheduleItemForUpdateDto scheduleItem);
        Task<HttpResponseMessage> DeleteScheduleItemAsync(int scheduleId, int scheduleItemId);
    }
}
