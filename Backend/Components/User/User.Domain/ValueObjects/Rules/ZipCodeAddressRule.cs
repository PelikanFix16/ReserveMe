using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.ValueObjects.Rules
{
    public class ZipCodeAddressRule : IBusinessRule
    {
        private readonly string _zipCode;

        public string Message => "ZipCode cannot be longer than 10 characters";

        public ZipCodeAddressRule(string zipCode)
        {
            _zipCode = zipCode;
        }

        public bool IsBroken()
        {
            if (string.IsNullOrEmpty(_zipCode))
                return true;
            if (_zipCode.Length > 10)
                return true;
            return false;
        }
    }

}
