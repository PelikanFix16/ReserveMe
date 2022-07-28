using SharedKernel.Domain;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.ValueObjects.Rules
{
    public class BirthDateRule : IBusinessRule
    {
        private readonly DateTimeOffset _birthDate;

        public string Message => "Birth date should be between 12 and 120 year old";

        private readonly int _minRequiredBirthDate = 12;
        private readonly int _maxRequiredBirthDate = 120;

        public BirthDateRule(DateTimeOffset birthDate)
        {
            _birthDate = birthDate;
        }

        public bool IsBroken()
        {
            var currentYear = AppTime.Now().Year;
            var birthDateYear = _birthDate.Year;
            var birthDateInYears = Math.Abs(currentYear - birthDateYear);
            return (birthDateInYears < _minRequiredBirthDate)
                || (birthDateInYears > _maxRequiredBirthDate);
        }
    }
}
