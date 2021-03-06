﻿using ScheduleGenerator.Shared.Dto;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ScheduleGenerator.Client.Services
{
    public class UsersService : IUsersService
    {
        private readonly IHttpService _httpService;
        public UsersService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<UserDto> GetUser(int userId)
        {
            var response = await _httpService.Get($"api/users/{userId}");
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
    }
}
