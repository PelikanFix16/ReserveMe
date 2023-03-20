using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Application.Cqrs.Commands.Employee.EmployeeRegister
{
    public class EmployeeRegisterCommandValidator : AbstractValidator<EmployeeRegisterCommand>
    {
        public EmployeeRegisterCommandValidator()
        {
            RuleFor(c => c.Email.Email)
                .NotNull()
                .WithMessage("Employee Email cannot be null")
                .NotEmpty()
                .WithMessage("Employee Email cannot be empty")
                .EmailAddress()
                .WithMessage("A valid email is required");
            RuleFor(c => c.Privileges)
                .IsInEnum()
                .WithMessage("Should be valid enum");
            RuleFor(c => c.ManagerId)
                .NotNull()
                .WithMessage("Manager id cannot be null")
                .NotEmpty()
                .WithMessage("Manager id cannot be empty");

        }
    }
}