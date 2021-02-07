using FluentValidation;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Shared.Validators
{
    public abstract class ScheduleItemValidator<T> : AbstractValidator<T> where T : ScheduleItemForManipulationDto
    {
        protected ScheduleItemValidator()
        {
            RuleFor(i => i.Subject).NotEmpty().Length(3, 80);
            RuleFor(i => i.RoomNumber).Length(1, 10);
            RuleFor(i => i.Lecturer).Length(3, 50);
            RuleFor(i => i.DayOfWeek).NotNull().IsInEnum();
            RuleFor(i => i.StartTime).NotEmpty(); //TODO add custom time validator
            RuleFor(i => i.EndTime).NotEmpty();
            RuleFor(i => i.TypeOfClasses).NotNull().IsInEnum();
        }
    }
}
