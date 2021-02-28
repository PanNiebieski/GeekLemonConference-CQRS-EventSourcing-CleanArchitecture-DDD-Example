using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.DomainEvents.CallForSpeeches;
using GeekLemonConference.DomainEvents.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.EventSourcing.Aggregates
{
    public class CallForSpeechAggregate : AggregateRoot
    {
        public Speaker Speaker { get; set; }
        public Speech Speech { get; set; }
        public Registration Registration { get; set; }
        public CallForSpeechNumber Number { get; set; }

        public Category Category { get; set; }
        public CallForSpeechStatus Status { get; set; }

        public CallForSpeechScoringResult Score { get; set; }

        public CallForSpeechUniqueId UniqueId { get; set; }

        public Decision PreliminaryDecision { get; set; }
        public Decision FinalDecision { get; set; }

        private void Apply(CallForSpeechSubmitEvent e)
        {
            Speaker = e.Speaker;
            Speech = e.Speech;
            Registration = e.Registration;
            Number = e.Number;
            Category = e.Category;
            Status = e.Status;
            PreliminaryDecision = e.PreliminaryDecision;
            FinalDecision = e.FinalDecision;
            UniqueId = e.UniqueId;
            Version = e.Version++;
            this.Key = e.UniqueId.GetAggregateKey();
        }

        private void Apply(CallForSpeechRejectedEvent e)
        {
            Speaker = e.Speaker;
            Speech = e.Speech;
            Registration = e.Registration;
            Number = e.Number;
            Category = e.Category;
            Status = e.Status;
            PreliminaryDecision = e.PreliminaryDecision;
            FinalDecision = e.FinalDecision;
            UniqueId = e.UniqueId;
            Version = e.Version++;
            this.Key = e.UniqueId.GetAggregateKey();
        }

        private void Apply(CallForSpeechPreliminaryAcceptEvent e)
        {
            Speaker = e.Speaker;
            Speech = e.Speech;
            Registration = e.Registration;
            Number = e.Number;
            Category = e.Category;
            Status = e.Status;
            PreliminaryDecision = e.PreliminaryDecision;
            FinalDecision = e.FinalDecision;
            UniqueId = e.UniqueId;
            Version = e.Version++;
            this.Key = e.UniqueId.GetAggregateKey();
        }


        private void Apply(CallForSpeechAcceptedEvent e)
        {
            Speaker = e.Speaker;
            Speech = e.Speech;
            Registration = e.Registration;
            Number = e.Number;
            Category = e.Category;
            Status = e.Status;
            PreliminaryDecision = e.PreliminaryDecision;
            FinalDecision = e.FinalDecision;
            UniqueId = e.UniqueId;
            Version = e.Version++;
            this.Key = e.UniqueId.GetAggregateKey();
        }

        private void Apply(CallForSpeechEvaulatedEvent e)
        {
            Speaker = e.Speaker;
            Speech = e.Speech;
            Registration = e.Registration;
            Number = e.Number;
            Category = e.Category;
            Status = e.Status;
            PreliminaryDecision = e.PreliminaryDecision;
            FinalDecision = e.FinalDecision;
            UniqueId = e.UniqueId;
            Version = e.Version++;
            this.Key = e.UniqueId.GetAggregateKey();
        }



        public CallForSpeechAggregate(CallForSpeech cc)
        {
            var c = new CallForSpeechSubmitEvent
                (cc.Speaker, cc.Speech, cc.Registration,
                cc.Number, cc.Category, cc.Status, cc.Score,
                cc.PreliminaryDecision, cc.FinalDecision,
                cc.UniqueId, cc.Version);
            this.Key = c.UniqueId.GetAggregateKey();
            ApplyChange(c);
        }

        public void Rejected(CallForSpeech cc)
        {
            var c = new CallForSpeechRejectedEvent
                (cc.Speaker, cc.Speech, cc.Registration,
                cc.Number, cc.Category, cc.Status, cc.Score,
                cc.PreliminaryDecision, cc.FinalDecision,
                cc.UniqueId, cc.Version);
            this.Key = c.UniqueId.GetAggregateKey();
            ApplyChange(c);
        }

        public void PreliminaryAccepted(CallForSpeech cc)
        {
            var c = new CallForSpeechPreliminaryAcceptEvent
                (cc.Speaker, cc.Speech, cc.Registration,
                cc.Number, cc.Category, cc.Status, cc.Score,
                cc.PreliminaryDecision, cc.FinalDecision,
                cc.UniqueId, cc.Version);
            this.Key = c.UniqueId.GetAggregateKey();
            ApplyChange(c);
        }

        public void Evaulated(CallForSpeech cc)
        {
            var c = new CallForSpeechEvaulatedEvent
                (cc.Speaker, cc.Speech, cc.Registration,
                cc.Number, cc.Category, cc.Status, cc.Score,
                cc.PreliminaryDecision, cc.FinalDecision,
                cc.UniqueId, cc.Version);
            this.Key = c.UniqueId.GetAggregateKey();
            ApplyChange(c);
        }

        public void Accepted(CallForSpeech cc)
        {
            var c = new CallForSpeechAcceptedEvent
                (cc.Speaker, cc.Speech, cc.Registration,
                cc.Number, cc.Category, cc.Status, cc.Score,
                cc.PreliminaryDecision, cc.FinalDecision,
                cc.UniqueId, cc.Version);
            this.Key = c.UniqueId.GetAggregateKey();
            ApplyChange(c);
        }

        public CallForSpeechAggregate()
        {

        }

    }
}
