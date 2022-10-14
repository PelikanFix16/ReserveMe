using SharedKernel.Domain.BusinessRule.Rules;
using SharedKernel.Domain.ValueObjects;

namespace User.Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {
        public string Value { get; private set; }

        public Password(string password)
        {
            CheckRule(new PasswordMustBeStrongRule(password));
            Value = password;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
