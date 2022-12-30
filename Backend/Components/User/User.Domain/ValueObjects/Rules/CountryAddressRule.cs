using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.ValueObjects.Rules
{
    public class CountryAddressRule : IBusinessRule
    {
        public string Message => "Country is not valid";

        private readonly string _country;

        public CountryAddressRule(string country)
        {
            _country = country;
        }

        public bool IsBroken()
        {
            if (string.IsNullOrEmpty(_country))
                return true;
            if (_country.Length > 100)
                return true;
            return false;
        }
    }
}
