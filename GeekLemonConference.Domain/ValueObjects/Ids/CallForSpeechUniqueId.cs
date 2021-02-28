using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Domain.ValueObjects.Ids
{
    public class CallForSpeechUniqueId : BaseUniqueId<CallForSpeechUniqueId>
    {
        public Guid Value { get; set; }

        public CallForSpeechUniqueId()
        {
            Value = Guid.NewGuid();
        }

        public CallForSpeechUniqueId(Guid value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }

        public static CallForSpeechUniqueId NewUniqueId()
        {
            return new CallForSpeechUniqueId();
        }

        public static CallForSpeechUniqueId Empty()
        {
            return new CallForSpeechUniqueId(Guid.Empty);
        }

        protected override string GetName()
        {
            return "CallForSpeech";
        }

        public override string ValueInString() => Value.ToString();

    }
}
