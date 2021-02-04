using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ScheduleGenerator.Shared.Dto;

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
