
using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.DomainEvents.Ddd;
using GeekLemonConference.Infrastructure.EventStorePlugin.MongoDb;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.EventStore.MongoDb
{
    public class MongoDbEventStore : IEventStore
    {
        private IMongoCollection<EventData> _events;

        private const string EventsCollection = "eventstore";

        private readonly IMongoDbContext _mongoDbContext;

        public MongoDbEventStore(IEventPublisher publisher, IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _events = _mongoDbContext.Database.GetCollection<EventData>(EventsCollection);
        }

        public List<DomainEvent> Get(Application.EventSourcing.AggregateKey aggregateId, int fromVersion)
        {
            try
            {
                var filterBuilder = Builders<EventData>.Filter;
                var filter = filterBuilder.Eq(EventData.StreamIdFieldName, aggregateId) &
                            filterBuilder.Gte(EventData.VersionFieldName, fromVersion);

                //#ToFix Make this async
                //var result = await _events.FindAsync(filter, cancellationToken: cancellationToken);
                //return (await result.ToListAsync(cancellationToken)).Select(x => x.PayLoad);

                var result = _events.Find(filter);
                var r = result.ToList().Select(x => x.PayLoad).ToList();
                return r;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save(DomainEvent @event)
        {
            using var session = _mongoDbContext.StartSession();

            try
            {
                //You can do atomic mass add
                var eventData = new EventData
                {
                    Id = Guid.NewGuid(),
                    StreamId = @event.Key.Id,
                    TimeStamp = @event.TimeStamp,
                    AssemblyQualifiedName = @event.GetType().AssemblyQualifiedName,
                    PayLoad = @event,
                    Version = @event.Version
                };

                _events.InsertOne(eventData);
            }
            catch (Exception exp)
            {
                session.AbortTransaction();
                throw;
            }
        }
    }
}



