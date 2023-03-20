using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.Manager;
using User.Domain.ValueObjects;

namespace User.Domain.Employee.Events
{
    public class EmployeeCreatedEvent : DomainEvent
    {
        public ManagerId ManagerId { get; }
        public Email Email { get; }
        public EmployeePrivileges Privileges { get; }

        public EmployeeCreatedEvent(
            EmployeeId key,
            ManagerId managerId,
            Email email,
            EmployeePrivileges privileges,
            int version) : base(key,version)
        {
            Email = email;
            Privileges = privileges;
            ManagerId = managerId;
        }
    }
}