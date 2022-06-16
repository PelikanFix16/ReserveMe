using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using User.Domain.ValueObjects;

namespace User.Domain.User.Events
{
    public class UserChangedNameEvent : DomainEvent
    {
        public Name Name;
        public UserChangedNameEvent(UserId key,Name name, int version) : base(key, version)
        {
            Name = name;
        }
    }
}