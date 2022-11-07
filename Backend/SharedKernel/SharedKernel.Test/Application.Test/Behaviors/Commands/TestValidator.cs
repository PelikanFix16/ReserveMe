using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Test.Behaviors.Commands
{
    public class TestValidator : AbstractValidator<TestCommand>
    {
        public TestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters");
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login is required")
                .MaximumLength(10)
                .WithMessage("Login must be at most 10 characters");

        }
    }
}
