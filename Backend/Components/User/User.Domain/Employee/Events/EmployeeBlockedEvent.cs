using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Domain.Employee.Events
{
    public class EmployeeBlockedEvent : DomainEvent
    {
        public EmployeeBlockStatus Status { get; }

        public EmployeeBlockedEvent(
            EmployeeId key,
            EmployeeBlockStatus status,
            int version) : base(key,version) => Status = status;
    }
}