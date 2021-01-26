using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGenerator.Server.Models;

namespace ScheduleGenerator.Server.Repositories
{
    public interface IUsersRepository
    {
        Task<bool> IsEmailTaken(string email);
        Task<User> GetUserById(int userId);
    }
}
