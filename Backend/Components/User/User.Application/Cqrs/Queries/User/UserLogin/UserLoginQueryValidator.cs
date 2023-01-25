using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace User.Application.Cqrs.Queries.User.UserLogin
{
    public class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(c => c.Login.Login)
                .NotNull()
                .WithMessage("Login cannot be null")
                .NotEmpty()
                .WithMessage("Email address is required")
                .EmailAddress()
                .WithMessage("A valid email is required");

            RuleFor(c => c.Password.Password)
                .NotNull()
                .WithMessage("Password cannot be null")
                .NotEmpty()
                .WithMessage("Password cannot be empty")
                .MinimumLength(8)
                .WithMessage("Minimum Length of Password cannot be less than 8 characters")
                .MaximumLength(50)
                .WithMessage("Maximum Length of Password cannot be more than 50 characters")
                .Matches("[0-9]+")
                .WithMessage("Password must contain at least one number")
                .Matches("[A-Z]+")
                .WithMessage("Password must contain at least one uppercase letter");
        }
    }
}
