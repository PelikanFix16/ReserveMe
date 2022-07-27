using System;
using FluentAssertions;
using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;
using Xunit;

namespace Domain.Test.ValueObject
{
    public class LoginTest
    {
        [Fact]
        public void LoginShouldThrowExceptionIfLoginIsNotEmail()
        {
            //Arrange
            const string Email = "test";
            //Act
            Action act = () => Login.Create(Email);
            //Assert
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("Login must be an email");
        }

        [Fact]
        public void Login_Should_Create_if_login_is_valid()
        {
            //arrange
            var email = "20hubert01@gmail.com";

            //Act
            var login = Login.Create(email);

            //Assert

            login.Value.Should().Be(email);

        }
    }
}
