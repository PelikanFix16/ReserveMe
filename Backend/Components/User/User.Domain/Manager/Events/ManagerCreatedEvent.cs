using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using User.Domain.ValueObjects;

namespace User.Domain.Manager.Events
{
    public class ManagerCreatedEvent : DomainEvent
    {
        public Address Address { get; private set; }
        public Email Email { get; private set; }

        public ManagerCreatedEvent(
            ManagerId key,
            Address address,
            Email email,
            int version)
            : base(key, version)
        {
            Address = address;
            Email = email;
        }
    }
}
