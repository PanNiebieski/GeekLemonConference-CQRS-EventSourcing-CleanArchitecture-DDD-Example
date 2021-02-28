using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.DomainEvents.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.EventSourcing.Contracts
{
    public interface IBusEventHandler
    {
        Type HandlerType { get; }
        void Handle(DomainEvent @event);
    }
}
