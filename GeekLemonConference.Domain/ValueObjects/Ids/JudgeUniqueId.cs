using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Domain.ValueObjects.Ids
{
    public class JudgeUniqueId : BaseUniqueId<JudgeUniqueId>
    {
        public Guid Value { get; set; }

        public JudgeUniqueId()
        {
            Value = Guid.NewGuid();
        }

        public JudgeUniqueId(Guid id)
        {
            Value = id;
        }



        public static JudgeUniqueId New()
        {
            return new JudgeUniqueId();
        }

        public static JudgeUniqueId Empty()
        {
            return new JudgeUniqueId(Guid.Empty);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }

        public static JudgeUniqueId NewUniqueId()
        {
            return new JudgeUniqueId();
        }

        protected override string GetName()
        {
            return "Judge";
        }

        public override string ValueInString() => Value.ToString();



    }
}
