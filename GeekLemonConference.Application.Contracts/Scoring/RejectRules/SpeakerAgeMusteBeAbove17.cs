using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.Contracts.Scoring
{
    public class SpeakerAgeMusteBeAbove17 : IScoringRejectRule
    {
        public bool IsSatisfiedBy(CallForSpeech cfs)
        {
            return cfs.Speaker.AgeInYearsAt(AppTime.Now()) > 17.Years();
        }

        public string Message => "Speaker age must be above 17.";
    }
}
