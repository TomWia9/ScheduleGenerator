using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Server.Shared.Profiles
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<ScheduleForCreationDto, Schedule>();
            CreateMap<ScheduleForUpdateDto, Schedule>();
            CreateMap<Schedule, ScheduleForUpdateDto>();
        }
    }
}
