using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.Manager.Rules
{
    public class ManagerCannotBeConfirmedMoreThanOnceRule : IBusinessRule
    {
        public string Message => "Manager cannot be confirmed more than once";

        private readonly ManagerStatus _status;

        public ManagerCannotBeConfirmedMoreThanOnceRule(ManagerStatus status) => _status = status;

        public bool IsBroken() => _status == ManagerStatus.Activated;
    }
}
