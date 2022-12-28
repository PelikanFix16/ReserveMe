using SharedKernel.Domain.Event;
using User.Domain.ValueObjects;

namespace User.Domain.User.Events
{
    public class UserRegisteredEvent : DomainEvent
    {
        public Email Login { get; private set; }
        public Password Password { get; private set; }
        public Name Name { get; private set; }
        public BirthDate BirthDate { get; private set; }

        public UserRegisteredEvent(
            UserId key,
            Email login,
            Password password,
            Name name,
            BirthDate birthDate,
            int version)
            : base(key, version)
        {
            Login = login;
            Password = password;
            Name = name;
            BirthDate = birthDate;
        }
    }
}
