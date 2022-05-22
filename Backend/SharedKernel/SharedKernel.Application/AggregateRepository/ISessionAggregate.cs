using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.AggregateRepository
{
    public interface ISessionAggregate
    {
         void Add<T,TK>(T aggregate,TK key) where T : AggregateRoot where TK : AggregateKey; 
         T Get<T>(AggregateKey key,int? exceptedVersion = null) where T : AggregateRoot,new();
         bool Commit();
    }
}