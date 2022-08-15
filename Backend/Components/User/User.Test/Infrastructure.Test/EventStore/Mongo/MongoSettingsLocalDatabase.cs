using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SharedKernel.Infrastructure.Repositories.EventStore.Mongo;

namespace Infrastructure.Test.EventStore.Mongo
{
    public static class MongoSettingsLocalDatabase
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string DatabaseName = "EventStore";
        private const string CollectionName = "UserEvents";

        public static IOptions<MongoSettings> GetConfig()
        {
            return Options.Create(
                new MongoSettings
                {
                    ConnectionString = ConnectionString,
                    DatabaseName = DatabaseName,
                    CollectionName = CollectionName
                });
        }
    }
}
