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
        private IUserAggregateRootFactory userFactory = new UserAggregateRootFactory();
        private DateTimeOffset BirthDate = AppTime.Now().AddYears(-18);

        private string FirstName = "John";
        private string LastName = "Doe";
        private string Login = "20hubert01@gmail.com";
        private string Password = "testTestow@1!";

        public void User_Aggregate_Facotry_Should_Create_Name_Without_Exception()
        {
            //Arrange
            //Act 
            Action addName = () => userFactory.AddName(FirstName, LastName);
            addName.Should().NotThrow();
        }

        [Fact]
        public void User_Aggregate_Factory_Should_Create_Login_Without_Exception()
        {
            //Arrange     
            //Act
            Action addLogin = () => userFactory.AddLogin(Login);
            addLogin.Should().NotThrow();
        }

        [Fact]
        public void User_Aggregate_Factory_Should_Create_Password_Without_Exception()
        {
            //Arrange         
            //Act
            Action addPassword = () => userFactory.AddPassword(Password);
            //Assert
            addPassword.Should().NotThrow();

        }

        [Fact]
        public void User_Aggregate_Facotry_Should_Create_BirthDate_Without_Exception()
        {
            //Arrange
            //Act
            Action addBirthdate = () => userFactory.AddBirthDate(BirthDate);
            //Assert
            addBirthdate.Should().NotThrow();
        }
        [Fact]
        public void User_Aggregate_Facotry_Should_Create_UserAggregateRoot_Without_Exception()
        {

            IUserAggregateRootFactory _userFactory = new UserAggregateRootFactory();
            _userFactory.AddBirthDate(BirthDate);
            _userFactory.AddPassword(Password);
            _userFactory.AddLogin(Login);
            _userFactory.AddName(FirstName, LastName);
            UserAggregateRoot user = _userFactory.Create();

            user.Id.Should().NotBeNull();
            user.Login!.Value.Should().Be(Login);
            user.Name!.FirstName.Should().Be(FirstName);
            user.Name!.LastName.Should().Be(LastName);
            user.Password!.Value.Should().Be(Password);
            user.BirthDate!.Value.Should().Be(BirthDate);
            user.RegisteredDate.Day.Should().Be(AppTime.Now().Day);

        }
    }
}
