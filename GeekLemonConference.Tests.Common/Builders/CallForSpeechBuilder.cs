using GeekLemonConference.Application.Contracts.Scoring;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Domain.ValueObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Tests.Builders
{
    public class CallForSpeechBuilder
    {
        private static Category category = CategoryBuilder.GivenCategory().Build();


        private Judge judge = new Judge(new Login("admin"),
            new Password("admin"),
            new Name("admin", "admin"),
            category);


        private Speaker speaker = new SpeakerBuilder().Build();

        private Speech speech = new SpeechBuilder().Build();

        private CallForSpeechNumber callForSpeechNumber = new CallForSpeechNumber(Guid.NewGuid().ToString());
        private bool evaluated = false;

        private CallForSpeechStatus targetStatus = CallForSpeechStatus.New;
        private ScoringRulesFactory scoringRulesFactory = new ScoringRulesFactory();

        public static CallForSpeechBuilder GivenCallForSpeech() => new CallForSpeechBuilder();

        public CallForSpeechBuilder Accepted()
        {
            targetStatus = CallForSpeechStatus.AcceptedByJudge;
            return this;
        }

        public CallForSpeechBuilder Rejected()
        {
            targetStatus = CallForSpeechStatus.Rejected;
            return this;
        }

        public CallForSpeechBuilder Evaluated()
        {
            evaluated = true;
            targetStatus = CallForSpeechStatus.EvaluatedByMachine;
            return this;
        }

        public CallForSpeechBuilder NotEvaluated()
        {
            evaluated = false;
            targetStatus = CallForSpeechStatus.New;
            return this;
        }

        public CallForSpeechBuilder New()
        {
            targetStatus = CallForSpeechStatus.New;
            evaluated = false;
            return this;
        }

        public CallForSpeechBuilder PreliminaryAcceptedByJudge()
        {
            targetStatus = CallForSpeechStatus.PreliminaryAcceptedByJudge;
            evaluated = true;
            return this;
        }

        public CallForSpeechBuilder WithNumber(string number)
        {
            callForSpeechNumber = new CallForSpeechNumber(number);
            return this;
        }

        public CallForSpeechBuilder WithSpeaker(Action<SpeakerBuilder> speakerBuilderAction)
        {
            var speakerBuilder = new SpeakerBuilder();
            speakerBuilderAction(speakerBuilder);
            speaker = speakerBuilder.Build();
            return this;
        }

        public CallForSpeechBuilder WithSpeech(Action<SpeechBuilder> speechBuilderAction)
        {
            var speechBuilder = new SpeechBuilder();
            speechBuilderAction(speechBuilder);
            speech = speechBuilder.Build();
            return this;
        }

        public CallForSpeechBuilder WithJudge(string login, Guid CategoryId)
        {
            CategoryId categoryId =
            new CategoryId(0);

            judge = new Judge(new Login(login),
            new Password(login),
            new Name(login, login),
            new Category(
            categoryId,
            login, login, login));
            return this;
        }

        public CallForSpeechBuilder WithCategory(
            Action<CategoryBuilder> categoryBuilderAction)
        {
            var categoryBuilder = new CategoryBuilder();
            categoryBuilderAction(categoryBuilder);
            category = categoryBuilder.Build();
            return this;
        }

        public CallForSpeech Build()
        {
            var cfs = new CallForSpeech
            (
                callForSpeechNumber,
                speech,
                speaker,
                category
            );

            if (targetStatus == CallForSpeechStatus.EvaluatedByMachine)
            {
                cfs.Evaluate(scoringRulesFactory.DefaultSet);
            }

            if (targetStatus == CallForSpeechStatus.PreliminaryAcceptedByJudge)
            {
                cfs.Evaluate(scoringRulesFactory.DefaultSet);
                cfs.PreliminaryAccept(judge);
            }

            if (targetStatus == CallForSpeechStatus.AcceptedByJudge)
            {
                cfs.Evaluate(scoringRulesFactory.DefaultSet);
                cfs.PreliminaryAccept(judge);
                cfs.Accept(judge);
            }

            if (targetStatus == CallForSpeechStatus.Rejected)
            {
                cfs.Reject(judge);
            }

            return cfs;
        }
    }
}
