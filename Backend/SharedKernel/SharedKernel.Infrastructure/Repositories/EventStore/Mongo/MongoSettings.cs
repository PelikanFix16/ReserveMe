namespace SharedKernel.Infrastructure.Repositories.EventStore.Mongo
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

    }
}