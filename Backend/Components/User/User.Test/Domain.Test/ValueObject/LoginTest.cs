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
        public void Login_Should_throw_excetpion_if_email_does_to_weak()
        {
            //Arrange
            var email = "test";
            //Act
            Action act = () => Login.Create(email);

            //Assert

            act.Should()
               .Throw<BusinessRuleValidationException>()
               .WithMessage("Login mest be an email");

        }
    }
}