using SharedKernel.Domain.ValueObjects;

namespace SharedKernel.Domain.UniqueKey
{
    public abstract class EntityKey : ValueObject
    {
        public Guid Key { get; private set; }

        protected EntityKey(Guid key)
        {
            Key = key;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Key;
        }
    }
}
