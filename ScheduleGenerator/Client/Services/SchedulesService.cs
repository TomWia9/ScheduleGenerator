using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Services
{
    public class SchedulesService : ISchedulesService
    {
        private readonly IHttpService _httpService;

        public SchedulesService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<HttpResponseMessage> CreateSchedule(int scheduleId, ScheduleForCreationDto schedule)
        {
            return await _httpService.Post($"api/schedules", schedule);

        }

        public async Task<HttpResponseMessage> UpdateSchedule(int scheduleId, ScheduleForUpdateDto schedule)
        {
            return await _httpService.Put($"api/schedules/{scheduleId}", schedule);
        }

        public async Task<HttpResponseMessage> DeleteSchedule(int scheduleId)
        {
            return await _httpService.Delete($"api/schedules/{scheduleId}");
        }
    }
}
