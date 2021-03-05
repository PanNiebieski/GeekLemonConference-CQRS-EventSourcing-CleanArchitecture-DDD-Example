using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.Domain;
using GeekLemonConference.DomainEvents.Ddd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Domain.Entity;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers.CallForSpeeches
{
    public class SubscribeEvaluateCallForSpeech : SubscribeBase
    {
        private IZEsCallForSpeechRepository _ZEsCallForSpeechRepository;
        private IMapper _mapper;

        public SubscribeEvaluateCallForSpeech(
            IZEsCallForSpeechRepository zEsCallForSpeechRepository,
            IMapper mapper) :
            base()
        {
            _ZEsCallForSpeechRepository = zEsCallForSpeechRepository;
            _mapper = mapper;
        }

        public override string QUEUE_Name => Constants.QUEUE_CALLFORSPEECH_EVALUATE;

        public override DomainEvent DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<CallForSpeechEvaulatedEvent>(json);
        }

        public async override Task<ExecutionStatus> HandleEvent(DomainEvent @event)
        {
            CallForSpeechEvaulatedEvent callForSpeechRejectedEvent =
                @event as CallForSpeechEvaulatedEvent;

            var cfs = _mapper.Map<CallForSpeech>(callForSpeechRejectedEvent);

            var execution = await
                _ZEsCallForSpeechRepository
                .SaveEvaluatationAsync
                (cfs.Id, cfs.ScoreResult, cfs.Status);

            return execution;
        }
    }
}
