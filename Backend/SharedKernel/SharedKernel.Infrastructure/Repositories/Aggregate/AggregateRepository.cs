using SharedKernel.Application.Repositories.Aggregate;
using SharedKernel.Application.Repositories.Exceptions;
using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Infrastructure.Repositories.Aggregate
{
    public class AggregateRepository : IAggregateRepository
    {

        private IDictionary<AggregateKey, AggregateRoot> _aggregates;
        private IEventRepository _eventRepository;

        public AggregateRepository(IEventRepository eventRepository)
        {
            _aggregates = new Dictionary<AggregateKey, AggregateRoot>();
            _eventRepository = eventRepository;
        }

        public async Task<bool> Commit()
        {
            foreach (var item in _aggregates)
            {
                IEnumerable<DomainEvent> events = item.Value.GetUncomittedChanges();
                if (!await _eventRepository.Save(events))
                    return false;
                _aggregates.Remove(item);
            }
            return true;
        }

        public async Task<T> Get<T>(AggregateKey key) where T : AggregateRoot, new()
        {
            T aggregate = new T();
            if (_aggregates.ContainsKey(key))
            {
                IEnumerable<DomainEvent> _events = _aggregates[key].GetUncomittedChanges();
                aggregate.LoadFromHistory(_events);
                return aggregate;
            }

            IEnumerable<DomainEvent> events = await _eventRepository.Get(key);
            aggregate.LoadFromHistory(events);
            return aggregate;

        }

        public void Save(AggregateRoot aggregate, AggregateKey key)
        {
            if (!CheckVersionAggregate(aggregate, key))
                throw new AggregateVersionException("Version of aggregate cannot be lower than current");

            _aggregates[key] = aggregate;

        }

        private bool CheckVersionAggregate(AggregateRoot aggregate, AggregateKey key)
        {
            AggregateRoot? rootCheckAggregate = null;
            foreach (var item in _aggregates)
            {
                if (item.Key == key)
                    rootCheckAggregate = item.Value;
            }

            if (rootCheckAggregate == null || rootCheckAggregate.Version < aggregate.Version)
                return true;
            else
                return false;

        }

    }
}