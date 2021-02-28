using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain
{
    public interface IScoringRejectRule
    {
        bool IsSatisfiedBy(CallForSpeech speechCandidate);

        string Message { get; }
    }

    public interface IScoringWarringRule
    {
        bool IsSatisfiedBy(CallForSpeech speechCandidate);

        string Message { get; }
    }
}
