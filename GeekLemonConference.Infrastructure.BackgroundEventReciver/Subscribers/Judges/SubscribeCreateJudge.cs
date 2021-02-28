using AutoMapper;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.DomainEvents.Ddd;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers.Judges
{
    public class SubscribeCreateJudge : SubscribeBase
    {
        private IZEsJudgeRepository _zEsJudgeRepository;
        private IMapper _mapper;

        public SubscribeCreateJudge(IZEsJudgeRepository zEsJudgeRepository,
            IMapper mapper) :
            base()
        {
            _zEsJudgeRepository = zEsJudgeRepository;
            _mapper = mapper;
        }

        public override string QUEUE_Name => Constants.QUEUE_JUDGE_CREATED;

        public override DomainEvent DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<JudgeCreatedEvent>(json);
        }

        public override async Task<ExecutionStatus> HandleEvent(DomainEvent @event)
        {
            JudgeCreatedEvent categoryCreateEvent = @event as JudgeCreatedEvent;

            var judge = _mapper.Map<Judge>(categoryCreateEvent);

            var execution = await _zEsJudgeRepository.AddAsync(judge);

            return execution.RemoveGeneric();
        }
    }
}
