using System.Collections.Generic;

namespace ScheduleGenerator.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
    }
}
