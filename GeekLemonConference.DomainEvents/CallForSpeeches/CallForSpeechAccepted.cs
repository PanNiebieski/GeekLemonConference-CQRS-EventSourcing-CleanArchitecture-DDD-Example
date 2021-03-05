using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using GeekLemonConference.DomainEvents.Ddd;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Application.EventSourcing;

namespace GeekLemonConference.Domain.DomainEvents
{
    public class CallForSpeechAcceptedEvent : DomainEvent
    {


        public Speaker Speaker { get; init; }
        public Speech Speech { get; init; }
        public Registration Registration { get; init; }
        public CallForSpeechNumber Number { get; init; }

        public Category Category { get; init; }
        public CallForSpeechStatus Status { get; init; }

        public CallForSpeechScoringResult ScoreResult { get; init; }

        public Decision PreliminaryDecision { get; init; }
        public Decision FinalDecision { get; init; }

        public CallForSpeechUniqueId UniqueId { get; init; }



        public CallForSpeechAcceptedEvent(Speaker speaker, Speech speech,
            Registration registration, CallForSpeechNumber number, Category category,
            CallForSpeechStatus status, CallForSpeechScoringResult scoreResult,
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
            ScoreResult = scoreResult;
            PreliminaryDecision = preliminaryDecision;
            FinalDecision = finalDecision;
            UniqueId = uniqueId;
            this.Key = UniqueId.GetAggregateKey();
        }



    }
}
