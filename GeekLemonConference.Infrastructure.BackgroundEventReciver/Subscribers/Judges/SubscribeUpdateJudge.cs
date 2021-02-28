using AutoMapper;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.DomainEvents.Categories;
using GeekLemonConference.DomainEvents.Ddd;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers.Judges
{
    public class SubscribeUpdateJudge : SubscribeBase
    {
        private IZEsJudgeRepository _zEsJudgeRepository;
        private IMapper _mapper;



        public SubscribeUpdateJudge(IZEsJudgeRepository zEsJudgeRepository,
            IMapper mapper) :
            base()
        {
            _zEsJudgeRepository = zEsJudgeRepository;
            _mapper = mapper;
        }

        public override string QUEUE_Name => Constants.QUEUE_JUDGE_UPDATED;

        public override DomainEvent DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<JudgeUpdatedEvent>(json);
        }

        public async override Task<ExecutionStatus> HandleEvent(DomainEvent @event)
        {
            JudgeUpdatedEvent categoryCreateEvent = @event as JudgeUpdatedEvent;

            var judge = _mapper.Map<Judge>(categoryCreateEvent);

            var execution = await
                _zEsJudgeRepository.UpdateByUniqueIdAsync(judge);

            return execution;
        }
    }
}
