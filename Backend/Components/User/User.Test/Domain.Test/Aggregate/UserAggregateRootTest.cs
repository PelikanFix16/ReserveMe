using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using SharedKernel.Domain;
using SharedKernel.Domain.BusinessRule;
using SharedKernel.Domain.Event;
using User.Domain.User;
using User.Domain.User.Events;
using User.Domain.User.Factory;
using User.Domain.ValueObjects;
using Xunit;

namespace Domain.Test.Aggregate
{
    public class UserAggregateRootTest
    {
        private readonly string _login = "example@mail.com";
        private readonly string _password = "Test12@1!";
        private readonly string _firstName = "Test";
        private readonly string _lastName = "Test";
        private readonly DateTimeOffset _birthDate = AppTime.Now().AddYears(-18);
        private readonly UserAggregateRootFactory _userFactory;

        public UserAggregateRootTest()
        {
            _userFactory = new UserAggregateRootFactory();
            _userFactory.AddBirthDate(_birthDate)
                .AddLogin(_login)
                .AddName(_firstName, _lastName)
                .AddPassword(_password);
        }

        [Fact]
        public void Should_Create_User_with_event_UserRegisteredEvent()
        {
            var userAggregateRoot = _userFactory.Create();
            IList<DomainEvent> domainEvents = userAggregateRoot.GetUncommittedChanges().ToList();
            domainEvents.Count.Should().Be(1);
            domainEvents[0].Version.Should().Be(0);
            userAggregateRoot.Login!.Value.Should().Be(_login);
            userAggregateRoot.Name!.FirstName.Should().Be(_firstName);
            userAggregateRoot.Name!.LastName.Should().Be(_lastName);
            userAggregateRoot.Password!.Value.Should().Be(_password);
        }

        [Fact]
        public void ShouldConfirmNewUserWithEventUserRegistrationConfirmedEvent()
        {
            var userAggregateRoot = _userFactory.Create();
            userAggregateRoot.Confirm();
            IList<DomainEvent> domainEvents = userAggregateRoot.GetUncommittedChanges().ToList();
            domainEvents.Count.Should().Be(2);
            domainEvents[1].Version.Should().Be(1);
            userAggregateRoot.Status.Should().Be(UserStatus.Activated);
        }

        [Fact]
        public void ShouldThrowExceptionWhenConfirmUserSecondTime()
        {
            var userAggregateRoot = _userFactory.Create();
            userAggregateRoot.Confirm();
            Action act = () => userAggregateRoot.Confirm();
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User Registration cannot be confirmed more than once");
        }

        [Fact]
        public void ShouldChangePasswordWhenIsConfirmedAndShouldCreateEventUserChangedPasswordEvent()
        {
            var userAggregateRoot = _userFactory.Create();
            const string NewPass = "Test12!test";
            var pass = new Password(NewPass);
            userAggregateRoot.Confirm();
            userAggregateRoot.ChangePassword(pass);
            IList<DomainEvent> events = userAggregateRoot.GetUncommittedChanges().ToList();
            events.Count.Should().Be(3);
            events[2].Version.Should().Be(2);
            userAggregateRoot.Password!.Value.Should().Be(NewPass);
        }

        [Fact]
        public void ShouldThrowExceptionWhenToTryChangePasswordWithoutConfirmation()
        {
            var userAggregateRoot = _userFactory.Create();
            const string NewPass = "Test12!test";
            var pass = new Password(NewPass);
            Action act = () => userAggregateRoot.ChangePassword(pass);
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User cannot be modified without confirmation");
        }

        [Fact]
        public void ShouldThrowExceptionWhenUserTryChangePasswordUsingSamePassword()
        {
            var userAggregateRoot = _userFactory.Create();
            var pass = new Password(_password);
            userAggregateRoot.Confirm();
            Action act = () => userAggregateRoot.ChangePassword(pass);
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User cannot change the same password to same value");
        }

        [Fact]
        public void ShouldChangeLoginWhenIsConfirmedAndShouldCreateEventUserChangedLoginEvent()
        {
            var userAggregateRoot = _userFactory.Create();
            const string NewLogin = "test@gmail.com";
            var login = new Login(NewLogin);
            userAggregateRoot.Confirm();
            userAggregateRoot.ChangeLogin(login);
            IList<DomainEvent> domainEvents = userAggregateRoot.GetUncommittedChanges().ToList();
            domainEvents.Count.Should().Be(3);
            domainEvents[2].Version.Should().Be(2);
            userAggregateRoot.Login!.Value.Should().Be(NewLogin);
        }

        [Fact]
        public void ShouldThrowExceptionWhenToTryChangeLoginWithoutConfirmation()
        {
            var userAggregateRoot = _userFactory.Create();
            const string NewLogin = "test@gmail.com";
            var login = new Login(NewLogin);
            Action act = () => userAggregateRoot.ChangeLogin(login);
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User cannot be modified without confirmation");
        }

        [Fact]
        public void ShouldThrowExceptionWhenUserTryChangeLoginUsingSameLogin()
        {
            var userAggregateRoot = _userFactory.Create();
            var login = new Login(_login);
            userAggregateRoot.Confirm();
            Action act = () => userAggregateRoot.ChangeLogin(login);
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User Cannot change login to the same login");
        }

        [Fact]
        public void ShouldChangeNameWhenIsConfirmedAndShouldCreateEventUserChangedNameEvent()
        {
            var userAggregateRoot = _userFactory.Create();
            const string FirstName = "Tomek";
            const string LastName = "Kowalski";
            var name = new Name(FirstName, LastName);
            userAggregateRoot.Confirm();
            userAggregateRoot.ChangeName(name);
            IList<DomainEvent> domainEvents = userAggregateRoot.GetUncommittedChanges().ToList();
            domainEvents.Count.Should().Be(3);
            domainEvents[2].Version.Should().Be(2);
            userAggregateRoot.Name!.FirstName.Should().Be(FirstName);
            userAggregateRoot.Name!.LastName.Should().Be(LastName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenToTryChangeNameWithoutConfirmation()
        {
            var userAggregateRoot = _userFactory.Create();
            const string FirstName = "Tomek";
            const string LastName = "Kowalski";
            var name = new Name(FirstName, LastName);
            Action act = () => userAggregateRoot.ChangeName(name);
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User cannot be modified without confirmation");
        }

        [Fact]
        public void ShouldThrowExceptionWhenUserTryChangeNameUsingSameName()
        {
            var userAggregateRoot = _userFactory.Create();
            var name = new Name(_firstName, _lastName);
            userAggregateRoot.Confirm();
            Action act = () => userAggregateRoot.ChangeName(name);
            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User cannot change the same name");
        }

        [Fact]
        public void ShouldEmitEventUserDeletedEvent()
        {
            var userAggregateRoot = _userFactory.Create();
            userAggregateRoot.Delete();
            IList<DomainEvent> domainEvents = userAggregateRoot.GetUncommittedChanges().ToList();
            var deleteTime = domainEvents[1].TimeStamp;
            domainEvents
                .Count
                .Should()
                .Be(2);
            userAggregateRoot.DeletedDate.Should().Be(deleteTime);
        }

        [Fact]
        public void ShouldLoadEventsFromHistoryListAndApplyChangesToEmptyAggregate()
        {
            var userId = new UserId(Guid.NewGuid());
            var login = new Login(_login);
            var password = new Password(_password);
            var name = new Name(_firstName, _lastName);
            var birthDate = new BirthDate(AppTime.Now().AddYears(-18));

            var userRegisteredEvent = new UserRegisteredEvent(
                userId,
                login,
                password,
                name,
                birthDate,
                0);

            var userRegistrationConfirmedEvent =
                                new UserRegistrationConfirmedEvent(
                                    userId,
                                    UserStatus.Activated,
                                    1);

            var newLogin = new Login("somelogin@gmail.com");
            var userChangedLoginEvent = new UserChangedLoginEvent(
                userId,
                newLogin,
                2);

            var domainEvents = new List<DomainEvent>
            {
                userRegisteredEvent,
                userRegistrationConfirmedEvent,
                userChangedLoginEvent
            };

            var userAggregate = new UserAggregateRoot();
            userAggregate.LoadFromHistory(domainEvents);
            userAggregate.Login!.Value.Should().Be(newLogin.Value);
            userAggregate.Version.Should().Be(3);
        }

        [Fact]
        public void ShouldLoadEventFromHistoryAddApplyNewEventFromAggregateUsingMethodChangePassword()
        {
            var userId = new UserId(Guid.NewGuid());
            var login = new Login(_login);
            var password = new Password(_password);
            var name = new Name(_firstName, _lastName);
            var birthDate = new BirthDate(AppTime.Now().AddYears(-18));

            var userRegisteredEvent = new UserRegisteredEvent(
                userId,
                login,
                password,
                name,
                birthDate,
                0);
            var userRegistrationConfirmedEvent =
                                new UserRegistrationConfirmedEvent(
                                    userId,
                                    UserStatus.Activated,
                                    1);
            var newLogin = new Login("somelogin@gmail.com");

            var userChangedLoginEvent = new UserChangedLoginEvent(
                userId,
                newLogin,
                2);

            var domainEvents = new List<DomainEvent>
            {
                userRegisteredEvent,
                userRegistrationConfirmedEvent,
                userChangedLoginEvent
            };

            var userAggregate = new UserAggregateRoot();
            userAggregate.LoadFromHistory(domainEvents);
            var newPassword = new Password("testTest123@#1");
            userAggregate.ChangePassword(newPassword);
            IList<DomainEvent> newEvents = userAggregate.GetUncommittedChanges().ToList();
            newEvents.Count.Should().Be(1);
            newEvents[0].Version.Should().Be(3);
            userAggregate.Version.Should().Be(4);
            userAggregate.Password!.Value.Should().Be(newPassword.Value);
        }
    }
}
