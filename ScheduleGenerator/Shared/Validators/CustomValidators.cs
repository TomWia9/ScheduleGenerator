using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ScheduleGenerator.Shared.Validators
{
    public static class CustomValidators
    {
        public static IRuleBuilderInitial<T, string> MatchPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Custom((password, context) =>
            {
                if (!password.Any(char.IsDigit))
                {
                    context.AddFailure("Password must contain at least one number");
                }

                if (!password.Any(char.IsLetter))
                {
                    context.AddFailure("Password must contain at least one letter");
                }

                if (!password.Any(char.IsUpper))
                {
                    context.AddFailure("Password must contain at least one upper letter");
                }

            });
        }
    }
}
