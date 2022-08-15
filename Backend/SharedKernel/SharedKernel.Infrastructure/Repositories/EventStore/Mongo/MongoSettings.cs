namespace SharedKernel.Infrastructure.Repositories.EventStore.Mongo
{
    public class MongoSettings
    {
        public const string SectionName = "MongoDbSettings";

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
