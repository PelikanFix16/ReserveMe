using SharedKernel.Application.Repositories.Aggregate;
using SharedKernel.Application.Repositories.Exceptions;
using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Infrastructure.Repositories.Aggregate
{
    public class AggregateRepository : IAggregateRepository
    {
        private readonly IDictionary<AggregateKey, AggregateRoot> _aggregates;
        private readonly IEventRepository _eventRepository;

        public AggregateRepository(IEventRepository eventRepository)
        {
            _aggregates = new Dictionary<AggregateKey, AggregateRoot>();
            _eventRepository = eventRepository;
        }

        public async Task<bool> CommitAsync()
        {
            foreach (var item in _aggregates)
            {
                var events = item.Value.GetUncommittedChanges();
                await _eventRepository.SaveAsync(events);
                _aggregates.Remove(item);
            }

            return true;
        }

        public async Task<T> GetAsync<T>(AggregateKey key) where T : AggregateRoot, new()
        {
            var aggregate = new T();
            if (_aggregates.ContainsKey(key))
            {
                var domainEvents = _aggregates[key].GetUncommittedChanges();
                aggregate.LoadFromHistory(domainEvents);
                return aggregate;
            }

            var events = await _eventRepository.GetAsync(key);
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
            AggregateRoot rootCheckAggregate = null;
            foreach (var item in _aggregates)
            {
                if (item.Key == key)
                    rootCheckAggregate = item.Value;
            }

            return rootCheckAggregate == null || rootCheckAggregate.Version < aggregate.Version;
        }
    }
}
