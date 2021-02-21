using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Shared.Auth;
using System.Threading.Tasks;

namespace ScheduleGenerator.Server.Repositories
{
    public interface IUsersRepository
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);
        Task<bool> IsEmailTaken(string email);
        Task<User> GetUserById(int userId);
    }
}
