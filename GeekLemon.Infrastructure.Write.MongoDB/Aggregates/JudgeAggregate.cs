//using GeekLemonConference.Application.Contracts;
//using GeekLemonConference.Domain.DomainEvents;
//using GeekLemonConference.Domain.Entities;
//using GeekLemonConference.Domain.ValueObjects;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace GeekLemon.Infrastructure.Write.MongoDB
//{
//    public class JudgeAggregate : AggregateRoot
//    {
//        private List<Email> email;
//        private string firstname;
//        private string secondname;
//        private int age;
//        private List<Phone> phones;

//        private void Apply(JudgeCreatedEvent e)
//        {
//            Version = e.Version++;

//        }

//        private void Apply(JudgeUpdatedEvent e)
//        {
//            Version = e.Version++;

//        }

//        private void Apply(JudgeDeletedEvent e)
//        {
//            Version = e.Version++;
//        }

//        private JudgeAggregate() { }

//        public JudgeAggregate(Judge j, int version)
//        {
//            //if (string.IsNullOrEmpty(email))
//            //{
//            //    throw new ArgumentException("email");
//            //}
//            //else if (string.IsNullOrEmpty(name))
//            //{
//            //    throw new ArgumentException("name");
//            //}
//            //else if (age == 0)
//            //{
//            //    throw new ArgumentException("age");
//            //}
//            //else if (phones == null || phones.Count == 0)
//            //{
//            //    throw new ArgumentException("phones");
//            //}


//            ApplyChange(new JudgeCreatedEvent());
//        }

//        public void Update(Judge j, int version)
//        {
//            ApplyChange(new JudgeUpdatedEvent());
//        }

//        public void Delete()
//        {
//            ApplyChange(new JudgeDeletedEvent());
//        }
//    }
//}
