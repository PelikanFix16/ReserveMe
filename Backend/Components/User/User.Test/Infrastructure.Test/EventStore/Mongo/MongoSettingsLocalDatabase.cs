using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Infrastructure.Repositories.EventStore.Mongo;

namespace Infrastructure.Test.EventStore.Mongo
{
    public static class MongoSettingsLocalDatabase
    {
        private static string ConnectionString => "mongodb://localhost:27017";
        private static string DatabaseName => "EventStore";
        private static string CollectionName => "UserEvents";

        public static MongoSettings GetConfig()
        {
            return new MongoSettings
            {
                ConnectionString = ConnectionString,
                DatabaseName = DatabaseName,
                CollectionName = CollectionName
            };
        }
    }
}
