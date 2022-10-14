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
            Action act = () => new User.Domain.ValueObjects.Password(Password);
            //Assert
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("Password is to weak");
        }

        [Fact]
        public void PasswordShouldCreateCorrectWhenPasswordIsStrong()
        {
            const string Password = "testT123@";
            var pass = new User.Domain.ValueObjects.Password(Password);
            pass.Value.Should().Be(Password);
        }
    }
}
