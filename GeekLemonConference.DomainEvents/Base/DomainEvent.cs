using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.DomainEvents.Ddd
{
    public abstract class DomainEvent : IEvent
    {
        public AggregateKey Key { get; set; }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        protected DomainEvent(DateTimeOffset occuredOn, int version)
        {
            TimeStamp = occuredOn;
            Version = version;
        }

        protected DomainEvent(int version)
        {
            TimeStamp = AppTime.Now();
            Version = version;
        }

        protected DomainEvent()
        {

        }

    }
}
