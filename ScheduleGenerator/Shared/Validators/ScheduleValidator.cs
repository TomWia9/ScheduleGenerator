using FluentValidation;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Shared.Validators
{
    public abstract class ScheduleValidator<T> : AbstractValidator<T> where T : ScheduleForManipulationDto
    {
        protected ScheduleValidator()
        {
            RuleFor(s => s.Name).NotEmpty().Length(2, 30);
        }
    }
}
