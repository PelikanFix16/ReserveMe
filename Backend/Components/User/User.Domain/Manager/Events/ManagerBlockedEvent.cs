using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace User.Domain.Manager.Events
{
    public class ManagerBlockedEvent : DomainEvent
    {
        public ManagerBlockedStatus Status { get; }

        public ManagerBlockedEvent(
            ManagerId key,
            ManagerBlockedStatus status,
            int version) : base(key,version) => Status = status;
    }
}
