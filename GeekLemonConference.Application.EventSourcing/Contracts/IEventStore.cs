using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.DomainEvents.Ddd;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.EventSourcing.Contracts
{
    public interface IEventStore
    {
        void Save(DomainEvent @event);
        List<DomainEvent> Get(AggregateKey aggregateId, int fromVersion);
    }
}
