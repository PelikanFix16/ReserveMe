using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Repositories.Aggregate
{
    public interface IEventRepository
    {
        Task Save(IEnumerable<DomainEvent> events);
        Task<IEnumerable<DomainEvent>> Get(AggregateKey key);
    }
}