﻿using System.ComponentModel.DataAnnotations;

namespace ScheduleGenerator.Shared.Dto
{
    public abstract class ScheduleForManipulationDto
    {
        /// <summary>
        /// The name of schedule
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(30, ErrorMessage = "The Name field may contain at most 30 characters.")]
        public string Name { get; set; }
    }
}
