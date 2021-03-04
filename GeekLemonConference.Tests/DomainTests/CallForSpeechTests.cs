using FluentAssertions;
using GeekLemonConference.Application.Contracts.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GeekLemonConference.Tests.Asserts;
using static GeekLemonConference.Tests.Builders.CallForSpeechBuilder;
using static GeekLemonConference.Tests.Builders.CategoryBuilder;
using static GeekLemonConference.Tests.Builders.JudgeBuilder;
using GeekLemonConference.Domain.ValueObjects;

namespace GeekLemonConference.Tests.DomainTests
{
    public class CallForSpeechTests
    {
        private readonly ScoringRulesFactory scoringRulesFactory = new ScoringRulesFactory();


        [Fact]
        public void NewCallForSpeech_IsCreatedIn_NewStatus_AndNullScore()
        {
            var cfs = GivenCallForSpeech()
                .WithSpeaker(speaker => speaker.WithAge(25))
                .Build();

            cfs
                .Should()
                .BeNew()
                .And
                .ScoreIsNull();
        }

        [Fact]
        public void ValidCallForSpeech_EvaluationScore_IsGreen()
        {
            var cfs = GivenCallForSpeech()
                .WithSpeaker(speaker => speaker.WithAge(31))
                .WithSpeech(speech =>
                    speech.WithTechnologyOrBussinessStory
                    (TechnologyOrBussinessStory.OnlyMyPersonalBussinessStory))
                .Build();

            cfs.Evaluate(scoringRulesFactory.DefaultSet);

            cfs
                .Should()
                .BeEvaluatedByMachine()
                .And
                .HaveGreenScore();
        }

        [Fact]
        public void InvalidCallForSpeech_EvaluationScore_IsRed()
        {
            var cfs = GivenCallForSpeech()
                .WithSpeaker(speaker => speaker.WithAge(17)
                .WithSpeakerWebsites(web => web.ClearWebsites()))
                .WithSpeech(speech =>
                    speech.WithTechnologyOrBussinessStory
                    (TechnologyOrBussinessStory.OnlyMyPersonalBussinessStory))
                .Build();

            cfs.Evaluate(scoringRulesFactory.DefaultSet);

            cfs
                .Should()
                .BeRejected()
                .And
                .HaveRedScore();
        }

        [Fact]
        public void CallForSpeech_InStatusNew_EvaluatedGreen_PreliminaryAccept_Then_Jugde_CanAccept()
        {
            int catid = 8888;

            var cfs = GivenCallForSpeech()
                .WithSpeaker(speaker => speaker.WithAge(31))
                .WithSpeech(speech =>
                    speech.WithTechnologyOrBussinessStory
                    (TechnologyOrBussinessStory.OnlyMyPersonalBussinessStory))
                .Evaluated()
                .WithCategory(cat => cat.WithId(catid))
                .Build();

            var judge = GivenJudge().
                WithCategory(cat => cat.WithId(catid))
                .Build();

            cfs.PreliminaryAccept(judge);
            cfs.Accept(judge);

            cfs
                .Should()
                .BeAccepted()
                .And
                .HaveGreenScore();
        }

        [Fact]
        public void CallForSpeech_InStatusNew_EvaluatedGreen_PreliminaryAccepted_Then_JugdeFormDiffrentCategory_CannotAccept()
        {
            int catid2 = int.MaxValue;

            var cfs = GivenCallForSpeech()
                .WithSpeaker(speaker => speaker.WithAge(31))
                .WithSpeech(speech =>
                    speech.WithTechnologyOrBussinessStory
                    (TechnologyOrBussinessStory.OnlyMyPersonalBussinessStory))
                .Evaluated()
                .PreliminaryAcceptedByJudge()
                .Build();

            var judge = GivenJudge().
                WithCategory(cat => cat.WithId(catid2))
                .Build();

            //cfs.PreliminaryAccept(judge);

            Action act = () => cfs.Accept(judge);

            act
              .Should()
              .Throw<ApplicationException>()
              .WithMessage("Judge is from diffrent category. Can't Accept");
        }

        [Fact]
        public void CallForSpeech_InStatusNew_EvaluatedGreen_CanBeRejected()
        {
            int catid = 8888;

            var cfs = GivenCallForSpeech()
                .WithSpeaker(speaker => speaker.WithAge(31))
                .WithSpeech(speech =>
                    speech.WithTechnologyOrBussinessStory
                    (TechnologyOrBussinessStory.OnlyMyPersonalBussinessStory))
                .Evaluated()
                .WithCategory(cat => cat.WithId(catid))
                .Build();

            var judge = GivenJudge().
            WithCategory(cat => cat.WithId(catid))
            .Build();

            cfs.Reject(judge);

            cfs
            .Should()
            .BeRejected()
            .And.HaveGreenScore();
        }

        [Fact]
        public void CallForSpeech_InStatusNew_EvaluatedGreen_PreliminaryAccepted_CanBeRejected()
        {
            int catid = 8888;

            var cfs = GivenCallForSpeech()
                .WithSpeaker(speaker => speaker.WithAge(31))
                .WithSpeech(speech =>
                    speech.WithTechnologyOrBussinessStory
                    (TechnologyOrBussinessStory.OnlyMyPersonalBussinessStory))
                .Evaluated()
                .WithCategory(cat => cat.WithId(catid))
                .Build();

            var judge = GivenJudge().
            WithCategory(cat => cat.WithId(catid))
            .Build();

            cfs.PreliminaryAccept(judge);
            cfs.Reject(judge);

            cfs
            .Should()
            .BeRejected()
            .And.HaveGreenScore();
        }

        [Fact]
        public void CallForSpeech_WithoutScore_NOTEvaluatedByMachine_CannotBePreliminaryAccept()
        {
            var cfs = GivenCallForSpeech()
                .NotEvaluated()
                .Build();

            var judge = GivenJudge()
                .Build();

            Action act = () => cfs.PreliminaryAccept(judge);

            act
                .Should()
                .Throw<ApplicationException>()
                .WithMessage(
                "Cannot accept application that WASNT'T in EvaluatedByMachine");
        }

        [Fact]
        public void CallForSpeech_WithoutScore_NOTEvaluatedByMachine_CanBeRejected()
        {
            int catid = 8888;
            var cfs = GivenCallForSpeech()
                .WithCategory(cat => cat.WithId(catid))
                .NotEvaluated()
                .Build();

            var judge = GivenJudge()
                .WithCategory(cat => cat.WithId(catid))
                .Build();

            cfs.Reject(judge);

            cfs
                .Should()
                .BeRejected()
                .And.ScoreIsNull();
        }

        [Fact]
        public void CallForSpeec_Accepted_CannotBeRejected()
        {
            int catid = 8888;
            var cfs = GivenCallForSpeech()
                .WithCategory(cat => cat.WithId(catid))
                .Evaluated()
                .Build();

            var judge = GivenJudge()
                .WithCategory(cat => cat.WithId(catid))
                .Build();

            cfs.PreliminaryAccept(judge);
            cfs.Accept(judge);

            Action act = () => cfs.Reject(judge);

            act
             .Should()
             .Throw<ApplicationException>()
             .WithMessage("Cannot reject application that is already accepted or rejected");
        }

        [Fact]
        public void CallForSpeec_Rejected_CannotBeRejected()
        {
            int catid = 8888;

            var cfs = GivenCallForSpeech()
                .WithCategory(cat => cat.WithId(catid))
                .Rejected()
                .Build();

            var judge = GivenJudge()
                .WithCategory(cat => cat.WithId(catid))
                .Build();

            Action act = () => cfs.Reject(judge);

            act
             .Should()
             .Throw<ApplicationException>()
             .WithMessage("Cannot reject application that is already accepted or rejected");
        }

        [Fact]
        public void CallForSpeec_PreliminaryAcceptedByJudge_CanbeAccepted()
        {
            int catid = 8888;

            var cfs = GivenCallForSpeech()
                .WithCategory(cat => cat.WithId(catid))
                .PreliminaryAcceptedByJudge()
                .Build();

            var judge = GivenJudge()
                .WithCategory(cat => cat.WithId(catid))
                .Build();

            cfs.Accept(judge);

            cfs
            .Should()
            .BeAccepted();

        }


    }
}
