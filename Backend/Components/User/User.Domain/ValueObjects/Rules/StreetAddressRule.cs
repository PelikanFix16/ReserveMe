using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.ValueObjects.Rules
{
    public class StreetAddressRule : IBusinessRule
    {
        public string Message => "Street cannot be longer than 100 characters";
        private readonly string _street;

        public StreetAddressRule(string street)
        {
            _street = street;
        }

        public bool IsBroken()
        {
            if (string.IsNullOrEmpty(_street))
                return true;
            if (_street.Length > 100)
                return true;
            return false;
        }

    }
}
