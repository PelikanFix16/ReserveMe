using SharedKernel.Domain.UniqueKey;

namespace User.Domain.User
{
    public class UserId : AggregateKey
    {
        public UserId(Guid key) : base(key)
        {
        }
    }
}