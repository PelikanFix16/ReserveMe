using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace User.Domain.User.Events
{
    public class UserDeletedEvent : DomainEvent
    {

        
        public UserDeletedEvent(UserId key,int version) : base(key, version)
        {
        }
    }
}