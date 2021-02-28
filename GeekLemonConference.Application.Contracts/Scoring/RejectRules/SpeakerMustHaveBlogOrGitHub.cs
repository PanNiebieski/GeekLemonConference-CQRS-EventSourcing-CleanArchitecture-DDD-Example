using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.Contracts.Scoring
{
    public class SpeakerMustHaveBlogOrGitHub : IScoringRejectRule
    {
        public bool IsSatisfiedBy(CallForSpeech cfs)
        {
            return cfs.Speaker.SpeakerWebsites.HaveGitHub()
                || cfs.Speaker.SpeakerWebsites.HaveBlog();
        }

        public string Message => "Speaker must have at blog or github";
    }
}
