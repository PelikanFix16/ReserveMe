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
        public BlockedStatus Status { get; private set; }

        public ManagerBlockedEvent(
            ManagerId key,
            BlockedStatus status,
            int version) : base(key, version)
        {
            Status = status;
        }
    }
}
