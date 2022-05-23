using SharedKernel.Domain.Aggregate;
using User.Domain.UserRegistration.Events;
using User.Domain.UserRegistration.Rules;
using User.Domain.ValueObjects;

namespace User.Domain.UserRegistration
{
    public class UserRegistration : AggregateRoot
    {
        public UserRegistrationId? Id { get; private set; }
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

        public UserRegistration(UserRegistrationId id, Login login, Password password, Name name, DateTimeOffset birthDate)
        {
            ApplyChange(new UserRegisteredEvent(id, login, password, name, birthDate, Version));

        }
        public UserRegistration()
        {

        }

        public void Confirm()
        {
            CheckRule(new UserCannotBeConfirmedMoreThanOnceRule(Status));
            if(Id is null)
                throw new ArgumentNullException("Id cannot be null");
            ApplyChange(new UserRegistrationConfirmedEvent(Id,UserStatus.Activated,Version));

        }









    }
}