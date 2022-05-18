namespace SharedKernel.Application.AggregateRepository
{
    public interface ISessionAggregate
    {
         void Add<T>(T aggregate) where T : AggregateRoot; 
         T Get<T>(AggregateKey key,int? exceptedVersion = null) where T : AggregateRoot;
         bool Commit();
    }
}