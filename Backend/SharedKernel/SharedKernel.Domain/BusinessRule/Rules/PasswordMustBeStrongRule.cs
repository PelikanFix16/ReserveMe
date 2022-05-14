using System.Text.RegularExpressions;

namespace SharedKernel.Domain.BusinessRule.Rules
{
    public class PasswordMustBeStrongRule : IBusinessRule
    {
        private readonly string Password;

        public PasswordMustBeStrongRule(string password)
        {
            Password = password;
        }

        public string Message => "Password is to weak";

        public bool IsBroken()
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            bool valid = hasNumber.IsMatch(Password) && hasUpperChar.IsMatch(Password) && hasMinimum8Chars.IsMatch(Password);
            return !valid;
        }
    }
}