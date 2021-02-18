using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ScheduleGenerator.Client.Helpers.ExtensionMethods;
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
        private IJSRuntime Js { get; set; }

        [Inject]
        private IScheduleItemsService ScheduleItemsService { get; set; }

        [Inject]
        private ISchedulesService SchedulesService { get; set; }

        [Inject]
        protected SchedulesState SchedulesState { get; set; }

        [Inject]
        protected ScheduleItemsState ScheduleItemsState { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected CreateScheduleItemModal CreateScheduleItemModal;

        protected bool Loading;
        protected bool LoadFailed;
        protected bool DeleteFailed;
        protected bool ActivatePdfStyle;

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

                ScheduleItemsState.ScheduleItems[WeekDay.Monday] = items!.Where(i => i.DayOfWeek == WeekDay.Monday).ToList();
                ScheduleItemsState.ScheduleItems[WeekDay.Tuesday] = items!.Where(i => i.DayOfWeek == WeekDay.Tuesday).ToList();
                ScheduleItemsState.ScheduleItems[WeekDay.Wednesday] = items!.Where(i => i.DayOfWeek == WeekDay.Wednesday).ToList();
                ScheduleItemsState.ScheduleItems[WeekDay.Thursday] = items!.Where(i => i.DayOfWeek == WeekDay.Thursday).ToList();
                ScheduleItemsState.ScheduleItems[WeekDay.Friday] = items!.Where(i => i.DayOfWeek == WeekDay.Friday).ToList();
            }
            Loading = false;

        }

        protected async Task DeleteScheduleAsync()
        {
            if (await Js.Confirm($"Remove this schedule?"))
            {
                var response = await SchedulesService.DeleteScheduleAsync(Id);

                if (response.IsSuccessStatusCode)
                {
                    SchedulesState.DeleteSchedule(Id);
                    NavigationManager.NavigateTo("");
                }
                else
                {
                    DeleteFailed = true;
                }
            }
        }

        protected async Task DownloadSchedule()
        {
            var scheduleName = SchedulesState.Schedules.FirstOrDefault(s => s.Id == Id)?.Name;

            ActivatePdfStyle = true;
            await Js.InvokeVoidAsync("generatePdf", scheduleName!);
            await Task.Delay(1);
            ActivatePdfStyle = false;
        }

        public void Dispose()
        {
            ScheduleItemsState.OnScheduleItemModified -= StateHasChanged;
        }
    }
}
