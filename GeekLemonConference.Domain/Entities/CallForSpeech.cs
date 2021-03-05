using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.Entity
{
    public class CallForSpeech : Entity<CallForSpeechId, CallForSpeechUniqueId>
    {
        public Speaker Speaker { get; set; }
        public Speech Speech { get; set; }
        public Registration Registration { get; set; }
        public CallForSpeechNumber Number { get; set; }

        public Category Category { get; private set; }
        public CallForSpeechStatus Status { get; private set; }

        [JsonProperty("scoreResult")]
        public CallForSpeechScoringResult ScoreResult { get; set; }

        public Decision PreliminaryDecision { get; private set; }
        public Decision FinalDecision { get; private set; }


        public CallForSpeech(CallForSpeechNumber number, Speech speech,
               Speaker speaker, Category cat)
            : this(number, CallForSpeechStatus.New, speaker, speech, cat,
        null, new Registration(AppTime.Now()), null, null,
        new CallForSpeechId(0))
        {

        }


        public CallForSpeech(
            CallForSpeechNumber number,
            CallForSpeechStatus status,
            Speaker speaker,
            Speech speech,
            Category category,
            CallForSpeechScoringResult scoreResult,
            Registration registration,
            Decision preliminaryDecision,
            Decision finalDecision,
            CallForSpeechId callForSpeechId)
        {
            if (category == null)
                throw new ArgumentException("Category cannot be null");
            if (number == null)
                throw new ArgumentException("Number cannot be null");
            if (speech == null)
                throw new ArgumentException("speech cannot be null");
            if (speaker == null)
                throw new ArgumentException("speaker cannot be null");
            if (registration == null)
                throw new ArgumentException("Registration cannot be null");

            Id = callForSpeechId;
            Number = number;
            Status = status;
            ScoreResult = scoreResult;
            Speech = speech;
            Speaker = speaker;
            Registration = registration;
            PreliminaryDecision = preliminaryDecision;
            FinalDecision = finalDecision;
            Category = category;
            UniqueId = CallForSpeechUniqueId.NewUniqueId();
        }

        // To satisfy EF Core
        protected CallForSpeech()
        {
            UniqueId = CallForSpeechUniqueId.NewUniqueId();
        }

        public void Evaluate(ScoringRules rules)
        {
            if (Status != CallForSpeechStatus.New)
            {
                throw new ApplicationException("Cannot accept application that isn't new");
            }

            ScoreResult = rules.Evaluate(this);
            if (!ScoreResult.IsRed())
            {
                Status = CallForSpeechStatus.EvaluatedByMachine;
            }
            else
            {
                Status = CallForSpeechStatus.Rejected;
            }
        }

        public ExecutionStatus TryEvaluate(ScoringRules rules)
        {
            if (Status != CallForSpeechStatus.New)
            {
                return ExecutionStatus.LogicError("Cannot accept application that isn't new");
            }

            ScoreResult = rules.Evaluate(this);
            if (!ScoreResult.IsRed())
            {
                Status = CallForSpeechStatus.EvaluatedByMachine;
            }
            else
            {
                Status = CallForSpeechStatus.Rejected;
            }
            return ExecutionStatus.LogicOk();
        }

        public void PreliminaryAccept(Judge decisionBy)
        {
            if (Status == CallForSpeechStatus.PreliminaryAcceptedByJudge)
            {
                throw new ApplicationException("You already PreliminaryAcceptedByJudge this CallForSpeech");
            }

            if (Status != CallForSpeechStatus.EvaluatedByMachine)
            {
                throw new ApplicationException("Cannot accept application that WASNT'T in EvaluatedByMachine");
            }

            if (ScoreResult == null)
            {
                throw new ApplicationException("Cannot accept application before scoring");
            }

            if (!decisionBy.CanAccept(this.Category.Id))
            {
                throw new ApplicationException("Judge is from diffrent category. Can't Accept");
            }

            Status = CallForSpeechStatus.PreliminaryAcceptedByJudge;
            PreliminaryDecision = new Decision(AppTime.Now(), decisionBy);
        }

        public ExecutionStatus TryPreliminaryAccept(Judge decisionBy)
        {
            if (Status == CallForSpeechStatus.PreliminaryAcceptedByJudge)
            {
                return ExecutionStatus.
                    LogicError("You already PreliminaryAcceptedByJudge this CallForSpeech");
            }

            if (Status != CallForSpeechStatus.EvaluatedByMachine)
            {
                return ExecutionStatus.LogicError("Cannot accept application that WASNT'T in EvaluatedByMachine");
            }

            if (ScoreResult == null)
            {
                return ExecutionStatus.LogicError("Cannot accept application before scoring");
            }

            if (!decisionBy.CanAccept(this.Category.Id))
            {
                return ExecutionStatus.
                    LogicError("Judge is from diffrent category. Can't Accept");
            }

            Status = CallForSpeechStatus.PreliminaryAcceptedByJudge;
            PreliminaryDecision = new Decision(AppTime.Now(), decisionBy);
            return ExecutionStatus.LogicOk();
        }



        public void Accept(Judge decisionBy)
        {
            if (Status == CallForSpeechStatus.AcceptedByJudge)
            {
                throw new ApplicationException("You already Accepted this CallForSpeech");
            }

            if (Status == CallForSpeechStatus.Rejected)
            {
                throw new ApplicationException("Cannot accept application that is already rejected");
            }

            if (Status != CallForSpeechStatus.PreliminaryAcceptedByJudge)
            {
                throw new ApplicationException("Cannot accept application that wasn't PreliminaryAccepted FIRST");
            }

            if (ScoreResult == null)
            {
                throw new ApplicationException("Cannot accept application before scoring");
            }

            if (!decisionBy.CanAccept(this.Category.Id))
            {
                throw new ApplicationException("Judge is from diffrent category. Can't Accept");
            }

            Status = CallForSpeechStatus.AcceptedByJudge;
            FinalDecision = new Decision(AppTime.Now(), decisionBy);
        }

        public ExecutionStatus TryAccept(Judge decisionBy)
        {
            if (Status == CallForSpeechStatus.AcceptedByJudge)
            {
                return ExecutionStatus.
                    LogicError("You already Accepted this CallForSpeech");
            }

            if (Status == CallForSpeechStatus.Rejected)
            {
                return ExecutionStatus.
                    LogicError("Cannot accept application that is already rejected");
            }

            if (Status != CallForSpeechStatus.PreliminaryAcceptedByJudge)
            {
                return ExecutionStatus.
                    LogicError("Cannot accept application that wasn't PreliminaryAccepted FIRST");
            }

            if (ScoreResult == null)
            {
                return ExecutionStatus.
                    LogicError("Cannot accept application before scoring");
            }

            if (!decisionBy.CanAccept(this.Category.Id))
            {
                return ExecutionStatus.
                    LogicError("Judge is from diffrent category. Can't Accept");
            }

            Status = CallForSpeechStatus.AcceptedByJudge;
            FinalDecision = new Decision(AppTime.Now(), decisionBy);
            return ExecutionStatus.LogicOk();
        }


        public void Reject(Judge decisionBy)
        {
            if (Status == CallForSpeechStatus.Rejected ||
                Status == CallForSpeechStatus.AcceptedByJudge)
            {
                throw new ApplicationException("Cannot reject application that is already accepted or rejected");
            }

            if (!decisionBy.CanAccept(this.Category.Id))
            {
                throw new ApplicationException("Judge is from diffrent category. Can't Accept");
            }

            Status = CallForSpeechStatus.Rejected;
            FinalDecision = new Decision(AppTime.Now(), decisionBy);
        }

        public ExecutionStatus TryReject(Judge decisionBy)
        {
            if (Status == CallForSpeechStatus.Rejected ||
                Status == CallForSpeechStatus.AcceptedByJudge)
            {
                return ExecutionStatus.
                    LogicError("Cannot reject application that is already accepted or rejected");
            }

            if (!decisionBy.CanAccept(this.Category.Id))
            {
                return ExecutionStatus.
                    LogicError("Judge is from diffrent category. Can't Accept");
            }

            Status = CallForSpeechStatus.Rejected;
            FinalDecision = new Decision(AppTime.Now(), decisionBy);

            return ExecutionStatus.LogicOk();
        }

        public CallForSpeechIds Ids()
        {
            if (this.Id != null && this.Id.Value != default)
                return new CallForSpeechIds()
                {
                    UniqueId = this.UniqueId,
                    CreatedId = this.Id
                };
            else
                return new CallForSpeechIds()
                {
                    UniqueId = this.UniqueId,
                    CreatedId = this.Id,
                    Status = IdsStatus.DudeYouCantReturnCreatedIdWhenYouAreEventSourcing

                };
        }
    }
}
