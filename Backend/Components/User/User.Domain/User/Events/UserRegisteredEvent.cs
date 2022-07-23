using SharedKernel.Domain.Event;
using User.Domain.ValueObjects;

namespace User.Domain.User.Events
{
    public class UserRegisteredEvent : DomainEvent
    {

        public Login Login { get; }
        public Password Password { get; }
        public Name Name { get; }
        public BirthDate BirthDate { get; }



        public UserRegisteredEvent(
            UserId key,
            Login login,
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
