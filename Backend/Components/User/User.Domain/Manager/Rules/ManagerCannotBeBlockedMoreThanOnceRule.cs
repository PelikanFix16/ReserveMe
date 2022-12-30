using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.Manager.Rules
{
    public class ManagerCannotBeBlockedMoreThanOnceRule : IBusinessRule
    {
        private readonly BlockedStatus _blockedStatus;

        public string Message => "Manager cannot be blocked more than once";

        public ManagerCannotBeBlockedMoreThanOnceRule(BlockedStatus status)
        {
            _blockedStatus = status;
        }

        public bool IsBroken()
        {
            return _blockedStatus == BlockedStatus.Blocked;
        }
    }

}
