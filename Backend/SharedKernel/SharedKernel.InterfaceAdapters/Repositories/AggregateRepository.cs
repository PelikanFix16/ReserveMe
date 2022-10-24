using FluentResults;
using SharedKernel.Application.Common.Errors.RepositoriesErrors;
using SharedKernel.Application.Interfaces.Repositories;
using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;

namespace SharedKernel.InterfaceAdapters.Repositories
{
    public class AggregateRepository : IAggregateRepository
    {
        private readonly IDictionary<AggregateKey, AggregateRoot> _aggregates;
        private readonly IEventController _eventRepository;

        public AggregateRepository(IEventController eventRepository)
        {
            _aggregates = new Dictionary<AggregateKey, AggregateRoot>();
            _eventRepository = eventRepository;
        }

        public async Task<Result> CommitAsync()
        {
            foreach (var item in _aggregates)
            {
                try
                {
                    var events = item.Value.GetUncommittedChanges();
                    await _eventRepository.SaveAsync(events);
                    _aggregates.Remove(item);
                }
                catch (Exception)
                {
                    return Result.Fail(new SaveEventError());
                }
            }

            return Result.Ok();
        }

        public async Task<Result<T>> GetAsync<T>(AggregateKey key) where T : AggregateRoot, new()
        {
            var aggregate = new T();
            if (_aggregates.ContainsKey(key))
            {
                var domainEvents = _aggregates[key].GetUncommittedChanges();
                aggregate.LoadFromHistory(domainEvents);
                return aggregate;
            }

            try
            {
                var events = await _eventRepository.GetAsync(key);
                if (!events.Any())
                    return Result.Fail(new AggregateNotFoundError(key));

                aggregate.LoadFromHistory(events);
                return aggregate;
            }
            catch (Exception)
            {
                return Result.Fail(new GetEventError());
            }
        }

        public Result Save(AggregateRoot aggregate, AggregateKey key)
        {
            if (!CheckVersionAggregate(aggregate, key))
                return Result.Fail(new AggregateVersionError(key));

            _aggregates[key] = aggregate;
            return Result.Ok();
        }

        private bool CheckVersionAggregate(AggregateRoot aggregate, AggregateKey key)
        {
            AggregateRoot rootCheckAggregate = null!;
            foreach (var item in _aggregates)
            {
                if (item.Key == key)
                    rootCheckAggregate = item.Value;
            }

            return rootCheckAggregate == null || rootCheckAggregate.Version < aggregate.Version;
        }
    }
}
