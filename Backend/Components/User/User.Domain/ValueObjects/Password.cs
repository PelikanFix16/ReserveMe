using SharedKernel.Domain.BusinessRule.Rules;
using SharedKernel.Domain.ValueObjects;

namespace User.Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {
        public string Value { get; private set; }

        private Password(string password)
        {
            Value = password;
        }
        public static Password Create(string password)
        {
            CheckRule(new PasswordMustBeStrongRule(password));
            return new Password(password);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
