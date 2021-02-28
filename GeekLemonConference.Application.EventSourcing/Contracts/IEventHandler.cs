
using GeekLemonConference.Application.EventSourcing.Messages;
using GeekLemonConference.DomainEvents.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.EventSourcing.Contracts
{
    public interface IEventHandler<T> : IHandler<T> where T : IEvent
    {
    }
}
