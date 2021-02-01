using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Services
{
    public interface ISchedulesService
    {
        Task<HttpResponseMessage> GetSchedulesAsync();

        Task<HttpResponseMessage> CreateScheduleAsync(int scheduleId, ScheduleForCreationDto schedule);
        Task<HttpResponseMessage> UpdateScheduleAsync(int scheduleId, ScheduleForUpdateDto schedule);
        Task<HttpResponseMessage> DeleteScheduleAsync(int scheduleId);
    }
}
