using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class Name : ValueObject<Name>
    {
        public Name(string first, string last)
        {
            if (string.IsNullOrWhiteSpace(first))
                throw new ArgumentException("First name cannot be empty");
            if (string.IsNullOrWhiteSpace(last))
                throw new ArgumentException("First name cannot be empty");

            First = first;
            Last = last;
        }


        protected Name()
        {
        }

        public string First { get; }
        public string Last { get; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return First;
            yield return Last;
        }
    }
}
