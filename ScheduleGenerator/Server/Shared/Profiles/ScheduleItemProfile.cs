using AutoMapper;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Server.Shared.Profiles
{
    public class ScheduleItemProfile : Profile
    {
        public ScheduleItemProfile()
        {
            CreateMap<ScheduleItem, ScheduleItemDto>();
            CreateMap<ScheduleItemForCreationDto, ScheduleItem>();
            CreateMap<ScheduleItemForUpdateDto, ScheduleItem>();
            CreateMap<ScheduleItem, ScheduleItemForUpdateDto>();
        }
    }
}
