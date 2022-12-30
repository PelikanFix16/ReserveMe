using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.ValueObjects.Rules
{
    public class CityAddressRule : IBusinessRule
    {
        private readonly string _city;

        public string Message => "City cannot be longer than 100 characters";

        public CityAddressRule(string city)
        {
            _city = city;
        }

        public bool IsBroken()
        {
            if (string.IsNullOrEmpty(_city))
                return true;
            if (_city.Length > 100)
                return true;
            return false;
        }
    }
}
