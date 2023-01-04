using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Domain.Employee.Events
{
    public class EmployeeConfirmedEvent : DomainEvent
    {
        public EmployeeStatus Status { get; }

        public EmployeeConfirmedEvent(
            EmployeeId key,
            EmployeeStatus status,
            int version) : base(key,version) => Status = status;
    }
}