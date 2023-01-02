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
        public Email Email { get; }

        public ManagerCreatedEvent(
            ManagerId key,
            Email email,
            int version)
            : base(key,version) => Email = email;
    }
}
