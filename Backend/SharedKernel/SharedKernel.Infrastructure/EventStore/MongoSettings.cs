using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedKernel.Infrastructure.EventStore
{
    public class MongoSettings
    {
        public const string SectionName = "MongoDbSettings";

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
