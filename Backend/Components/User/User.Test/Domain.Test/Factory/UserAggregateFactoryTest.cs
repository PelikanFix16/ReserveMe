using System;
using FluentAssertions;
using SharedKernel.Domain;
using User.Domain.User;
using User.Domain.User.Factory;
using Xunit;

namespace Domain.Test.Factory
{
    public class UserAggregateFactoryTest
    {
        private readonly IUserAggregateRootFactory _userFactory = new UserAggregateRootFactory();
        private readonly DateTimeOffset _birthDate = AppTime.Now().AddYears(-18);
        private readonly string _firstName = "John";
        private readonly string _lastName = "Doe";
        private readonly string _login = "20hubert01@gmail.com";
        private readonly string _password = "testTestow@1!";

        [Fact]
        public void UserAggregateFactoryShouldCreateNameWithoutException()
        {
            Action addName = () => _userFactory.AddName(_firstName, _lastName);
            addName.Should().NotThrow();
        }

        [Fact]
        public void UserAggregateFactoryShouldCreateLoginWithoutException()
        {
            //Arrange
            //Act
            Action addLogin = () => _userFactory.AddLogin(_login);
            addLogin.Should().NotThrow();
        }

        [Fact]
        public void UserAggregateFactoryShouldCreatePasswordWithoutException()
        {
            //Arrange
            //Act
            Action addPassword = () => _userFactory.AddPassword(_password);
            //Assert
            addPassword.Should().NotThrow();
        }

        [Fact]
        public void UserAggregateFactoryShouldCreateBirthDateWithoutException()
        {
            //Arrange
            //Act
            Action addBirthDate = () => _userFactory.AddBirthDate(_birthDate);
            //Assert
            addBirthDate.Should().NotThrow();
        }

        [Fact]
        public void UserAggregateFactoryShouldCreateUserAggregateRootWithoutException()
        {
            var userFactory = new UserAggregateRootFactory();
            userFactory.AddBirthDate(_birthDate);
            userFactory.AddPassword(_password);
            userFactory.AddLogin(_login);
            userFactory.AddName(_firstName, _lastName);
            var user = userFactory.Create();
            //Assert
            user.Id.Should().NotBeNull();
            user.Login!.Value.Should().Be(_login);
            user.Name!.FirstName.Should().Be(_firstName);
            user.Name!.LastName.Should().Be(_lastName);
            user.Password!.Value.Should().Be(_password);
            user.BirthDate!.Value.Should().Be(_birthDate);
            user.RegisteredDate.Day.Should().Be(AppTime.Now().Day);
        }
    }
}
