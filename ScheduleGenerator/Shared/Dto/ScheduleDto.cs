using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Shared.Dto
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public bool Has7Days { get; set; }
        public IEnumerable<ScheduleItemDto> ScheduleItems { get; set; }
    }
}
