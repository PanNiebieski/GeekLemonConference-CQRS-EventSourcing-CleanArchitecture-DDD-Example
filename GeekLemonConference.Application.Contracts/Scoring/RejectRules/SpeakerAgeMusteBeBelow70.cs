using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.Contracts.Scoring
{
    public class SpeakerAgeMusteBeBelow70 : IScoringRejectRule
    {
        public bool IsSatisfiedBy(CallForSpeech cfs)
        {

            return cfs.Speaker.AgeInYearsAt(AppTime.Now()) < 70.Years();
        }

        public string Message => "Speaker age must be below 70.";
    }
}
