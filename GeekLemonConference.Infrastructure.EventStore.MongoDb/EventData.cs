using GeekLemonConference.DomainEvents.Ddd;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace GeekLemonConference.Infrastructure.EventStore.MongoDb
{
    public class EventData
    {
        public const string IdFieldName = "_id";
        public const string StreamIdFieldName = "_streamId";
        public const string VersionFieldName = "_version";

        [BsonElement(IdFieldName)]
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement(StreamIdFieldName)]
        public string StreamId { get; set; }

        [BsonElement(VersionFieldName)]
        public int Version { get; set; }

        [BsonElement("_payload")]
        public DomainEvent PayLoad { get; set; }

        [BsonElement("_timestamp")]
        public DateTimeOffset TimeStamp { get; set; }

        [BsonElement("_clrTypeFullname")]
        public string AssemblyQualifiedName { get; set; }
    }
}
