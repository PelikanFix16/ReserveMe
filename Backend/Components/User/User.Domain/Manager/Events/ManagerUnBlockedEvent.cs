using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace User.Domain.Manager.Events
{
    public class ManagerUnBlockedEvent : DomainEvent
    {
        public BlockedStatus Status { get; private set; }
        public ManagerUnBlockedEvent(
            ManagerId key,
            BlockedStatus status,
            int version) : base(key, version)
        {
            Status = status;
        }
    }
}
