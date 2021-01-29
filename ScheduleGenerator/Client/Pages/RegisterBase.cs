using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScheduleGenerator.Client.Services;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Pages
{
    public class RegisterBase : ComponentBase
    {
        [Inject]
        protected IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected UserForCreationDto UserForCreation = new();
        protected bool Loading;
        protected string Error;
        protected bool? RegisterFailed;
        protected bool UsernameConflict;

        protected override void OnInitialized()
        {
            // redirect to home if already logged in
            if (AuthenticationService.User != null)
            {
                NavigationManager.NavigateTo("");
            }
        }

        protected async Task HandleValidSubmit()
        {
            Loading = true;
            try
            {
                var response = await AuthenticationService.Register(UserForCreation);

                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    UsernameConflict = true;
                    RegisterFailed = null;
                }

                else if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.Conflict)
                {
                    RegisterFailed = true;
                    UsernameConflict = false;

                }

                else
                {
                    RegisterFailed = false;
                    UsernameConflict = false;
                }

                Loading = false;

            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Loading = false;
                StateHasChanged();
            }
        }
    }
}
