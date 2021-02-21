using AutoMapper;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Helpers.Profiles
{
    public class ScheduleItemProfile : Profile
    {
        public ScheduleItemProfile()
        {
            CreateMap<ScheduleItemDto, ScheduleItemForUpdateDto>();
        }
    }
}
