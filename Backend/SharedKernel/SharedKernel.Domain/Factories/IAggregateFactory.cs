using SharedKernel.Domain.Aggregate;

namespace SharedKernel.Domain.Factories
{
    public interface IAggregateFactory<T> where T : AggregateRoot
    {
        T Create();
    }
}