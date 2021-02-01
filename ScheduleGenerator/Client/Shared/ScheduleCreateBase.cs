using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScheduleGenerator.Client.Shared.Modals;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Shared
{
    public class ScheduleCreateBase : ComponentBase
    {
        protected ScheduleForCreationDto ScheduleForCreation = new();
        protected CreateScheduleItemModal CreateScheduleItemModal;

        protected bool Loading;


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
