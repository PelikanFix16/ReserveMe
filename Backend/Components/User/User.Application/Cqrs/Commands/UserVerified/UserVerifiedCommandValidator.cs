using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace User.Application.Cqrs.Commands.UserVerified
{
    public class UserVerifiedCommandValidator : AbstractValidator<UserVerifiedCommand>
    {
        public UserVerifiedCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .WithMessage("Id is required")
                .NotEmpty()
                .WithMessage("Id is required");
        }
    }
}
