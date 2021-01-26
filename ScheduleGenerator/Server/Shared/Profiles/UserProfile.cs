using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Server.Shared.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserForCreationDto, User>();
        }
    }
}
