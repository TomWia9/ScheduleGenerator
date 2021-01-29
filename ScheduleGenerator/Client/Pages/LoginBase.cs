using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScheduleGenerator.Client.Helpers.ExtensionMethods;
using ScheduleGenerator.Client.Services;
using ScheduleGenerator.Shared.Auth;

namespace ScheduleGenerator.Client.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IAuthenticationService AuthenticationService { get; set; }

        protected AuthenticateRequest AuthenticateRequest = new();
        protected bool Loading;
        protected string Error;

        protected override void OnInitialized()
        {
            // redirect to home if already logged in
            if (AuthenticationService.IsUserLoggedIn())
            {
                NavigationManager.NavigateTo("");
            }
        }
        protected async Task HandleValidSubmit()
        {
            Loading = true;
            try
            {
                await AuthenticationService.Login(AuthenticateRequest.Email, AuthenticateRequest.Password);
                var returnUrl = NavigationManager.QueryString("returnUrl") ?? "/";
                NavigationManager.NavigateTo(returnUrl);
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
