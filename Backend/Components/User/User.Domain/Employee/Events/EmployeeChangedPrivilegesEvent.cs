using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Domain.Employee.Events
{
    public class EmployeeChangedPrivilegesEvent : DomainEvent
    {
        public EmployeePrivileges Privileges { get; }

        public EmployeeChangedPrivilegesEvent(
            EmployeeId key,
            EmployeePrivileges privileges,
            int version) : base(key,version) => Privileges = privileges;
    }
}