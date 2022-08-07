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
        private readonly UserRegisterCommand _command;
        private readonly IValidator<UserRegisterCommand> _validator;

        public UserRegisterCommandValidatorTest()
        {
            _command = new UserRegisterCommand()
            {
                Name = new NameDto()
                {
                    FirstName = "Test",
                    LastName = "Test"
                },
                Login = new LoginDto() { Login = "test@example.com" },
                Password = new PasswordDto() { Password = "someStrong!Password12" },
                BirthDate = new BirthDateDto() { BirthDate = AppTime.Now().AddYears(-18) }
            };
            _validator = new UserRegisterCommandValidator();
        }

        [Fact]
        public async Task ShouldValidUserRegisterCommandAsync()
        {
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfLoginIsNotEmailAsync()
        {
            _command.Login.Login = "badEmail";

            var result = await _validator.ValidateAsync(_command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfLoginIsNotNullAsync()
        {
            _command.Login.Login = null;

            var result = await _validator.ValidateAsync(_command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfLoginIsEmptyAsync()
        {
            _command.Login.Login = "";
            var result = await _validator.ValidateAsync(_command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfFirstNameIsNullAsync()
        {
            _command.Name.FirstName = null;
            var result = await _validator.ValidateAsync(_command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfFirstNameIsEmptyAsync()
        {
            _command.Name.FirstName = "";

            var result = await _validator.ValidateAsync(_command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfFirstNameIsLessThan3CharactersAsync()
        {
            _command.Name.FirstName = "te";
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfFirstNameIsMoreThan50CharactersAsync()
        {
            _command.Name.FirstName = string.Concat(Enumerable.Repeat("a", 51));
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfLastNameIsNullAsync()
        {
            _command.Name.LastName = null;
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfLastNameIsEmptyAsync()
        {
            _command.Name.LastName = "";
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfLastNameIsLessThan3CharactersAsync()
        {
            _command.Name.LastName = "te";
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfLastNameIsMoreThan50CharactersAsync()
        {
            _command.Name.LastName = string.Concat(Enumerable.Repeat("a", 51));
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfPasswordIsNullAsync()
        {
            _command.Password.Password = null;
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfPasswordIsWeakAsync()
        {
            _command.Password.Password = "test_mark_test2312";
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfPasswordIsEmptyAsync()
        {
            _command.Password.Password = "";
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfPasswordIsLessThan8CharactersAsync()
        {
            _command.Password.Password = string.Concat(Enumerable.Repeat("a", 7));
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfPasswordIsMoreThan50CharactersAsync()
        {
            _command.Password.Password = string.Concat(Enumerable.Repeat("a", 51));
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfBirthDateIsNullAsync()
        {
            _command.BirthDate.BirthDate = DateTime.MinValue;
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfBirthDateIsLessThan12YearsAsync()
        {
            _command.BirthDate.BirthDate = AppTime.Now().AddYears(-11);
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfBirthDateIsMoreThan120YearsAsync()
        {
            _command.BirthDate.BirthDate = AppTime.Now().AddYears(-121);
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotValidUserRegisterCommandIfBirthDateIsMoreThanCurrentDateAsync()
        {
            _command.BirthDate.BirthDate = AppTime.Now();
            var result = await _validator.ValidateAsync(_command);
            result.IsValid.Should().BeFalse();
        }
    }
}
