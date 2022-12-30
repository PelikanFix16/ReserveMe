using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.Manager.Rules
{
    public class ManagerCannotChangeWhenBlockedRule : IBusinessRule
    {
        private readonly BlockedStatus _blockedStatus;

        public string Message => "Manager cannot be modified when blocked";

        public ManagerCannotChangeWhenBlockedRule(BlockedStatus status)
        {
            _blockedStatus = status;
        }

        public bool IsBroken()
        {
            return _blockedStatus == BlockedStatus.Blocked;
        }
    }

}
