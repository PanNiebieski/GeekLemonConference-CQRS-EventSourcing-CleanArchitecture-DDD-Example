using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public enum CallForSpeechStatus
    {
        New = 0,
        EvaluatedByMachine = 1,
        PreliminaryAcceptedByJudge = 2,
        AcceptedByJudge = 3,
        //AcceptedByJudgeButHasProblems = 4,
        Rejected = 5
    }
}
