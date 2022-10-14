using System;
using FluentAssertions;
using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;
using Xunit;

namespace Domain.Test.ValueObject
{
    public class NameTest
    {
        private readonly string _exception_message = "Name must be valid";

        [Fact]
        public void NameShouldThrowExceptionWhenContainsLettersAndNumbers()
        {
            const string FirstName = "Tes1t12";
            const string LastName = "test2";
            Action act = () => new Name(FirstName, LastName);
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage(_exception_message);
        }

        [Fact]
        public void NameShouldThrowExceptionWhenNameOrLastNameStartsWithLowercaseLetters()
        {
            const string FirstName = "test";
            const string LastName = "test";
            Action act = () => new Name(FirstName, LastName);
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage(_exception_message);
        }

        [Fact]
        public void NameShouldBeCorrectWhenStartsWithUppercaseLettersAndWithoutNumbers()
        {
            const string FirstName = "Test";
            const string LastName = "Test";
            var name = new Name(FirstName, LastName);
            name.FirstName.Should().Be(FirstName);
            name.LastName.Should().Be(LastName);
        }
    }
}
