using SharedKernel.Domain.Event;
using User.Domain.ValueObjects;

namespace User.Domain.User.Events
{
    public class UserChangedLoginEvent : DomainEvent
    {
        public Email Login { get; private set; }

        public UserChangedLoginEvent(UserId id, Email login, int version)
            : base(id, version)
        {
            Login = login;
        }
    }
}
