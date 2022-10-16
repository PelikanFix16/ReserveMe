using SharedKernel.Domain.ValueObjects;
using User.Domain.ValueObjects.Rules;

namespace User.Domain.ValueObjects
{
    public sealed class BirthDate : ValueObject
    {
        public DateTimeOffset Value { get; private set; }

        public BirthDate(DateTimeOffset birthDate)
        {
            //User should be between 12 and 120 years old
            CheckRule(new BirthDateRule(birthDate));
            Value = birthDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
