using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.Manager.Rules
{
    public class ManagerCannotBeBlockedMoreThanOnceRule : IBusinessRule
    {
        private readonly ManagerBlockedStatus _blockedStatus;

        public string Message => "Manager cannot be blocked more than once";

        public ManagerCannotBeBlockedMoreThanOnceRule(ManagerBlockedStatus status)
        {
            _blockedStatus = status;
        }

        public bool IsBroken()
        {
            return _blockedStatus == ManagerBlockedStatus.Blocked;
        }
    }

}
