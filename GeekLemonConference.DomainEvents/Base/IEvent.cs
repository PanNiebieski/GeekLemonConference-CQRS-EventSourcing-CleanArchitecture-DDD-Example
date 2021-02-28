using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.DomainEvents.Ddd
{
    public interface IEvent : IMessage
    {

        public int Version { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}
