using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using User.Domain.ValueObjects;

namespace User.Domain.User.Events
{
    public class UserChangedPasswordEvent : DomainEvent
    {
        public Password Password { get; private set; }

        public UserChangedPasswordEvent(UserId key, Password password, int version)
            : base(key, version)
        {
            Password = password;
        }
    }
}
