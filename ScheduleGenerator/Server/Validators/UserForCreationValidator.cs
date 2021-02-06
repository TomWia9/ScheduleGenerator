using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ScheduleGenerator.Shared.Dto;

namespace ScheduleGenerator.Server.Validators
{
    public class UserForCreationValidator : AbstractValidator<UserForCreationDto>
    {
        public UserForCreationValidator()
        {
            RuleFor(u => u.Email).NotEmpty().Length(3, 20).EmailAddress();

            RuleFor(u => u.Password).NotEmpty().Length(5, 20);

            RuleFor(u => u.ConfirmPassword).NotEmpty().Length(5, 20).Equal(u => u.Password)
                .When(u => !string.IsNullOrWhiteSpace(u.Password)).WithMessage("Passwords do not match");
        }
    }
}
