using SharedKernel.Domain.BusinessRule;

namespace User.Domain.User.Rules
{
    public class UserCannotBeModifiedWithoutConfirmation : IBusinessRule
    {
        public string Message => "User cannot be modified without confirmation";

        private readonly UserStatus _status;

        public UserCannotBeModifiedWithoutConfirmation(UserStatus status)
        {
            _status = status;
        }

        public bool IsBroken() => _status == UserStatus.DeActivated;
    }
}
