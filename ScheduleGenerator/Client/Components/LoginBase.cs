using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScheduleGenerator.Client.Services;
using ScheduleGenerator.Shared.Auth;

namespace ScheduleGenerator.Client.Components
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
        protected async Task HandleValidSubmit()
        {
            Loading = true;
            try
            {
                await AuthenticationService.Login(AuthenticateRequest.Email, AuthenticateRequest.Password);
                //var returnUrl = NavigationManager.QueryString("returnUrl") ?? "/";
                NavigationManager.NavigateTo("");
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
