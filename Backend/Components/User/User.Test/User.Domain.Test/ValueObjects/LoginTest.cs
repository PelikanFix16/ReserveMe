using System;
using FluentAssertions;
using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;
using Xunit;

namespace User.Domain.Test.ValueObjects
{
    public class LoginTest
    {
        [Fact]
        public void Login_Should_throw_domain_exception_if_is_not_valid_email()
        {
            //Arrange
            string email = "some_not_valid_email";

            Action act = () => Login.Create(email);

            act.Should().Throw<BusinessRuleValidationException>().WithMessage("Login mest be an email");


        }

        [Fact]
        public void Login_should_create_if_login_is_valid_email()
        {
            string email = "20hubert01@gmail.com";
            
            Login login = Login.Create(email);

            login.Value.Should().Be(email);

        }
    }
}