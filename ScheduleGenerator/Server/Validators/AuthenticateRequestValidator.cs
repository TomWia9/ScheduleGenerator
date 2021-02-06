using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ScheduleGenerator.Shared.Auth;

namespace ScheduleGenerator.Server.Validators
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(a => a.Email).NotEmpty().Length(3, 20).EmailAddress();
            RuleFor(a => a.Password).NotEmpty().Length(5, 20);
        }
    }
}
