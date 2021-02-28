using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class Registration : ValueObject<Registration>
    {
        public DateTime RegistrationDate { get; }

        //[JsonConstructor]
        public Registration(DateTime registrationDate)
        {
            RegistrationDate = registrationDate;
        }

        protected Registration()
        {
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return RegistrationDate;
        }
    }
}
