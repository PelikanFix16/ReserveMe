using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using User.Domain.ValueObjects;

namespace User.Domain.Manager.Events
{
    public class ManagerEmailChangedEvent : DomainEvent
    {
        public Email Email { get; private set; }

        public ManagerEmailChangedEvent(
            ManagerId key,
            Email email,
            int version) : base(key, version)
        {
            Email = email;
        }
    }
}
