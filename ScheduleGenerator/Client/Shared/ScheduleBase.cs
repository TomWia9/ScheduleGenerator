using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScheduleGenerator.Client.Services;
using ScheduleGenerator.Client.Shared.Modals;
using ScheduleGenerator.Shared.Dto;
using ScheduleGenerator.Shared.Enums;

namespace ScheduleGenerator.Client.Shared
{
    public class ScheduleBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IScheduleItemsService ScheduleItemsService { get; set; }
        
        protected ScheduleForCreationDto ScheduleForCreation = new();
        protected CreateScheduleItemModal CreateScheduleItemModal;

        protected IEnumerable<ScheduleItemDto> Items = new List<ScheduleItemDto>();
        
        protected bool Loading;
        protected bool LoadFailed;

        protected override async Task OnParametersSetAsync()
        {
            var response = await ScheduleItemsService.GetScheduleItemsAsync(Id);
            if (!response.IsSuccessStatusCode)
            {
                LoadFailed = true;
            }
            else
            {
                Items = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleItemDto>>();
            }
        }

        protected async Task HandleValidSubmit()
        {
            Loading = true;
            try
            {
                Console.WriteLine("Schedule created");
                Loading = false;
            }
            catch (Exception ex)
            {
              
            }
        }
    }
}
