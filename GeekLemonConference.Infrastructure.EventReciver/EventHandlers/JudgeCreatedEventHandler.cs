using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.DomainEvents.Ddd;
//using Microsoft.Extensions.Logging;
//using NLog;
using System;
using System.Linq;

namespace CustomerApi.WriteModels.Events.Handlers
{
    public class JudgeCreatedEventHandler : IBusEventHandler
    {
        //private readonly CustomerReadModelRepository readModelRepository;

        //private Logger logger = LogManager.GetLogger("CustomerCreatedEventHandler");

        public JudgeCreatedEventHandler()
        {
            ////this.readModelRepository = readModelRepository;
        }

        public Type HandlerType
        {
            get { return typeof(JudgeCreatedEvent); }
        }

        public async void Handle(DomainEvent @event)
        {
            JudgeCreatedEvent customerCreatedEvent = (JudgeCreatedEvent)@event;

            //await readModelRepository.Create(new Judge()
            //{
            //    Id = customerCreatedEvent.Id,
            //    Email = customerCreatedEvent.Email,
            //    Name = customerCreatedEvent.Name,
            //    Age = customerCreatedEvent.Age,
            //    Phones = customerCreatedEvent.Phones.Select(x =>
            //        new Phone()
            //        {
            //            Type = x.Type,
            //            AreaCode = x.AreaCode,
            //            Number = x.Number
            //        }).ToList()
            //});

            //logger.Info("A new CustomerCreatedEvent has been processed: {0} ({1})", customerCreatedEvent.Id, customerCreatedEvent.Version);
        }
    }
}
