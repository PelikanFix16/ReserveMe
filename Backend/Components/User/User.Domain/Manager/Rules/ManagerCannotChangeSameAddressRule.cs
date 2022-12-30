using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;
using User.Domain.ValueObjects;

namespace User.Domain.Manager.Rules
{
    public class ManagerCannotChangeSameAddressRule : IBusinessRule
    {
        private readonly Address _address;
        private readonly Address _newAddress;

        public string Message => "Manager cannot change same address";

        public ManagerCannotChangeSameAddressRule(Address address, Address newAddress)
        {
            _address = address;
            _newAddress = newAddress;
        }

        public bool IsBroken()
        {
            return _address == _newAddress;
        }
    }

}
