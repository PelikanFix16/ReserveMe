using SharedKernel.Domain.ValueObjects;
using User.Domain.ValueObjects.Rules;

namespace User.Domain.ValueObjects
{
    public sealed class Name : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Name(string firstName, string lastName)
        {
            CheckRule(new ValidNameRule(firstName));
            CheckRule(new ValidNameRule(lastName));
            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
