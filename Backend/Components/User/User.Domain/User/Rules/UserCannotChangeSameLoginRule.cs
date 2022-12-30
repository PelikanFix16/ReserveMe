using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;

namespace User.Domain.User.Rules
{
    public class UserCannotChangeSameLoginRule : IBusinessRule
    {
        public string Message => "User Cannot change login to the same login";
        private readonly Email _oldLogin;
        private readonly Email _newLogin;

        public UserCannotChangeSameLoginRule(Email oldLogin, Email newLogin)
        {
            _oldLogin = oldLogin;
            _newLogin = newLogin;
        }

        public bool IsBroken() => _oldLogin == _newLogin;
    }
}