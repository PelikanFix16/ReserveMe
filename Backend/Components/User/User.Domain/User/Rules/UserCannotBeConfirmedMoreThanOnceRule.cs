using SharedKernel.Domain.BusinessRule;

namespace User.Domain.User.Rules
{
    public class UserCannotBeConfirmedMoreThanOnceRule : IBusinessRule
    {
        public string Message => "User Registration cannot be confirmed more than once";

        private readonly UserStatus _status;

        internal UserCannotBeConfirmedMoreThanOnceRule(UserStatus status)
        {
            _status = status;
        }

        public bool IsBroken() => _status == UserStatus.Activated;
    }
}
