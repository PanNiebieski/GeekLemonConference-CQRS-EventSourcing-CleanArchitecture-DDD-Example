using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects.Ids
{
    public class JudgeId : BaseId<JudgeId>
    {
        public int Value { get; set; }

        public JudgeId(int value)
        {
            Value = value;
        }

        public JudgeId()
        {

        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }



    }
}
