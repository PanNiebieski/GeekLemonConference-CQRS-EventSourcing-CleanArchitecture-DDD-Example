using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class Speaker : ValueObject<Speaker>
    {
        public DateTime Birthdate { get; init; }
        public Name Name { get; init; }
        public Address Address { get; init; }
        public SpeakerWebsites SpeakerWebsites { get; init; }

        public string Biography { get; init; }
        public Contact Contact { get; init; }

        public Speaker(Name name, DateTime birthdate,
            Address address, SpeakerWebsites speakerWebsites, string biography,
            Contact contact)
        {
            if (contact == null)
                throw new ArgumentException("Contact cannot be null");
            if (biography == null)
                throw new ArgumentException("biography cannot be null");
            if (name == null)
                throw new ArgumentException("Name cannot be null");
            if (address == null)
                throw new ArgumentException("Address cannot be null");
            if (birthdate == default)
                throw new ArgumentException("Birthdate cannot be empty");
            if (speakerWebsites == default)
                throw new ArgumentException("SpeakerWebsites cannot be empty");

            Name = name;
            Birthdate = birthdate;
            Address = address;
            SpeakerWebsites = speakerWebsites;
            Biography = biography;
            Contact = contact;
        }

        public AgeInYears AgeInYearsAt(DateTime date)
        {
            return AgeInYears.Between(Birthdate, date);
        }



        protected Speaker()
        {

        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>
            {
                Name,
                Birthdate,
                Address
            };
        }
    }
}
