using MongoDB.Driver;
using SharedKernel.Application;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Infrastructure.EventStore
{
    public class MongoDbEventStore : IEventStore
    {
        private readonly IMongoDbContext mongoDbContext;
        private const string EventsCollection = "eventstore";
        private IMongoCollection<EventData> events;

        public MongoDbEventStore(IMongoDbContext context)
        {
            mongoDbContext = context;
            events = mongoDbContext.Database.GetCollection<EventData>(EventsCollection);
        }

        public List<DomainEvent> Get(AggregateKey aggregateKey, int fromVersion)
        {
            try
            {
                var filterBuilder = Builders<EventData>.Filter;
                var filter = filterBuilder.Eq(EventData.StreamIdFieldname,aggregateKey.Key.ToString()) & 
                                        filterBuilder.Gte(EventData.VersionFieldname,fromVersion);
                var result = events.Find(filter);
                var r = result.ToList().Select(x =>x.PayLoad).ToList();
                return r as List<DomainEvent>;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Save(DomainEvent @event)
        {
            using var session = mongoDbContext.StartSession();
            try{
                var eventData = new EventData{
                    Id = Guid.NewGuid(),
                    StreamId = @event.Key.Key.ToString(),
                    Timestamp = @event.TimeStamp,
                    AssemblyQualifiedName = @event.GetType().AssemblyQualifiedName,
                    PayLoad = @event,
                    Version = @event.Version

                };
                events.InsertOne(eventData);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}