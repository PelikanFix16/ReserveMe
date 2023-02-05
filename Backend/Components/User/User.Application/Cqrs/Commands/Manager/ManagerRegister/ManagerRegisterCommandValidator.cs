using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Application.Cqrs.Commands.Manager.ManagerRegister
{
    public class ManagerRegisterCommandValidator : AbstractValidator<ManagerRegisterCommand>
    {
        public ManagerRegisterCommandValidator() =>
                RuleFor(c => c.ManagerEmail.Email)
                .NotNull()
                .WithMessage("Manager Email cannot be null")
                .NotEmpty()
                .WithMessage("Manager Email is required")
                .EmailAddress()
                .WithMessage("A valid email is required");
    }
}