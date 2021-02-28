using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.EventSourcing.Exceptions
{
    public class ConcurrencyException : System.Exception
    {
        public ConcurrencyException(AggregateKey id)
            : base(string.Format("A different version than expected was found in aggregate {0}", id))
        {
        }
    }
}
