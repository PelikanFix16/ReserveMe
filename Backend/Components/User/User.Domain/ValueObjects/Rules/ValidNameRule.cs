using System.Text.RegularExpressions;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.ValueObjects.Rules
{
    public class ValidNameRule : IBusinessRule
    {
        private readonly string _name;

        public string Message => "Name must be valid";

        public ValidNameRule(string name)
        {
            _name = name;
        }

        public bool IsBroken()
        {
            if (string.IsNullOrEmpty(_name))
                return true;

            return !Regex.Match(_name, "^[A-Z][a-zA-Z]*$").Success;
        }
    }
}
