using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
