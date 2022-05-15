using SharedKernel.Domain.UniqueKey;

namespace User.Domain.UserRegistration
{
    public class UserRegistrationId : AggregateKey
    {
        public UserRegistrationId(Guid key) : base(key)
        {
        }
    }
}