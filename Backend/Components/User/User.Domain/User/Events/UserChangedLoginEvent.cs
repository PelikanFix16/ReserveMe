using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using User.Domain.ValueObjects;

namespace User.Domain.User.Events
{
    public class UserChangedLoginEvent : DomainEvent
    {
        public Login Login;
        public UserChangedLoginEvent(UserId id,Login login, int version) : base(id, version)
        {
            Login = login;
        }
    }
}