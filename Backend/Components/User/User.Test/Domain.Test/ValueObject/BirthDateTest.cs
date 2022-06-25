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
        private string birth_date_message = 
                            "Birth date should be between 12 and 120 year old";
        [Fact]
        public void User_Should_throw_exception_when_be_younger_than_12_years()
        {
            //Arrange
            DateTimeOffset Young_User = AppTime.Now().AddYears(11);


            Action act = () => BirthDate.Create(Young_User);

            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage(birth_date_message);

        }

        [Fact]
        public void User_Should_throw_exception_when_be_older_than_120_years()
        {
            DateTimeOffset Old_user = AppTime.Now().AddYears(121);

            Action act = () => BirthDate.Create(Old_user);
            
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage(birth_date_message);
        }

        [Fact]
        public void User_should_create_when_be_older_or_equal_to_12_years()
        {
            DateTimeOffset correct_user = AppTime.Now().AddYears(12);

            BirthDate birthdate = BirthDate.Create(correct_user);

            birthdate.Value.Should().Be(correct_user);


        }

        [Fact]
        public void User_should_create_when_be_older_or_equal_to_120_years()
        {
                DateTimeOffset correct_user = AppTime.Now().AddYears(120);
                
                BirthDate birthdate = BirthDate.Create(correct_user);

                birthdate.Value.Should().Be(correct_user);
        }
    }
}