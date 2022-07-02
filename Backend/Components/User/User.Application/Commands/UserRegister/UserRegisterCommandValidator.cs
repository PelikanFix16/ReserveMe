using FluentValidation;
using SharedKernel.Domain;

namespace User.Application.Commands.UserRegister
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(c => c.Login)
                .NotNull()
                .WithMessage("Login cannot be null")
                .NotEmpty()
                .WithMessage("Email address is required")
                .EmailAddress()
                .WithMessage("A valid email is required");
            
            RuleFor(c => c.Name.FirstName)
                .NotNull()
                .WithMessage("First name cannot be null")
                .NotEmpty()
                .WithMessage("First name cannot be empty")
                .MinimumLength(3)
                .WithMessage("Minimum Length of First name cannot be less than 3 characters")
                .MaximumLength(50)
                .WithMessage("Maximum Length of First name cannot be more than 50 characters");
            RuleFor(c => c.Name.LastName)
                .NotNull()
                .WithMessage("First name cannot be null")
                .NotEmpty()
                .WithMessage("First name cannot be empty")
                .MinimumLength(3)
                .WithMessage("Minimum Length of Last name cannot be less than 3 characters")
                .MaximumLength(50)
                .WithMessage("Maximum Length of Last name cannot be more than 50 characters");

            RuleFor(c => c.Password)
                .NotNull()
                .WithMessage("Password cannot be null")
                .NotEmpty()
                .WithMessage("Password cannot be empty")
                .MinimumLength(8)
                .WithMessage("Minimum Length of Password cannot be less than 8 characters")
                .MaximumLength(50)
                .WithMessage("Maximum Length of Password cannot be more than 50 characters");

            RuleFor(c => c.BirthDate)
                .NotNull()
                .WithMessage("Birth date cannot be null")
                .NotEmpty()
                .WithMessage("Birth date cannot be empty")
                .LessThan(AppTime.Now().AddYears(-11))
                .WithMessage("Birth date must be greater than or equal to 12 years")
                .GreaterThan(AppTime.Now().AddYears(-120))
                .WithMessage("Birth Date must be less than or equal to 120 years");
                // .LessThan(AppTime.Now())
                // .WithMessage("Birth Date must be lower than current date");

                


        }
    }
}