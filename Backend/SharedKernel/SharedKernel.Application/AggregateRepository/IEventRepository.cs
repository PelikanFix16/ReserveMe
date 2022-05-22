using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.AggregateRepository
{
    public interface IEventRepository
    {
         void Save<T> (AggregateKey key,T aggregate, int? exceptedVersion=null) where T : AggregateRoot;
         AggregateRoot Get<T> (AggregateKey aggregateId) where T : AggregateRoot,new();
    }
}