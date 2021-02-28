﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.EventSourcing.Exceptions
{
    public class AggregateOrEventMissingIdException : System.Exception
    {
        public AggregateOrEventMissingIdException(Type aggregateType, Type eventType)
            : base(string.Format("An event of type {0} was tried to save from {1} but no id where set on either", eventType.FullName, aggregateType.FullName))
        {

        }
    }
}
