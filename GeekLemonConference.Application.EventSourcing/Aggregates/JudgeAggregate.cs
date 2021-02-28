using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Domain.ValueObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.EventSourcing.Aggregates
{
    public class JudgeAggregate : AggregateRoot
    {
        public Login Login { get; private set; }
        public Password Password { get; private set; }
        public Name Name { get; private set; }

        public Category Category { get; private set; }

        public List<Email> Emails { get; private set; }
        public DateTime Birthdate { get; private set; }
        public List<Phone> Phones { get; private set; }
        public JudgeUniqueId UniqueId { get; private set; }

        private void Apply(JudgeCreatedEvent e)
        {
            Version = e.Version;
            Login = e.Login;
            Password = e.Password;
            Category = e.Category;
            Birthdate = e.Birthdate;
            UniqueId = e.UniqueId;
            Key = e.Key;
        }

        private void Apply(JudgeUpdatedEvent e)
        {
            Version = e.Version++;
            Login = e.Login;
            Password = e.Password;
            Category = e.Category;
            Birthdate = e.Birthdate;
            UniqueId = e.UniqueId;
            Key = e.Key;
        }

        private void Apply(JudgeDeletedEvent e)
        {
            Version = e.Version++;
            UniqueId = e.UniqueId;
            Key = e.Key;
        }

        public JudgeAggregate()
        {

        }

        public JudgeAggregate(Judge j)
        {
            var c = new JudgeCreatedEvent(j.Birthdate,
                j.Category, j.UniqueId,
                j.Login,
                j.Password,
                j.Name, j.Version);

            ApplyChange(c);
        }

        public void Update(Judge j)
        {


            var c = new JudgeUpdatedEvent(Birthdate,
    Category, UniqueId,
    Login,
    Password,
    Name, Version);

            c.Key = c.UniqueId.GetAggregateKey();
            ApplyChange(c);
        }

        public void Delete(JudgeUniqueId id, int version)
        {
            var eve = new
                JudgeDeletedEvent(version)
            { UniqueId = id };

            eve.Key = eve.UniqueId.GetAggregateKey();

            ApplyChange(eve);
        }
    }
}
