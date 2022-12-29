using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using User.Domain.ValueObjects;

namespace User.Domain.Manager.Events
{
    public class ManagerAddressChangedEvent : DomainEvent
    {
        public Address Address { get; private set; }

        public ManagerAddressChangedEvent(
            ManagerId key,
            Address localAddress,
            int version)
            : base(key, version)
        {
            Address = localAddress;
        }
    }
}
