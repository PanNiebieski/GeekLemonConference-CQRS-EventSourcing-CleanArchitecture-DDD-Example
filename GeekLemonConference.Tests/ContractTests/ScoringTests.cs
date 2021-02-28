using GeekLemonConference.Application.Contracts.Scoring;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Tests.Builders;
using GeekLemonConference.Tests.ContractTests.ClassDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static GeekLemonConference.Tests.Builders.SpeakerBuilder;
using static GeekLemonConference.Tests.Builders.CallForSpeechBuilder;
using FluentAssertions;

namespace GeekLemonConference.Tests.DomainTests
{
    public class ScoringTests
    {
        private readonly ScoringRulesFactory scoringRulesFactory
            = new ScoringRulesFactory();

        [Fact]
        public void Speaker_Have_Blog_SpeakerMustHaveBlogOrGitHub_Rule_IsSatisfied()
        {
            var cfs = GivenCallForSpeech()
            .WithSpeaker(speaker => speaker.
             WithSpeakerWebsites
             (web => web.ClearWebsites()
             .WithBlog("http://cezary.pl")))
            .Build();

            var rule = new SpeakerMustHaveBlogOrGitHub();
            var ruleCheckResult = rule.IsSatisfiedBy(cfs);

            ruleCheckResult.Should().BeTrue();
        }

        [Fact]
        public void Speaker_Have_GitHub_SpeakerMustHaveBlogOrGitHub_Rule_IsSatisfied()
        {
            var cfs = GivenCallForSpeech()
                .WithSpeaker(speaker => speaker.
                 WithSpeakerWebsites
                 (web => web.ClearWebsites()
                 .WithGitHub("https://github.com/PanNiebieski")))
                .Build();

            var rule = new SpeakerMustHaveBlogOrGitHub();
            var ruleCheckResult = rule.IsSatisfiedBy(cfs);

            ruleCheckResult.Should().BeTrue();
        }

        [Fact]
        public void Speaker_Age_Is_17_SpeakerAgeMusteBeAbove17_Rule_IsNotSatisfied()
        {
            var cfs = GivenCallForSpeech()
            .WithSpeaker(speaker => speaker.WithAge(17))
            .Build();

            var rule = new SpeakerAgeMusteBeAbove17();
            var ruleCheckResult = rule.IsSatisfiedBy(cfs);

            ruleCheckResult.Should().BeFalse();
        }

        [Fact]
        public void Speaker_Age_Is_70_SpeakerAgeMusteBeBelow70_Rule_IsNotSatisfied()
        {
            var cfs = GivenCallForSpeech()
                .WithSpeaker(speaker => speaker.WithAge(70))
                .Build();

            var rule = new SpeakerAgeMusteBeBelow70();
            var ruleCheckResult = rule.IsSatisfiedBy(cfs);

            ruleCheckResult.Should().BeFalse();
        }

        [Fact]
        public void Speaker_Doesnt_Have_Any_SocialMedia_SpeakerMustHaveAtLeastOneSocialMedia_IsNotSatisfied()
        {
            var cfs = GivenCallForSpeech()
            .WithSpeaker(speaker =>
                speaker.
                WithSpeakerWebsites(web => web.ClearWebsites()))
            .Build();

            var rule = new SpeakerMustHaveAtLeastOneSocialMedia();
            var ruleCheckResult = rule.IsSatisfiedBy(cfs);

            ruleCheckResult.Should().BeFalse();
        }


        [Theory]
        [ClassData(typeof(SpeakerWebsitesTestData))]
        public void Speaker_Does_Have_JustOne_SocialMedia_SpeakerMustHaveAtLeastOneSocialMedia_IsSatisfied(SpeakerWebsites web)
        {
            var cfs = GivenCallForSpeech()
                .WithSpeaker(speaker => speaker.WithSpeakerWebsites(web))
                .Build();

            var rule = new SpeakerMustHaveAtLeastOneSocialMedia();
            var ruleCheckResult = rule.IsSatisfiedBy(cfs);

            ruleCheckResult.Should().BeTrue();
        }

    }
}
