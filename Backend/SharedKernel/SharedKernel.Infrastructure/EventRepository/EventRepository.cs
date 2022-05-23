using SharedKernel.Application;
using SharedKernel.Application.AggregateRepository;
using SharedKernel.Application.EventBus;
using SharedKernel.Application.Exceptions;
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
                throw new AggregateNotFoundException(key);
            aggregate.LoadFromHistory(events);
            return aggregate;
        }

        public void Save<T>(AggregateKey key,T aggregate, int? exceptedVersion = null) where T : AggregateRoot
        {
            if(exceptedVersion != null && _eventStore.Get(key,exceptedVersion.Value).Any())
                throw new ConcurrencyException(key);
            foreach(var @event in aggregate.GetUncomittedChanges()){
                if(@event.Key.Key == AggregateKey.Empty)
                    throw new AggregateOrEventMissingIdException(aggregate.GetType(),@event.GetType());
                _eventStore.Save(@event);
                _eventPublisher.Publish(@event);
            }
            aggregate.MarkChangesAsCommitted();

        }
    }
}