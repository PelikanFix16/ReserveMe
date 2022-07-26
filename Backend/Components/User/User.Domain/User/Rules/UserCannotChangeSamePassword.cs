using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;

namespace User.Domain.User.Rules
{
    public class UserCannotChangeSamePassword : IBusinessRule
    {
        public string Message => "User cannot change the same password to same value";

        private readonly Password _oldPassword;
        private readonly Password _newPassword;

        public UserCannotChangeSamePassword(Password oldPassword, Password newPassword)
        {
            _oldPassword = oldPassword;
            _newPassword = newPassword;
        }

        public bool IsBroken() => _oldPassword == _newPassword;
    }
}
