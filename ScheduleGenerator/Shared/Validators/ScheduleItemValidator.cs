using FluentValidation;
using ScheduleGenerator.Shared.Dto;
using System;

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
            RuleFor(i => i.StartTime).NotEmpty();
            RuleFor(i => i.EndTime).NotEmpty().Must((i, endTime) => BeAValidDates(i.StartTime, endTime))
                .WithMessage("The difference between the start and end times should be at least 15 minutes");
            RuleFor(i => i.TypeOfClasses).NotNull().IsInEnum();
        }

        private static bool BeAValidDates(DateTime startTime, DateTime endTime)
        {
            var time = endTime.TimeOfDay - startTime.TimeOfDay;

            return startTime.TimeOfDay < endTime.TimeOfDay && time.TotalMinutes >= 15;
        }
    }
}
