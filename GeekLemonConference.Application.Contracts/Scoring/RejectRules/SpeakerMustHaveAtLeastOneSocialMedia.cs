using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.Contracts.Scoring
{
    public class SpeakerMustHaveAtLeastOneSocialMedia : IScoringRejectRule
    {
        public bool IsSatisfiedBy(CallForSpeech cfs)
        {
            return cfs.Speaker.SpeakerWebsites.HaveSocialMedia();
        }

        public string Message => "Speaker must have at least one social media";
    }
}
