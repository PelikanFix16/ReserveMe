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
        public BirthDate? BirthDate { get; private set; }
        public DateTimeOffset RegisteredDate { get; private set; }
        public DateTimeOffset DeletedDate { get; private set; }
        public UserStatus Status { get; private set; } = UserStatus.DeActivated;


        private void Apply(UserRegisteredEvent e)
        {
            Id = e.Key as UserId;
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
        private void Apply(UserRegistrationConfirmedEvent e)
        {
            Status = e.Status;
        }
        private void Apply(UserChangedNameEvent e)
        {
            Name = e.Name;
        }
        private void Apply(UserDeletedEvent e)
        {
            DeletedDate = e.TimeStamp;
        }

        public UserAggregateRoot(UserId id, Login login, Password password, Name name, BirthDate birthDate)
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

            CheckRule(new UserCannotBeModifiedWithoutConfirmatio(Status));
            CheckRule(new UserCannotChangeSamePassword(Password, newPassword));
            ApplyChange(new UserChangedPasswordEvent(Id, newPassword, Version));
        }


        public void ChangeLogin(Login newLogin)
        {
            if (Login is null)
                throw new NullReferenceException("Login cannot be null");
            if (Id is null)
                throw new NullReferenceException("Id cannot be null");

            CheckRule(new UserCannotBeModifiedWithoutConfirmatio(Status));
            CheckRule(new UserCannotChangeSameLogin(Login, newLogin));
            ApplyChange(new UserChangedLoginEvent(Id, newLogin, Version));

        }

        public void ChangeName(Name newName)
        {
            if (Id is null)
                throw new NullReferenceException("Id cannot be null");
            if (Name is null)
                throw new NullReferenceException("Name cannot be null");
            CheckRule(new UserCannotBeModifiedWithoutConfirmatio(Status));
            CheckRule(new UserCannotChangeSameName(newName, Name));
            ApplyChange(new UserChangedNameEvent(Id, newName, Version));

        }

        public void Delete()
        {
            if (Id is null)
                throw new NullReferenceException("Id cannot be null");
            ApplyChange(new UserDeletedEvent(Id, Version));
        }



    }
}
