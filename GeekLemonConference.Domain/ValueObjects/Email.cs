using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        public Email(string email, EmailType type)
        {
            Value = email;
            Type = type;
        }

        public Email()
        {
        }

        public string Value { get; init; }

        public EmailType Type { get; init; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
            yield return Type;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
