using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Application.EventSourcing.Exceptions;
using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekLemon.Infrastructure.Write.MongoDB
{
    public class EventRepository : IEventRepository
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _publisher;

        public EventRepository(IEventStore eventStore, IEventPublisher publisher)
        {
            if (eventStore == null)
                throw new ArgumentNullException("eventStore");
            if (publisher == null)
                throw new ArgumentNullException("publisher");
            _eventStore = eventStore;
            _publisher = publisher;
        }

        public void Save<T>(T aggregate, int? expectedVersion = null) where T : AggregateRoot
        {
            if (expectedVersion != null && _eventStore.Get(
                    aggregate.Key, expectedVersion.Value).Any())
                throw new ConcurrencyException(aggregate.Key);
            var i = 0;
            foreach (var @event in aggregate.GetUncommittedChanges())
            {
                if (@event.Key == AggregateKey.Empty)
                    @event.Key = aggregate.Key;
                if (@event.Key == AggregateKey.Empty)
                    throw new AggregateOrEventMissingIdException(
                        aggregate.GetType(), @event.GetType());
                i++;
                @event.Version = aggregate.Version + i;
                @event.TimeStamp = DateTimeOffset.UtcNow;
                _eventStore.Save(@event);
                _publisher.Publish(@event);
            }
            aggregate.MarkChangesAsCommitted();
        }

        public T Get<T>(AggregateKey aggregateId) where T : AggregateRoot
        {
            return LoadAggregate<T>(aggregateId);
        }

        private T LoadAggregate<T>(AggregateKey id) where T : AggregateRoot
        {
            var aggregate = GeekLemonConference.Application.EventSourcing.AggregateFactory.CreateAggregate<T>();

            //#ToFix
            var events = _eventStore.Get(id, -1);
            if (!events.Any())
                throw new AggregateNotFoundException(id);

            aggregate.LoadFromHistory(events);
            return aggregate;
        }
    }
}
