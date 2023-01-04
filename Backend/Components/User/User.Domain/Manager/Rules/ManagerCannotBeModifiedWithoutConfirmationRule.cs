using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.BusinessRule;

namespace User.Domain.Manager.Rules
{
    public class ManagerCannotBeModifiedWithoutConfirmationRule : IBusinessRule
    {
        private readonly ManagerStatus _status;

        public string Message => "Manager cannot be modified without confirmation";

        public ManagerCannotBeModifiedWithoutConfirmationRule(ManagerStatus status) => _status = status;

        public bool IsBroken() => _status == ManagerStatus.DeActivated;
    }

}
