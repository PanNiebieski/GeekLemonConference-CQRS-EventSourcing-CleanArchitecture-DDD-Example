using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.EventSourcing.Exceptions
{
    public class EventsOutOfOrderException : System.Exception
    {
        public EventsOutOfOrderException(AggregateKey id)
            : base(string.Format("Eventstore gave event for aggregate {0} out of order", id))
        {
        }
    }
}
