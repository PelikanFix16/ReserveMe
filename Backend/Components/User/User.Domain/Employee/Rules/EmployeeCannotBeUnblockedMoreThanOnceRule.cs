using SharedKernel.Domain.BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Domain.Employee.Rules
{
    public class EmployeeCannotBeUnblockedMoreThanOnceRule : IBusinessRule
    {
        private readonly EmployeeBlockStatus _employeeBlockStatus;

        public string Message => "Employee cannot be unblocked more than once";

        public EmployeeCannotBeUnblockedMoreThanOnceRule(EmployeeBlockStatus status) => _employeeBlockStatus = status;

        public bool IsBroken() => _employeeBlockStatus == EmployeeBlockStatus.UnBlocked;
    }
}