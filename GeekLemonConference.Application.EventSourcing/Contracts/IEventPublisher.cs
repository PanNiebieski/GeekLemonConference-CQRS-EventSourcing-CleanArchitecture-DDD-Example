using GeekLemonConference.DomainEvents.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.EventSourcing.Contracts
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : DomainEvent;
    }
}
