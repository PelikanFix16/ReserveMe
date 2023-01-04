using SharedKernel.Domain.BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Domain.Employee.Rules
{
    public class EmployeeCannotBeConfirmedMoreThanOnceRule : IBusinessRule
    {
        private readonly EmployeeStatus _employeeStatus;

        public string Message => "Employee cannot be confirmed more than once";

        public EmployeeCannotBeConfirmedMoreThanOnceRule(EmployeeStatus status) => _employeeStatus = status;

        public bool IsBroken() => _employeeStatus == EmployeeStatus.Activated;
    }
}