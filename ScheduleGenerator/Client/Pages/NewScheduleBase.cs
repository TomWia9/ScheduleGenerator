using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScheduleGenerator.Client.Services;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Pages
{
    public class NewScheduleBase : ComponentBase
    {
        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        private ISchedulesService SchedulesService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private AppState AppState { get; set; }
        protected ScheduleForCreationDto ScheduleForCreation { get; set; } = new();
        protected bool Conflict { get; set; }
        protected bool CreationFailed { get; set; }

        protected override void OnInitialized()
        {
            //TODO Implement anonymous page where anonymous user can create schedule without saving it
            //if (!AuthenticationService.IsUserLoggedIn())
            //{
            //    NavigationManager.NavigateTo("schedules/anonymous");
            //}
        }

        protected async Task CreateScheduleAsync()
        {
            var response = await SchedulesService.CreateScheduleAsync(ScheduleForCreation);

            if (response.IsSuccessStatusCode)
            {
                var schedule = await response.Content.ReadFromJsonAsync<ScheduleDto>();
                AppState.AddSchedule(schedule);
                
                NavigationManager.NavigateTo($"schedules/{schedule!.Id}");
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                Conflict = true;
            }
            else
            {
                CreationFailed = true;
            }
        }
    }
}
