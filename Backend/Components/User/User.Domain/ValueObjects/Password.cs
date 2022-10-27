using SharedKernel.Domain.BusinessRule.Rules;
using SharedKernel.Domain.ValueObjects;

namespace User.Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {
        public string Value { get; private set; }

        public Password(string value)
        {
            CheckRule(new PasswordMustBeStrongRule(value));
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
