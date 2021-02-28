using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.DomainEvents.CallForSpeeches;
using GeekLemonConference.DomainEvents.Categories;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.EventStorePlugin.MongoDb
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoDatabase Database { get; }

        public IClientSessionHandle Session { get; private set; }

        private MongoClient _client;

        public MongoDbContext()
        {

            string connectionString = "mongodb://localhost:27017/dbtest?readPreference=primary";
            var mongoUrl = new MongoUrl(connectionString);
            _client = new MongoClient(mongoUrl);
            Database = _client.GetDatabase("GeekLemon");
            ClassMapping();
        }

        private static void ClassMapping()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(CategoryUpdateEvent))) { BsonClassMap.RegisterClassMap<CategoryUpdateEvent>(); }
            if (!BsonClassMap.IsClassMapRegistered(typeof(CategoryCreateEvent))) { BsonClassMap.RegisterClassMap<CategoryCreateEvent>(); }
            if (!BsonClassMap.IsClassMapRegistered(typeof(JudgeCreatedEvent))) { BsonClassMap.RegisterClassMap<JudgeCreatedEvent>(); }
            if (!BsonClassMap.IsClassMapRegistered(typeof(JudgeUpdatedEvent))) { BsonClassMap.RegisterClassMap<JudgeUpdatedEvent>(); }
            if (!BsonClassMap.IsClassMapRegistered(typeof(JudgeDeletedEvent))) { BsonClassMap.RegisterClassMap<JudgeDeletedEvent>(); }
            if (!BsonClassMap.IsClassMapRegistered(typeof(CallForSpeechAcceptedEvent))) { BsonClassMap.RegisterClassMap<CallForSpeechAcceptedEvent>(); }
            if (!BsonClassMap.IsClassMapRegistered(typeof(CallForSpeechPreliminaryAcceptEvent))) { BsonClassMap.RegisterClassMap<CallForSpeechPreliminaryAcceptEvent>(); }
            if (!BsonClassMap.IsClassMapRegistered(typeof(CallForSpeechRejectedEvent))) { BsonClassMap.RegisterClassMap<CallForSpeechRejectedEvent>(); }
            if (!BsonClassMap.IsClassMapRegistered(typeof(CallForSpeechEvaulatedEvent))) { BsonClassMap.RegisterClassMap<CallForSpeechEvaulatedEvent>(); }
            if (!BsonClassMap.IsClassMapRegistered(typeof(CallForSpeechSubmitEvent))) { BsonClassMap.RegisterClassMap<CallForSpeechSubmitEvent>(); }
        }

        public IClientSessionHandle StartSession()
        {
            var session = _client.StartSession();
            Session = session;
            return session;
        }
    }
}
