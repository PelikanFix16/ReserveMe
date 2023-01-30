using SharedKernel.Domain.Event;

namespace User.Domain.User.Events
{
    public class UserRegistrationConfirmedEvent : DomainEvent
    {
        public UserStatus Status { get; }

        public UserRegistrationConfirmedEvent(UserId key,UserStatus status,int version)
            : base(key,version) => Status = status;
    }
}
