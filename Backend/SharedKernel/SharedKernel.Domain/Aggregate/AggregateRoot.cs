using SharedKernel.Domain.Event;
using SharedKernel.Domain.ValueObjects;

namespace SharedKernel.Domain.Aggregate
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> _changes = new List<DomainEvent>();

        public AggregateKey? Key { get; protected set; }
        public int Version { get; protected set; }




        public IEnumerable<DomainEvent> GetUncomittedChanges()
        {
            lock (_changes)
            {
                return _changes.ToArray();
            }
        }
        public void MarkChangesAsCommitted()
        {
            lock (_changes)
            {
                _changes.Clear();
            }
        }
        public void LoadFromHistory(IEnumerable<DomainEvent> history)
        {
            foreach (var e in history)
            {
                //if (e.Version != Version + 1)
                //throw new EventsOutOfOrderException(e.Key);
                ApplyChange(e, false);
            }
        }

        protected void ApplyChange(DomainEvent @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(DomainEvent @event, bool isNew)
        {
            lock (_changes)
            {
                this.AsDynamic()?.Apply(@event);

                if (isNew)
                {
                    _changes.Add(@event);
                    //   Version = ++@event.Version;
                }
                else
                {
                    Key = @event.Key;
                    //   Version++;
                }
                Version++;
                Key = @event.Key;
            }
        }



    }
}