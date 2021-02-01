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
        Task<HttpResponseMessage> CreateScheduleItem(int scheduleId, ScheduleItemForCreationDto scheduleItem);
        Task<HttpResponseMessage> UpdateScheduleItem(int scheduleId, int scheduleItemId, ScheduleItemForUpdateDto scheduleItem);
        Task<HttpResponseMessage> DeleteScheduleItem(int scheduleId, int scheduleItemId);
    }
}
