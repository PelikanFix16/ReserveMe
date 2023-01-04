using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.ValueObjects;

namespace User.Domain.Employee.Events
{
    public class EmployeeChangedPasswordEvent : DomainEvent
    {
        public Password Password { get; }

        public EmployeeChangedPasswordEvent(
            EmployeeId key,
            Password password,
            int version) : base(key,version) => Password = password;
    }
}