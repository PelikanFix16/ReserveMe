using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using SharedKernel.Domain;
using User.Application.Commands.UserRegister;
using User.Application.Mapper.Dto;
using Xunit;

namespace Application.Test.Commands.UserRegister
{


    public class UserRegisterCommandValidatorTest
    {
        private UserRegisterCommand command;
        private IValidator<UserRegisterCommand> _validator;

        public UserRegisterCommandValidatorTest()
        {
            command = new UserRegisterCommand()
            {
                Name = new NameDto(){
                    FirstName = "Test",
                    LastName = "Test"
                },
                Login = "test@example.com",
                Password = "someStrong!Password12",
                BirthDate = AppTime.Now().AddYears(-18)
            };
            _validator = new UserRegisterCommandValidator();
        }

        [Fact]
        public async Task Should_valid_user_register_command()
        {
            ValidationResult result = await _validator.ValidateAsync(command);
            result.IsValid.Should().BeTrue();
            

        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_login_is_not_email()
        {
            command.Login = "badEmail";
            

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }
    }
}