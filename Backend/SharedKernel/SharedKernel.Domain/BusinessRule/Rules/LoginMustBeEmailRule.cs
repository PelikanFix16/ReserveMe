using System.Text.RegularExpressions;

namespace SharedKernel.Domain.BusinessRule.Rules
{
    public class LoginMustBeEmailRule : IBusinessRule
    {
        private readonly string _email;

        public LoginMustBeEmailRule(string email)
        {
            _email = email;
        }

        public string Message => "Login must be an email";

        public bool IsBroken()
        {
            if (string.IsNullOrEmpty(_email))
                return true;

            const string ValidEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var reg = new Regex(ValidEmailPattern, RegexOptions.IgnoreCase);
            var validated = reg.IsMatch(_email);
            return !validated;
        }
    }
}
