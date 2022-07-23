using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SharedKernel.Application.Repositories.EventStore;
using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Infrastructure.Repositories.EventStore.Mongo
{
    public class MongoEventStore : IEventStoreRepository
    {
        private readonly IMongoCollection<DomainEvent> _collection;
        public MongoEventStore(MongoSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);
            BsonClassMap.RegisterClassMap<DomainEvent>(cm =>
            {
                cm.MapIdField(c => c.Key);
            });
            _collection = mongoDatabase.GetCollection<DomainEvent>(settings.CollectionName);
        }
        public async Task<IEnumerable<DomainEvent>> Get(AggregateKey key)
        {
            var filter = Builders<DomainEvent>.Filter.Eq("Key", key);
            var events = await _collection.Find(filter).ToListAsync();
            return events;
        }

        public async Task Save(DomainEvent @event)
        {
            await _collection.InsertOneAsync(@event);
        }
    }
}
