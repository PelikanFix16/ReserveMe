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

        private string _login = "example@mail.com";
        private string _password = "Test12@1!";
        private string _firstName = "Test";
        private string _lastName = "Test";
        private DateTimeOffset _birthDate = AppTime.Now().AddYears(-18);
        private UserAggregateRootFactory userFactory;

        public UserAggregateRootTest()
        {
            userFactory = new UserAggregateRootFactory();
            userFactory.AddBirthDate(_birthDate)
                .AddLogin(_login)
                .AddName(_firstName, _lastName)
                .AddPassword(_password);


        }

        [Fact]
        public void Should_Create_User_with_event_UserRegisteredEvent()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();

            IList<DomainEvent> _event = userAggregateRoot.GetUncomittedChanges().ToList();

            _event.Count().Should().Be(1);
            _event[0].Version.Should().Be(0);
            userAggregateRoot.Login!.Value.Should().Be(_login);
            userAggregateRoot.Name!.FirstName.Should().Be(_firstName);
            userAggregateRoot.Name!.LastName.Should().Be(_lastName);
            userAggregateRoot.Password!.Value.Should().Be(_password);


        }

        [Fact]
        public void Should_Confirm_New_User_with_event_UserRegistrationConfirmedEvent()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();
            userAggregateRoot.Confirm();

            IList<DomainEvent> _event = userAggregateRoot.GetUncomittedChanges().ToList();

            _event.Count().Should().Be(2);
            _event[1].Version.Should().Be(1);
            userAggregateRoot.Status.Should().Be(UserStatus.Activated);


        }

        [Fact]
        public void Should_throw_exception_when_confirm_user_second_time()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();
            userAggregateRoot.Confirm();

            Action act = () => userAggregateRoot.Confirm();

            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User Registration cannot be confirmed more than once");

        }

        [Fact]
        public void Should_change_password_when_is_confirmed_and_should_create_event_UserChangedPasswordEvent()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();
            string newPass = "Test12!test";
            Password pass = Password.Create(newPass);

            userAggregateRoot.Confirm();
            userAggregateRoot.ChangePassword(pass);

            IList<DomainEvent> events = userAggregateRoot.GetUncomittedChanges().ToList();

            events.Count().Should().Be(3);
            events[2].Version.Should().Be(2);
            userAggregateRoot.Password!.Value.Should().Be(newPass);

        }

        [Fact]
        public void Should_throw_exception_when_to_try_change_password_without_confirmation()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();
            string newPass = "Test12!test";
            Password pass = Password.Create(newPass);

            Action act = () => userAggregateRoot.ChangePassword(pass);

            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User cannot be modified without confirmation");

        }

        [Fact]
        public void Should_throw_exception_when_user_try_change_password_using_same_password()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();

            Password pass = Password.Create(_password);

            userAggregateRoot.Confirm();
            Action act = () => userAggregateRoot.ChangePassword(pass);

            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User cannot change the same passwrod to same value");


        }

        [Fact]
        public void Should_change_login_when_is_confirmed_and_should_create_event_UserChangedLoginEvent()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();
            var newLogin = "test@gmail.com";
            Login login = Login.Create(newLogin);

            userAggregateRoot.Confirm();
            userAggregateRoot.ChangeLogin(login);

            IList<DomainEvent> domainEvents = userAggregateRoot.GetUncomittedChanges().ToList();

            domainEvents.Count().Should().Be(3);
            domainEvents[2].Version.Should().Be(2);
            userAggregateRoot.Login!.Value.Should().Be(newLogin);


        }

        [Fact]
        public void Should_throw_exception_when_to_try_change_login_without_confirmation()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();
            var newLogin = "test@gmail.com";
            Login login = Login.Create(newLogin);

            Action act = () => userAggregateRoot.ChangeLogin(login);

            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User cannot be modified without confirmation");
        }

        [Fact]
        public void Should_throw_exception_when_user_try_change_login_using_same_login()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();
            Login login = Login.Create(_login);

            userAggregateRoot.Confirm();
            Action act = () => userAggregateRoot.ChangeLogin(login);

            act.Should()
                .Throw<BusinessRuleValidationException>()
                .WithMessage("User Cannot change login to the same login");

        }

        [Fact]
        public void Should_change_name_when_is_confirmed_and_should_create_event_UserChangedNameEvent()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();
            var FirstName = "Tomek";
            var LastName = "Kowalski";

            Name name = Name.Create(FirstName, LastName);

            userAggregateRoot.Confirm();
            userAggregateRoot.ChangeName(name);

            IList<DomainEvent> domainEvents = userAggregateRoot.GetUncomittedChanges().ToList();

            domainEvents.Count().Should().Be(3);
            domainEvents[2].Version.Should().Be(2);
            userAggregateRoot.Name!.FirstName.Should().Be(FirstName);
            userAggregateRoot.Name!.LastName.Should().Be(LastName);

        }

        [Fact]
        public void Should_throw_exception_when_to_try_change_name_without_confirmation()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();
            var FirstName = "Tomek";
            var LastName = "Kowalski";

            Name name = Name.Create(FirstName, LastName);

            Action act = () => userAggregateRoot.ChangeName(name);
            act.Should()
              .Throw<BusinessRuleValidationException>()
              .WithMessage("User cannot be modified without confirmation");

        }

        [Fact]
        public void Should_throw_exception_when_user_try_change_name_using_same_name()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();
            Name name = Name.Create(_firstName, _lastName);
            userAggregateRoot.Confirm();
            Action act = () => userAggregateRoot.ChangeName(name);

            act.Should()
                    .Throw<BusinessRuleValidationException>()
                    .WithMessage("User cannot change the same name");


        }

        [Fact]
        public void Should_emit_event_UserDeletedEvent()
        {
            UserAggregateRoot userAggregateRoot = userFactory.Create();

            userAggregateRoot.Delete();

            IList<DomainEvent> domainEvents = userAggregateRoot.GetUncomittedChanges().ToList();
            DateTimeOffset deleteTime = domainEvents[1].TimeStamp;

            domainEvents.Count().Should().Be(2);
            userAggregateRoot.DeletedDate.Should().Be(deleteTime);

        }

        [Fact]
        public void Should_Load_events_from_history_list_and_apply_changes_to_empty_aggregate()
        {
            UserId userId = new UserId(Guid.NewGuid());
            Login login = Login.Create(_login);
            Password password = Password.Create(_password);
            Name name = Name.Create(_firstName, _lastName);
            BirthDate birthDate = BirthDate.Create(AppTime.Now().AddYears(-18));


            UserRegisteredEvent userRegisteredEvent = new UserRegisteredEvent(userId,
                                                                              login,
                                                                              password,
                                                                              name,
                                                                              birthDate,
                                                                              0);
            UserRegistrationConfirmedEvent userRegistrationConfirmedEvent =
                                new UserRegistrationConfirmedEvent(userId,
                                                                   UserStatus.Activated,
                                                                    1);

            var newLogin = Login.Create("somelogin@gmail.com");

            UserChangedLoginEvent userChangedLoginEvent = new UserChangedLoginEvent(userId,
                                                                                    newLogin,
                                                                                    2);

            List<DomainEvent> domainEvents = new List<DomainEvent>();
            domainEvents.Add(userRegisteredEvent);
            domainEvents.Add(userRegistrationConfirmedEvent);
            domainEvents.Add(userChangedLoginEvent);

            UserAggregateRoot userAggregate = new UserAggregateRoot();

            userAggregate.LoadFromHistory(domainEvents);

            userAggregate.Login!.Value.Should().Be(newLogin.Value);
            userAggregate.Version.Should().Be(3);



        }

        [Fact]
        public void Should_Load_event_from_history_add_apply_new_event_from_aggregate_using_method_ChangePassword_event_should_have_current_version_and_new_password()
        {
                    UserId userId = new UserId(Guid.NewGuid());
            Login login = Login.Create(_login);
            Password password = Password.Create(_password);
            Name name = Name.Create(_firstName, _lastName);
            BirthDate birthDate = BirthDate.Create(AppTime.Now().AddYears(-18));


            UserRegisteredEvent userRegisteredEvent = new UserRegisteredEvent(userId,
                                                                              login,
                                                                              password,
                                                                              name,
                                                                              birthDate,
                                                                              0);
            UserRegistrationConfirmedEvent userRegistrationConfirmedEvent =
                                new UserRegistrationConfirmedEvent(userId,
                                                                   UserStatus.Activated,
                                                                    1);

            var newLogin = Login.Create("somelogin@gmail.com");

            UserChangedLoginEvent userChangedLoginEvent = new UserChangedLoginEvent(userId,
                                                                                    newLogin,
                                                                                    2);

            List<DomainEvent> domainEvents = new List<DomainEvent>();
            domainEvents.Add(userRegisteredEvent);
            domainEvents.Add(userRegistrationConfirmedEvent);
            domainEvents.Add(userChangedLoginEvent);

            UserAggregateRoot userAggregate = new UserAggregateRoot();

            userAggregate.LoadFromHistory(domainEvents);
            
            var newPassword = Password.Create("testTest123@#1");

            userAggregate.ChangePassword(newPassword);

            IList<DomainEvent> newEvents = userAggregate.GetUncomittedChanges().ToList();

            newEvents.Count().Should().Be(1);
            newEvents[0].Version.Should().Be(3);
            userAggregate.Version.Should().Be(4);
            userAggregate.Password!.Value.Should().Be(newPassword.Value);



        }


    }
}