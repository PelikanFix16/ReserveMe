using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using SharedKernel.InterfaceAdapters.Dto;
using SharedKernel.SharedKernel.InterfaceAdapters.Interfaces.EventStore;

namespace SharedKernel.Infrastructure.Repositories.EventStore.Mongo
{
    public class MongoEventStore : IEventStoreRepository
    {
        private readonly IMongoCollection<StoreEvent> _collection;
        private static readonly object s_lock = new();
        private readonly MongoSettings _settings;

        public MongoEventStore(IOptions<MongoSettings> settings)
        {
            _settings = settings.Value;
            lock (s_lock)
            {
                if (!BsonClassMap.IsClassMapRegistered(typeof(StoreEvent)))
                {
                    BsonClassMap.RegisterClassMap<StoreEvent>(cm => cm.AutoMap());
                    var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
                    ConventionRegistry.Register("IgnoreExtraElements", conventionPack, _ => true);
                }
            }

            var mongoClient = new MongoClient(_settings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_settings.DatabaseName);
            _collection = mongoDatabase.GetCollection<StoreEvent>(_settings.CollectionName);
        }

        public async Task<IEnumerable<StoreEvent>> GetAsync(EventKey key)
        {
            var filter = Builders<StoreEvent>.Filter.Eq("EventId", key.Key);
            return await _collection.Find(filter).SortBy(b => b.Version).ToListAsync();
        }

        public async Task SaveAsync(StoreEvent @event)
        {
            await _collection.InsertOneAsync(@event);
        }
    }
}
