using System.Threading.Tasks;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Client.Services
{
    public interface IUsersService
    {
        Task<UserDto> GetUser(int userId);

    }
}
