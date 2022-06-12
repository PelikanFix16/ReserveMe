using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using User.Domain.ValueObjects;

namespace User.Domain.User.Events
{
    public class UserChangedPasswordEvent : DomainEvent
    {
        public Password Password;

        public UserChangedPasswordEvent(AggregateKey key,Password password, int version) : base(key, version)
        {
            Password = password;
        }
    }
}