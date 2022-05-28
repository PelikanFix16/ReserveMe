using MongoDB.Driver;

namespace SharedKernel.Infrastructure.EventStore
{
    public interface IMongoDbContext
    {
         IMongoDatabase Database {get;}
         IClientSessionHandle StartSession();
    }
}