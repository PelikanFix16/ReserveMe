using System;
using FluentAssertions;
using SharedKernel.Domain;
using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;
using Xunit;

namespace Domain.Test.ValueObject
{
    public class BirthDateTest
    {
        private readonly string _birth_date_message =
                            "Birth date should be between 12 and 120 year old";

        [Fact]
        public void UserShouldThrowExceptionWhenBeYoungerThan12Years()
        {
            //Arrange
            var young_User = AppTime.Now().AddYears(-11);
            Action act = () => new BirthDate(young_User);
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage(_birth_date_message);
        }

        [Fact]
        public void UserShouldThrowExceptionWhenBeOlderThan120Years()
        {
            var old_user = AppTime.Now().AddYears(-121);

            Action act = () => new BirthDate(old_user);

            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage(_birth_date_message);
        }

        [Fact]
        public void UserShouldCreateWhenBeOlderOrEqualTo12Years()
        {
            var correct_user = AppTime.Now().AddYears(-12);
            var birthDate = new BirthDate(correct_user);
            birthDate.Value.Should().Be(correct_user);
        }

        [Fact]
        public void UserShouldCreateWhenBeOlderOrEqualTo120Years()
        {
            var correct_user = AppTime.Now().AddYears(-120);
            var birthDate = new BirthDate(correct_user);
            birthDate.Value.Should().Be(correct_user);
        }

        [Fact]
        public void UserShouldNotCreateIfBirthDateIsMoreOrEqualCurrentDate()
        {
            var currentDate = DateTimeOffset.Now;

            Action act = () => new BirthDate(currentDate);

            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage(_birth_date_message);
        }
    }
}
