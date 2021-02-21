using ScheduleGenerator.Shared.Dto;
using System.Threading.Tasks;

namespace ScheduleGenerator.Client.Services
{
    public interface IUsersService
    {
        Task<UserDto> GetUser(int userId);

    }
}
