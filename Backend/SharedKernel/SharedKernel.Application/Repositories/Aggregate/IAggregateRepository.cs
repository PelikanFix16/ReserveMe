using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Repositories.Aggregate
{
    public interface IAggregateRepository
    {
        void Save(AggregateRoot aggregate, AggregateKey key);
        Task<T> GetAsync<T>(AggregateKey key) where T : AggregateRoot, new();
        Task<bool> CommitAsync();
    }
}
