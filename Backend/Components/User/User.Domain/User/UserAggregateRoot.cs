using SharedKernel.Domain.Aggregate;
using User.Domain.User.Events;
using User.Domain.User.Rules;
using User.Domain.ValueObjects;

namespace User.Domain.User
{
    public class UserAggregateRoot : AggregateRoot
    {
        public UserId? Id { get; private set; }
        public Login? Login { get; private set; }
        public Password? Password { get; private set; }
        public Name? Name { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }
        public DateTimeOffset RegisteredDate { get; private set; }
        public UserStatus Status { get; private set; } = UserStatus.DeActivated;

        private void Apply(UserRegisteredEvent e)
        {
            Id = e.Id;
            Login = e.Login;
            Password = e.Password;
            Name = e.Name;
            BirthDate = e.BirthDate;
            RegisteredDate = e.TimeStamp;
        }
        private void Apply(UserChangedPasswordEvent e)
        {
            Password = e.Password;
        }
        private void Apply(UserChangedLoginEvent e)
        {
            Login = e.Login;
        }

        public UserAggregateRoot(UserId id, Login login, Password password, Name name, DateTimeOffset birthDate)
        {
            ApplyChange(new UserRegisteredEvent(id, login, password, name, birthDate, Version));

        }
        public UserAggregateRoot()
        {

        }

        public void Confirm()
        {
            CheckRule(new UserCannotBeConfirmedMoreThanOnceRule(Status));
            if (Id is null)
                throw new NullReferenceException("Id cannot be null");
            ApplyChange(new UserRegistrationConfirmedEvent(Id, UserStatus.Activated, Version));

        }


        public void ChangePassword(Password newPassword)
        {
            if (Password is null)
                throw new NullReferenceException("Password cannot be null");
            if (Id is null)
                throw new NullReferenceException("Id cannot be null");

            CheckRule(new UserCannotChangeSamePassword(Password, newPassword));
            ApplyChange(new UserChangedPasswordEvent(Id, newPassword, Version));
        }


        public void ChangeLogin(Login newLogin)
        {
            if (Login is null)
                throw new NullReferenceException("Login cannot be null");
            if (Id is null)
                throw new NullReferenceException("Id cannot be null");
            CheckRule(new UserCannotChangeSameLogin(Login, newLogin));
            ApplyChange(new UserChangedLoginEvent(Id, newLogin, Version));

        }



    }
}