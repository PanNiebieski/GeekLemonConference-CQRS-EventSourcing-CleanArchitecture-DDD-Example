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
    public class SubscribeDeleteJudge : SubscribeBase
    {
        private IZEsJudgeRepository _zEsJudgeRepository;
        private IMapper _mapper;

        public SubscribeDeleteJudge(IZEsJudgeRepository zEsJudgeRepository,
            IMapper mapper) :
            base()
        {
            _zEsJudgeRepository = zEsJudgeRepository;
            _mapper = mapper;
        }

        public override string QUEUE_Name => Constants.QUEUE_JUDGE_DELETED;

        public override DomainEvent DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<JudgeDeletedEvent>(json);
        }

        public override async Task<ExecutionStatus> HandleEvent(DomainEvent @event)
        {
            JudgeDeletedEvent judgeDeletedEvent = @event as JudgeDeletedEvent;

            var execution = await
                _zEsJudgeRepository.DeleteAsync(judgeDeletedEvent.UniqueId);

            return execution;
        }
    }
}
