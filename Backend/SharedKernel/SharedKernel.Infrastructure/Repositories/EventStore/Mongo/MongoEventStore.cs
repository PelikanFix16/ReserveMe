using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
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
        private static readonly object s_lock = new();

        public MongoEventStore(MongoSettings settings)
        {
            lock (s_lock)
            {
                if (!BsonClassMap.IsClassMapRegistered(typeof(DomainEvent)))
                {
                    BsonClassMap.RegisterClassMap<DomainEvent>(cm =>
                    {
                        cm.AutoMap();
                        cm.MapMember(c => c.Version);
                        cm.MapMember(c => c.Key);
                    });
                    var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
                    ConventionRegistry.Register("IgnoreExtraElements", conventionPack, _ => true);
                }
            }

            var mongoClient = new MongoClient(settings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);
            _collection = mongoDatabase.GetCollection<DomainEvent>(settings.CollectionName);
        }

        public async Task<IEnumerable<DomainEvent>> GetAsync(AggregateKey key)
        {
            var filter = Builders<DomainEvent>.Filter.Eq("Key", key);
            return await _collection.Find(filter).SortBy(b => b.Version).ToListAsync();
        }

        public async Task SaveAsync(DomainEvent @event)
        {
            await _collection.InsertOneAsync(@event);
        }
    }
}
