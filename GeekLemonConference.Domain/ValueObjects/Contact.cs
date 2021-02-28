using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class Contact : ValueObject<Contact>
    {
        public string Phone { get; }
        public string Email { get; }

        public Contact(string phone, string email)
        {
            if (phone == null)
                throw new ArgumentException("phone cannot be null");
            if (email == null)
                throw new ArgumentException("email cannot be null");

            Phone = phone;
            Email = email;
        }

        protected Contact()
        {

        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>
            {
                Phone,
                Email
            };
        }
    }
}
