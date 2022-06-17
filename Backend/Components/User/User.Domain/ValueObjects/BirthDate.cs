using SharedKernel.Domain.ValueObjects;

namespace User.Domain.ValueObjects
{
    public class BirthDate : ValueObject
    {
        public DateTimeOffset Value { get; private set; }

        private BirthDate(DateTimeOffset birthDate)
        {
            Value = birthDate;
        }

        public static BirthDate Create(DateTimeOffset birthDate)
        {
            //Some check rule here 
            return new BirthDate(birthDate);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}