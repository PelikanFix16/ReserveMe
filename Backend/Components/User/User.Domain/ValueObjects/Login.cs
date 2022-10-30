using SharedKernel.Domain.ValueObjects;
using User.Domain.ValueObjects.Rules;

namespace User.Domain.ValueObjects
{
    public sealed class Login : ValueObject
    {
        public string Value { get; private set; }

        public Login(string value)
        {
            CheckRule(new LoginMustBeEmailRule(value));
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
