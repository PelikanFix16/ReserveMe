using SharedKernel.Application;
using SharedKernel.Application.AggregateRepository;
using SharedKernel.Application.EventBus;
using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Infrastructure.EventRepository
{
    public class EventRepository : IEventRepository
    {

        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _eventPublisher;

        public EventRepository(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            if (eventStore == null)
                throw new ArgumentNullException("eventStore");
            if (eventPublisher == null)
                throw new ArgumentNullException("eventPublisher");
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
        }

        public AggregateRoot Get<T>(AggregateKey aggregateId) where T : AggregateRoot,new()
        {
            return LoadAggregate<T>(aggregateId);
        }

        private T LoadAggregate<T>(AggregateKey key) where T : AggregateRoot, new()
        {
            var aggregate = new T();
            var events = _eventStore.Get(key,-1);
            if(!events.Any())
                throw new InvalidDataException($"Events not found for key {key}");
            aggregate.LoadFromHistory(events);
            return aggregate;
        }

        public void Save<T>(AggregateKey key,T aggregate, int? exceptedVersion = null) where T : AggregateRoot
        {
            if(exceptedVersion != null && _eventStore.Get(key,exceptedVersion.Value).Any())
                throw new InvalidOperationException($"Event with this version already exists key {key}");
            foreach(var @event in aggregate.GetUncomittedChanges()){
                _eventStore.Save(@event);
                _eventPublisher.Publish(@event);
            }
            aggregate.MarkChangesAsCommitted();

        }
    }
}