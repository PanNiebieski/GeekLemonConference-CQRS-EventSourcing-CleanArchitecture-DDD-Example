using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.DomainEvents.Ddd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.DomainEvents.CallForSpeeches
{
    public class CallForSpeechSubmitEvent : DomainEvent
    {
        public Speaker Speaker { get; init; }
        public Speech Speech { get; init; }
        public Registration Registration { get; init; }
        public CallForSpeechNumber Number { get; init; }

        public Category Category { get; init; }
        public CallForSpeechStatus Status { get; init; }

        public CallForSpeechScoringResult Score { get; init; }

        public Decision PreliminaryDecision { get; init; }
        public Decision FinalDecision { get; init; }

        public CallForSpeechUniqueId UniqueId { get; init; }

        public CallForSpeechSubmitEvent(Speaker speaker, Speech speech,
            Registration registration, CallForSpeechNumber number, Category category,
            CallForSpeechStatus status, CallForSpeechScoringResult score,
            Decision preliminaryDecision, Decision finalDecision, CallForSpeechUniqueId uniqueId,
            int version)
            : base(version)
        {
            Speaker = speaker;
            Speech = speech;
            Registration = registration;
            Number = number;
            Category = category;
            Status = status;
            Score = score;
            PreliminaryDecision = preliminaryDecision;
            FinalDecision = finalDecision;
            UniqueId = uniqueId;
            this.Key = UniqueId.GetAggregateKey();
        }

        public CallForSpeechSubmitEvent()
        {

        }
    }
}
