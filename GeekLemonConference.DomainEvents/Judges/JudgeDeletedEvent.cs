using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using GeekLemonConference.DomainEvents.Ddd;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Application.EventSourcing;

namespace GeekLemonConference.Domain.DomainEvents
{
    public class JudgeDeletedEvent : DomainEvent
    {
        public JudgeUniqueId UniqueId { get; init; }

        public JudgeDeletedEvent(int version) :
            base(version)
        {


        }


        public JudgeDeletedEvent()
        {

        }

    }
}
