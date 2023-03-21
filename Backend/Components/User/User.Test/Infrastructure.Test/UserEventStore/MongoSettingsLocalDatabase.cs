using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SharedKernel.Infrastructure.EventStore;

namespace Infrastructure.Test.UserEventStore
{
    public class MongoSettingsLocalDatabase
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string DatabaseName = "EventStore";
        private const string CollectionName = "UserEvents";

        public static IOptions<MongoSettings> GetConfig() => Options.Create(
                new MongoSettings
                {
                    ConnectionString = ConnectionString,
                    DatabaseName = DatabaseName,
                    CollectionName = CollectionName
                });
    }
}
