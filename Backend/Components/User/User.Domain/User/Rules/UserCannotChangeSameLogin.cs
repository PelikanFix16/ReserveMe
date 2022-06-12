using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;

namespace User.Domain.User.Rules
{
    public class UserCannotChangeSameLogin : IBusinessRule
    {
        public string Message => "User Cannot change login to the same login";
        private readonly Login _oldLogin;
        private readonly Login _newLogin;

        public UserCannotChangeSameLogin(Login oldLogin,Login newLogin)
        {
            _oldLogin = oldLogin;
            _newLogin = newLogin;
        }

        public bool IsBroken() => _oldLogin == _newLogin;
        
    }
}