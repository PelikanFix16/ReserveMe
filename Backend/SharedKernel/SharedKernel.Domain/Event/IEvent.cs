namespace SharedKernel.Domain.Event
{
    public interface IEvent
    {
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

    }
}