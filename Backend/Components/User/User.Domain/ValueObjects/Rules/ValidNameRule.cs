using System.Text.RegularExpressions;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.ValueObjects.Rules
{
    public class ValidNameRule : IBusinessRule
    {
        private readonly string Name;

        public string Message => "Name must be valid";

        public ValidNameRule(string _name)
        {
            Name = _name;
        }
        public bool IsBroken()
        {
            if (string.IsNullOrEmpty(Name))
                return true;

            return (!Regex.Match(Name, "^[A-Z][a-zA-Z]*$").Success);
        }
    }
}