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
        public void Password_Should_Throw_exception_when_is_to_weak()
        {
            //Arrange
            var password = "test";
            //Act
            Action act = () => Password.Create(password);
            //Assert

            act.Should()
               .Throw<BusinessRuleValidationException>()
               .WithMessage("Password is to weak");

        }

        [Fact]
        public void Password_Should_Create_correct_when_password_is_strong(){
            var password = "testT123@";

            var pass = Password.Create(password);

            pass.Value.Should().Be(password);
            

        }
    }
}