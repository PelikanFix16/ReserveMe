using SharedKernel.Domain.BusinessRule.Rules;
using SharedKernel.Domain.ValueObjects;

namespace User.Domain.ValueObjects
{
    public sealed class Login : ValueObject
    {
        public string Value { get; private set; }

        public Login(string login)
        {
            CheckRule(new LoginMustBeEmailRule(login));
            Value = login;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
