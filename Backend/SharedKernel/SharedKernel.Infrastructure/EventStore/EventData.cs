using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using SharedKernel.Domain.Event;

namespace SharedKernel.Infrastructure.EventStore
{
    public class EventData
    {
        public const string IdFieldName = "id";
        public const string StreamIdFieldname = "streamId";
        public const string VersionFieldname = "version";

        [BsonElement(IdFieldName)]
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id {get;set;}

        [BsonElement(StreamIdFieldname)]
        public string? StreamId {get;set;}

        [BsonElement(VersionFieldname)]
        public int Version {get;set;}

        [BsonElement("payload")]
        public DomainEvent? PayLoad {get;set;}

        [BsonElement("timestamp")]
        public DateTimeOffset Timestamp {get;set;}

        [BsonElement("clrTypeFullName")]
        public string? AssemblyQualifiedName {get;set;}

    }
}