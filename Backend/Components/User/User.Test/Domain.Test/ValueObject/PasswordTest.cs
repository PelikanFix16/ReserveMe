using System;
using FluentAssertions;
using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;
using Xunit;

namespace Domain.Test.ValueObject
{
    public class PasswordTest
    {
        [Fact]
        public void PasswordShouldThrowExceptionWhenIsToWeak()
        {
            //Arrange
            const string Password = "test";
            //Act
            Action act = () => User.Domain.ValueObjects.Password.Create(Password);
            //Assert
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("Password is to weak");
        }

        [Fact]
        public void PasswordShouldCreateCorrectWhenPasswordIsStrong()
        {
            const string Password = "testT123@";
            var pass = User.Domain.ValueObjects.Password.Create(Password);
            pass.Value.Should().Be(Password);
        }
    }
}
