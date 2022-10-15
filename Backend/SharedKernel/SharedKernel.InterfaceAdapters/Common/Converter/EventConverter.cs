using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedKernel.Application.Common.Event;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Dto;

namespace SharedKernel.InterfaceAdapters.Common.Converter
{
    public static class EventConverter
    {
        public static DomainEvent StoreToDomainEvent(StoreEvent storeEvent)
        {
            var qualified = Assembly.CreateQualifiedName(storeEvent.AssemblyName, storeEvent.EventName);
            var elementType = Type.GetType(qualified);
            if (elementType is null)
                throw new ArgumentException($"Type {storeEvent.EventName} not found");

            var domainObj = JsonConvert.DeserializeObject(storeEvent.EventData, elementType);
            if (domainObj is null)
                throw new Exception("Could not convert store event to domain event");

            if (domainObj is not DomainEvent obj)
                throw new Exception("Could not convert store event to domain event");

            return obj;
        }

        public static EventKey DomainToStoreEventKey(AggregateKey domainKey)
        {
            return new EventKey()
            {
                Key = domainKey.Key
            };
        }

        public static StoreEvent DomainToStoreEvent(DomainEvent domainEvent)
        {
            var assembly = domainEvent.GetType().Assembly.GetName().Name ?? "";

            return new StoreEvent()
            {
                EventId = domainEvent.Key.Key,
                EventName = domainEvent.GetType().Namespace + "." + domainEvent.GetType().Name,
                EventData = JsonConvert.SerializeObject(domainEvent),
                EventDate = domainEvent.TimeStamp,
                Version = domainEvent.Version,
                AssemblyName = assembly
            };
        }

        public static SharedEvent DomainToSharedEvent(DomainEvent domainEvent)
        {
            return new SharedEvent()
            {
                EventName = domainEvent.GetType().Name,
                EventData = JsonConvert.SerializeObject(domainEvent)
            };
        }
    }
}
