using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using GeekLemonConference.DomainEvents.Ddd;
using GeekLemonConference.Domain.ValueObjects.Security;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Application.EventSourcing;

namespace GeekLemonConference.Domain.DomainEvents
{
    public class JudgeCreatedEvent : DomainEvent
    {
        public Login Login { get; init; }
        public Password Password { get; init; }
        public Name Name { get; init; }
        public Category Category { get; init; }
        public DateTime Birthdate { get; init; }
        public JudgeUniqueId UniqueId { get; init; }

        public JudgeCreatedEvent(DateTime birthdate,
            Category category, JudgeUniqueId uniqueId,
            Login login, Password password, Name name,
            int version) :
            base(version)
        {
            Birthdate = birthdate;
            Category = category;
            UniqueId = uniqueId;
            Login = login;
            Name = name;
            Password = password;
            Key = this.UniqueId.GetAggregateKey();
            Version = version;
        }

        public JudgeCreatedEvent()
        {

        }


    }
}
