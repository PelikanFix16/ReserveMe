using System.Text.RegularExpressions;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.ValueObjects.Rules
{
    public class PasswordMustBeStrongRule : IBusinessRule
    {
        private readonly string _password;

        public PasswordMustBeStrongRule(string password)
        {
            _password = password;
        }

        public string Message => "Password is to weak";

        public bool IsBroken()
        {
            if (string.IsNullOrEmpty(_password))
                return true;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var valid = hasNumber.IsMatch(_password) && hasUpperChar.IsMatch(_password) && hasMinimum8Chars.IsMatch(_password);
            return !valid;
        }
    }
}
