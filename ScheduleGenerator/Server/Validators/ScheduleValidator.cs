using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Server.Validators
{
    public abstract class ScheduleValidator<T> : AbstractValidator<T> where T : ScheduleForManipulationDto
    {
        protected ScheduleValidator()
        {
            RuleFor(s => s.Name).NotEmpty().Length(2, 30);
        }
    }
}
