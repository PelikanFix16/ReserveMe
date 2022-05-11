namespace SharedKernel.Domain.ValueObjects
{
   public class AggregateKey : ValueObject
    {
        public Guid Key { get; set; }


        public AggregateKey(Guid key)
        {
            Key = key;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Key;
        }
    }
}