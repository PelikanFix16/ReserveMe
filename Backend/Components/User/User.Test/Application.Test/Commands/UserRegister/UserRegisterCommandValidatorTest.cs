using System;
using System.Linq;
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
                Name = new NameDto()
                {
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

        [Fact]
        public async Task Should_not_valid_user_register_command_if_login_is_not_null()
        {
            command.Login = null;

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_login_is_empty()
        {
            command.Login = "";
            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_first_name_is_null()
        {
            command.Name.FirstName = null;
            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_first_name_is_empty()
        {
            command.Name.FirstName = "";

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_first_name_is_less_than_3_characters()
        {

            command.Name.FirstName = "te";

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_first_name_is_more_than_50_characters()
        {

            command.Name.FirstName = string.Concat(Enumerable.Repeat("a", 51));

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_last_name_is_null()
        {
            command.Name.LastName = null;
            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_last_name_is_empty()
        {
            command.Name.LastName = "";

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_last_name_is_less_than_3_characters()
        {

            command.Name.LastName = "te";

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_last_name_is_more_than_50_characters()
        {

            command.Name.LastName = string.Concat(Enumerable.Repeat("a", 51));

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_password_is_null()
        {
            command.Password = null;

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_password_is_empty()
        {
            command.Password = "";

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_password_is_less_than_8_characters()
        {
            command.Password = string.Concat(Enumerable.Repeat("a", 7));

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_password_is_more_than_50_characters()
        {
            command.Password = string.Concat(Enumerable.Repeat("a", 51));

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_birth_date_is_null()
        {
            command.BirthDate = DateTime.MinValue;

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_birth_date_is_less_than_12_years()
        {
            command.BirthDate = AppTime.Now().AddYears(-11);

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_valid_user_register_command_if_birth_date_is_more_than_120_years()
        {
            command.BirthDate = AppTime.Now().AddYears(-121);

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public async Task Should_not_valid_user_register_command_if_birth_date_is_more_than_current_date()
        {
            command.BirthDate = AppTime.Now();

            ValidationResult result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
        }






    }
}