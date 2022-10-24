using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Dto;

namespace SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController
{
    public interface IEventStoreManagerRepositories
    {
        Task SaveAsync(DomainEvent @event);
        Task<IEnumerable<DomainEvent>> GetAsync(AggregateKey key);
    }
}
