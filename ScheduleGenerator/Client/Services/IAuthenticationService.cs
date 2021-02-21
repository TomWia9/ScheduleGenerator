using ScheduleGenerator.Shared.Auth;
using ScheduleGenerator.Shared.Dto;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScheduleGenerator.Client.Services
{
    public interface IAuthenticationService
    {
        AuthenticateResponse User { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
        Task<HttpResponseMessage> Register(UserForCreationDto user);
        bool IsUserLoggedIn();
    }
}
