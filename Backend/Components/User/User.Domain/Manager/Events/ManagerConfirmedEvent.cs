using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace User.Domain.Manager.Events
{
    public class ManagerConfirmedEvent : DomainEvent
    {
        public ManagerStatus Status { get; private set; }

        public ManagerConfirmedEvent(
            ManagerId key,
            ManagerStatus status,
            int version)
            : base(key, version)
        {
            Status = status;
        }
    }
}
