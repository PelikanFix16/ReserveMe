using SharedKernel.Domain.BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Domain.Employee.Rules
{
    public class EmployeeCannotChangeWhenBlockedRule : IBusinessRule
    {
        private readonly EmployeeBlockStatus _employeeStatus;

        public string Message => "Employee cannot change when blocked";

        public EmployeeCannotChangeWhenBlockedRule(EmployeeBlockStatus status) => _employeeStatus = status;

        public bool IsBroken() => _employeeStatus == EmployeeBlockStatus.Blocked;
    }

}