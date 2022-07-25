using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace User.Domain.User.Events
{
    public class UserRegistrationConfirmedEvent : DomainEvent
    {
        public UserStatus Status { get; private set; }

        public UserRegistrationConfirmedEvent(UserId key, UserStatus status, int version)
            : base(key, version)
        {
            Status = status;
        }
    }
}
