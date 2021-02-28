//using GeekLemonConference.Domain.Ddd;
//using GeekLemonConference.Domain.Entities;
//using GeekLemonConference.Domain.ValueObjects;
//using GeekLemonConference.Domain.ValueObjects.Ids;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace GeekLemonConference.Domain.Entity
//{
//    public class CallForSpeech : Entity<CallForSpeechId>
//    {
//        public Speaker Speaker { get; }
//        public Speech Speech { get; }
//        public Registration Registration { get; }
//        public CallForSpeechNumber Number { get; }

//        public Category Category { get; private set; }
//        public CallForSpeechStatus Status { get; private set; }

//        public CallForSpeechScoringResult Score { get; private set; }

//        public Decision PreliminaryDecision { get; private set; }
//        public Decision FinalDecision { get; private set; }


//        public CallForSpeech(CallForSpeechNumber number, Speech speech,
//               Speaker speaker, Category cat)
//            : this(number, CallForSpeechStatus.New, speaker, speech, cat,
//        null, new Registration(AppTime.Now()), null, null,
//        new CallForSpeechId(0))
//        {

//        }

//        public CallForSpeech(
//            CallForSpeechNumber number,
//            CallForSpeechStatus status,
//            Speaker speaker,
//            Speech speech,
//            Category category,
//            CallForSpeechScoringResult score,
//            Registration registration,
//            Decision preliminaryDecision,
//            Decision finalDecision,
//            CallForSpeechId callForSpeechId)
//        {
//            if (category == null)
//                throw new ArgumentException("Category cannot be null");
//            if (number == null)
//                throw new ArgumentException("Number cannot be null");
//            if (speech == null)
//                throw new ArgumentException("speech cannot be null");
//            if (speaker == null)
//                throw new ArgumentException("speaker cannot be null");
//            if (registration == null)
//                throw new ArgumentException("Registration cannot be null");

//            Id = callForSpeechId;
//            Number = number;
//            Status = status;
//            Score = score;
//            Speech = speech;
//            Speaker = speaker;
//            Registration = registration;
//            PreliminaryDecision = preliminaryDecision;
//            FinalDecision = finalDecision;
//            Category = category;
//        }

//        // To satisfy EF Core
//        protected CallForSpeech()
//        {
//        }

//        public void Evaluate(ScoringRules rules)
//        {
//            if (Status != CallForSpeechStatus.New)
//            {
//                throw new ApplicationException("Cannot accept application that isn't new");
//            }

//            Score = rules.Evaluate(this);
//            if (!Score.IsRed())
//            {
//                Status = CallForSpeechStatus.EvaluatedByMachine;
//            }
//            else
//            {
//                Status = CallForSpeechStatus.Rejected;
//            }
//        }

//        public void PreliminaryAccept(Judge decisionBy)
//        {
//            if (Status != CallForSpeechStatus.EvaluatedByMachine)
//            {
//                throw new ApplicationException("Cannot accept application that WASNT'T in EvaluatedByMachine");
//            }

//            if (Score == null)
//            {
//                throw new ApplicationException("Cannot accept application before scoring");
//            }

//            Status = CallForSpeechStatus.PreliminaryAcceptedByJudge;
//            PreliminaryDecision = new Decision(AppTime.Now(), decisionBy);
//        }

//        public void Accept(Judge decisionBy)
//        {
//            if (Status == CallForSpeechStatus.Rejected)
//            {
//                throw new ApplicationException("Cannot accept application that is already rejected");
//            }

//            if (Status != CallForSpeechStatus.PreliminaryAcceptedByJudge)
//            {
//                throw new ApplicationException("Cannot accept application that wasn't PreliminaryAccepted FIRST");
//            }

//            if (Score == null)
//            {
//                throw new ApplicationException("Cannot accept application before scoring");
//            }

//            if (!decisionBy.CanAccept(this.Category.Id))
//            {
//                throw new ApplicationException("Judge is from diffrent category. Can't Accept");
//            }

//            Status = CallForSpeechStatus.AcceptedByJudge;
//            FinalDecision = new Decision(AppTime.Now(), decisionBy);
//        }

//        public void AcceptWithProblems(Judge decisionBy)
//        {
//            if (Status != CallForSpeechStatus.PreliminaryAcceptedByJudge)
//            {
//                throw new ApplicationException("Cannot accept application that wasn't PreliminaryAccepted FIRST");
//            }

//            if (Score == null)
//            {
//                throw new ApplicationException("Cannot accept application before scoring");
//            }

//            Status = CallForSpeechStatus.AcceptedByJudgeButHasProblems;
//            FinalDecision = new Decision(AppTime.Now(), decisionBy);
//        }

//        public void Reject(Judge decisionBy)
//        {
//            if (Status == CallForSpeechStatus.Rejected ||
//                Status == CallForSpeechStatus.AcceptedByJudge)
//            {
//                throw new ApplicationException("Cannot reject application that is already accepted or rejected");
//            }

//            Status = CallForSpeechStatus.Rejected;
//            FinalDecision = new Decision(AppTime.Now(), decisionBy);
//        }
//    }
//}
