using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            await LoadItems();
        }

        private async Task LoadItems()
        {
            Loading = true;
            var response = await ScheduleItemsService.GetScheduleItemsAsync(Id);
            if (!response.IsSuccessStatusCode)
            {
                LoadFailed = true;
            }
            else
            {
                Items = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleItemDto>>();
            }
            Loading = false;

        }

        protected void AddItem(ScheduleItemDto scheduleItem)
        {
            if (scheduleItem == null) return;
            var scheduleItems = Items.ToList();
            scheduleItems.Add(scheduleItem);
            Items = scheduleItems;
            StateHasChanged();
        }
    }
}
