using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScheduleGenerator.Shared.Auth;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IHttpService _httpService;
        private readonly ILocalStorageService _localStorageService;

        public AuthenticateResponse User { get; private set; }

        public AuthenticationService(
            NavigationManager navigationManager,
            ILocalStorageService localStorageService, IHttpService httpService)
        {
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _httpService = httpService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<AuthenticateResponse>("user");
        }

        public async Task Login(string email, string password)
        {
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = email,
                Password = password
            };

            var response = await _httpService.Post("api/users/authenticate", authenticateRequest);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("authentication/logout");
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

                throw new Exception(error["message"]);

            }

            User = await response.Content.ReadFromJsonAsync<AuthenticateResponse>();

            await _localStorageService.SetItem("user", User);

        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("");
        }

        public async Task<HttpResponseMessage> Register(UserForCreationDto user)
        {
            return await _httpService.Post($"api/users", user);
        }

        public bool IsUserLoggedIn()
        {
            return User != null;
        }
    }
}
