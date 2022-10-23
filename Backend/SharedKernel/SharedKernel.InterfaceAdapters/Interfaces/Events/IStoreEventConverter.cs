using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Dto;

namespace SharedKernel.InterfaceAdapters.Interfaces.Events
{
    public interface IStoreEventConverter
    {
        public StoreEvent DomainToStoreEvent(DomainEvent domainEvent);
        public DomainEvent StoreToDomainEvent(StoreEvent storeEvent);
        public EventKey AggregateKeyToEventKey(AggregateKey key);
    }
}
