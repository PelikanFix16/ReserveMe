using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController
{
    public interface IEventController
    {
        Task SaveAsync(IEnumerable<DomainEvent> events);
        Task<IEnumerable<DomainEvent>> GetAsync(AggregateKey key);
    }
}
