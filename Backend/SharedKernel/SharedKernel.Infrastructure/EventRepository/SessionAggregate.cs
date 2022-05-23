using SharedKernel.Application.AggregateRepository;
using SharedKernel.Application.Exceptions;
using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Infrastructure.EventRepository
{
    public class SessionAggregate : ISessionAggregate
    {

        private readonly IEventRepository _aggregateRepository;
        private readonly Dictionary<AggregateKey, AggregateRoot> _aggregateStore;

        public SessionAggregate(IEventRepository eventRepository)
        {
            if (eventRepository == null)
                throw new ArgumentNullException("eventRepository");
            _aggregateRepository = eventRepository;
            _aggregateStore = new Dictionary<AggregateKey, AggregateRoot>();
        }

        public void Add<T, TK>(T aggregate, TK key)
            where T : AggregateRoot
            where TK : AggregateKey
        {

            if (!IsTracked(key))
            {
                _aggregateStore.Add(key, aggregate);
            }
            else if (_aggregateStore[key] != aggregate)
                throw new ConcurrencyException(key);

        }

        private bool IsTracked(AggregateKey key) => _aggregateStore.ContainsKey(key);

        public bool Commit()
        {

            foreach (var key in _aggregateStore)
            {
                _aggregateRepository.Save(key.Key, key.Value, key.Value.Version);
            }
            _aggregateStore.Clear();
            return true;


        }

        public T Get<T>(AggregateKey key, int? exceptedVersion = null) where T : AggregateRoot, new()
        {
            if (IsTracked(key))
            {
                var trackedAggregate = (T)_aggregateStore[key];
                if (exceptedVersion != null && trackedAggregate.Version != exceptedVersion)
                    throw new ConcurrencyException(key);
                return trackedAggregate;
            }
            var aggregate = _aggregateRepository.Get<T>(key);
            if (exceptedVersion != null && aggregate.Version != exceptedVersion)
                throw new ConcurrencyException(key);
            Add(aggregate, key);
            return (T)aggregate;
        }

    }

}

