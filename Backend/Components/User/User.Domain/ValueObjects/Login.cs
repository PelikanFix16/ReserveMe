using SharedKernel.Domain.BusinessRule.Rules;
using SharedKernel.Domain.ValueObjects;

namespace User.Domain.ValueObjects
{
    public class Login : ValueObject
    {
        public string Value { get; private set; }

        private Login(string login)
        {
            Value = login;
        }

        public static Login Create(string login)
        {
            CheckRule(new LoginMustBeEmailRule(login));
            return new Login(login);
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}