namespace SharedKernel.Application.AggregateRepository
{
    public interface IEventRepository
    {
         void Save<T> (T aggregate, int? exceptedVersion=null) where T : AggregateRoot;
         T Get<T> (AggregateKey aggregateId) where T : AggregateRoot;
    }
}