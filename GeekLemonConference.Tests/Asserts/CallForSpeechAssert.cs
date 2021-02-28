using FluentAssertions;
using FluentAssertions.Primitives;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Tests.Asserts
{
    public class CallForSpeechAssert : ReferenceTypeAssertions<CallForSpeech, CallForSpeechAssert>
    {
        public CallForSpeechAssert(CallForSpeech cfs)
            : base(cfs)
        {

        }

        public AndConstraint<CallForSpeechAssert> BeInStatus(CallForSpeechStatus expectedStatus)
        {
            Subject.Status.Should().Be(expectedStatus);
            return new AndConstraint<CallForSpeechAssert>(this);
        }

        public AndConstraint<CallForSpeechAssert> BeAccepted()
        {
            return BeInStatus(CallForSpeechStatus.AcceptedByJudge);
        }

        //public AndConstraint<CallForSpeechAssert> BeAcceptedByJudgeButHasProblems()
        //{
        //    return BeInStatus(CallForSpeechStatus.AcceptedByJudgeButHasProblems);
        //}

        public AndConstraint<CallForSpeechAssert> BePreliminaryAcceptedByJudge()
        {
            return BeInStatus(CallForSpeechStatus.PreliminaryAcceptedByJudge);
        }

        public AndConstraint<CallForSpeechAssert> BeRejected()
        {
            return BeInStatus(CallForSpeechStatus.Rejected);
        }

        public AndConstraint<CallForSpeechAssert> BeNew()
        {
            return BeInStatus(CallForSpeechStatus.New);
        }

        public AndConstraint<CallForSpeechAssert> BeEvaluatedByMachine()
        {
            return BeInStatus(CallForSpeechStatus.EvaluatedByMachine);
        }

        public AndConstraint<CallForSpeechAssert> ScoreIsNull()
        {
            Subject.Score.Should().BeNull();
            return new AndConstraint<CallForSpeechAssert>(this);
        }

        public AndConstraint<CallForSpeechAssert> ScoreIs(CallForSpeechMachineScore expectedScore)
        {
            Subject.Score?.Score.Should().Be(expectedScore);
            return new AndConstraint<CallForSpeechAssert>(this);
        }

        public AndConstraint<CallForSpeechAssert> HaveRedScore()
        {
            return ScoreIs(CallForSpeechMachineScore.Red);
        }

        public AndConstraint<CallForSpeechAssert> HaveGreenScore()
        {
            return ScoreIs(CallForSpeechMachineScore.Green);
        }

        public AndConstraint<CallForSpeechAssert> HaveYellowScore()
        {
            return ScoreIs(CallForSpeechMachineScore.Yellow);
        }

        protected override string Identifier => "CallForSpeechAssert";
    }
}
