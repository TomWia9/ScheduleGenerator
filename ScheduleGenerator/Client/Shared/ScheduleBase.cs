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
    public class ScheduleBase : ComponentBase, IDisposable
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IScheduleItemsService ScheduleItemsService { get; set; }

        [Inject]
        protected ScheduleItemsState ScheduleItemsState { get; set; }

        //protected ScheduleForCreationDto ScheduleForCreation = new();
        protected CreateScheduleItemModal CreateScheduleItemModal;

        protected bool Loading;
        protected bool LoadFailed;

        protected override async Task OnParametersSetAsync()
        {
            await LoadItems();
            ScheduleItemsState.OnScheduleItemModified += StateHasChanged;
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
                var items = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleItemDto>>();

                ScheduleItemsState.ScheduleItems = items!.ToList();
            }
            Loading = false;

        }

        public void Dispose()
        {
            ScheduleItemsState.OnScheduleItemModified -= StateHasChanged;
        }
    }
}
