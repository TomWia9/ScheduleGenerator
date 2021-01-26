using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGenerator.Server.Models;
using ScheduleGenerator.Shared.Auth;

namespace ScheduleGenerator.Server.Repositories
{
    public interface IUsersRepository
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);
        Task<bool> IsEmailTaken(string email);
        Task<User> GetUserById(int userId);
    }
}
