using System;
using FluentAssertions;
using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;
using Xunit;

namespace Domain.Test.ValueObject
{
    public class NameTest
    {
        private string exception_message = "Name must be valid";

        [Fact]
        public void Name_should_throw_exception_when_contains_letters_and_numbers()
        {
            var firstName = "Tes1t12";
            var LastName = "test2";

            Action act = () => Name.Create(firstName, LastName);

            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage(exception_message);

        }

        [Fact]
        public void Name_Should_throw_exception_when_name_or_LastName_starts_with_lowercase_letters()
        {
            var firstName = "test";
            var lastName = "test";

            Action act = () => Name.Create(firstName, lastName);

            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage(exception_message);
        }

        [Fact]
        public void Name_should_be_correct_when_starts_with_uppercase_letters_and_witchout_numbers()
        {
            var firstName = "Test";
            var lastName = "Test";

            Name name = Name.Create(firstName, lastName);

            name.FirstName.Should().Be(firstName);
            name.LastName.Should().Be(lastName);
            
        }
    }
}