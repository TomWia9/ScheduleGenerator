using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Shared.Dto
{
    /// <summary>
    /// User with Id and Email fields
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The id of user
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The email of user
        /// </summary>
        public string Email { get; set; }
    }
}
