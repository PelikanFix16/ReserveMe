using SharedKernel.Domain.BusinessRule;

namespace User.Domain.User.Rules
{
    public class UserCannotBeModifiedWithoutConfirmationRule : IBusinessRule
    {
        public string Message => "User cannot be modified without confirmation";

        private readonly UserStatus _status;

        public UserCannotBeModifiedWithoutConfirmationRule(UserStatus status)
        {
            _status = status;
        }

        public bool IsBroken() => _status == UserStatus.DeActivated;
    }
}
