using GeekLemonConference.Domain;
using GeekLemonConference.DomainEvents.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.EventSourcing.Messages
{
    public interface IHandler<T> where T : IMessage
    {
        void Handle(T message);
    }
}
