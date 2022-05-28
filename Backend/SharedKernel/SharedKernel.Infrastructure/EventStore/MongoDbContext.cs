using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace SharedKernel.Infrastructure.EventStore
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoDatabase Database {get;private set;}

        private MongoClient mongoClient;

        public MongoDbContext(IConfiguration configuration)
        {
            string connectionString = configuration["MongoDB"];
            var mongoUrl = new MongoUrl(connectionString);
            mongoClient = new MongoClient(mongoUrl);
            Database = mongoClient.GetDatabase(configuration["MongoDatabaseName"]);
            
        }

        public IClientSessionHandle StartSession()
        {
            var session = mongoClient.StartSession();
            return session;
        }
    }
}