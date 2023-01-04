using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.Manager.Rules
{
    public class ManagerCannotBeUnblockedMoreThanOnceRule : IBusinessRule
    {
        private readonly ManagerBlockedStatus _blockedStatus;

        public string Message => "Manager cannot be unblocked more than once";

        public ManagerCannotBeUnblockedMoreThanOnceRule(ManagerBlockedStatus status) => _blockedStatus = status;

        public bool IsBroken() => _blockedStatus == ManagerBlockedStatus.UnBlocked;
    }

}
