using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class Decision : ValueObject<Decision>
    {
        public DateTime DecisionDate { get; init; }
        public JudgeId DecisionBy { get; init; }

        public Decision(DateTime decisionDate, Judge decisionBy)
            : this(decisionDate, decisionBy.Id)
        {
        }

        //[JsonConstructor]
        public Decision(DateTime decisionDate, JudgeId decisionBy)
        {
            DecisionDate = decisionDate;
            DecisionBy = decisionBy;
        }

        // To Satisfy EF Core
        //And Serialization
        public Decision()
        {
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return DecisionDate;
            yield return DecisionBy;
        }
    }
}
