using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScheduleGenerator.Client.Helpers.ExtensionMethods;
using ScheduleGenerator.Client.Services;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Pages
{
    public class RegisterBase : ComponentBase
    {
        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        [Inject] 
        private NavigationManager NavigationManager { get; set; }

        protected readonly UserForCreationDto UserForCreation = new();
        protected bool Loading;
        protected string Error;
        protected bool? RegisterFailed;
        protected bool EmailConflict;

        protected override void OnInitialized()
        {
            // redirect to home if already logged in
            if (AuthenticationService.User != null)
            {
                NavigationManager.NavigateTo("", true);
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
                    EmailConflict = true;
                    RegisterFailed = null;
                }

                else if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.Conflict)
                {
                    RegisterFailed = true;
                    EmailConflict = false;

                }

                else
                {
                    RegisterFailed = false;
                    EmailConflict = false;
                    await AuthenticationService.Login(UserForCreation.Email, UserForCreation.Password);
                    var returnUrl = NavigationManager.QueryString("returnUrl") ?? "/";
                    NavigationManager.NavigateTo(returnUrl, true);
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
