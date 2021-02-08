using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Shared.Dto
{
    /// <summary>
    /// Schedule with Id, Name, DateOfCreation and Has7Days fields
    /// </summary>
    public class ScheduleDto
    {
        /// <summary>
        /// The id of schedule
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of schedule
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The date of schedule creation
        /// </summary>
        public DateTime DateOfCreation { get; set; }
        /// <summary>
        /// Schedule has 7 days flag
        /// </summary>
    }
}
