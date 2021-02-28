using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.EventSourcing.Exceptions
{
    public class AggregateNotFoundException : System.Exception
    {
        public AggregateNotFoundException(AggregateKey id)
            : base(string.Format("Aggregate {0} was not found", id))
        {
        }
    }
}
