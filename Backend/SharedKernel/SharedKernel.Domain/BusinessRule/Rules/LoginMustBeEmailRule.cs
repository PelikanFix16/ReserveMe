using System.Text.RegularExpressions;

namespace SharedKernel.Domain.BusinessRule.Rules
{
    public class LoginMustBeEmailRule : IBusinessRule
    {

        private readonly string Email;


        public LoginMustBeEmailRule(string email)
        {
            Email = email;
        }

        public string Message => "Login mest be an email";

        public bool IsBroken()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
        + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
        + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            Regex reg = new Regex(validEmailPattern, RegexOptions.IgnoreCase);
            bool validated = reg.IsMatch(Email);
            return !validated;
        }
    }
}