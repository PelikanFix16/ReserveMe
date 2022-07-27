using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Repositories.Aggregate
{
    public interface IEventRepository
    {
        Task SaveAsync(IEnumerable<DomainEvent> events);
        Task<IEnumerable<DomainEvent>> GetAsync(AggregateKey key);
    }
}
