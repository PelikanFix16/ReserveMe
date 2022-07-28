using SharedKernel.Domain.ValueObjects;

namespace SharedKernel.Domain.UniqueKey
{
    public abstract class AggregateKey : ValueObject
    {
        public Guid Key { get; private set; }

        protected AggregateKey(Guid key)
        {
            Key = key;
        }

        public static Guid Empty => Guid.Empty;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Key;
        }
    }
}
