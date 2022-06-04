using SharedKernel.Domain.UniqueKey;

namespace User.Domain.User
{
    public class UserRegistrationId : AggregateKey
    {
        public UserRegistrationId(Guid key) : base(key)
        {
        }
    }
}