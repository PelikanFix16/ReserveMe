using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.ValueObjects.Rules
{
    public class StateAddressRule : IBusinessRule
    {
        private readonly string _state;

        public string Message => "State cannot be longer than 100 characters";

        public StateAddressRule(string state)
        {
            _state = state;
        }
        public bool IsBroken()
        {
            if (string.IsNullOrEmpty(_state))
                return true;
            if (_state.Length > 100)
                return true;
            return false;
        }
    }
}
