using AutoMapper;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.DomainEvents.Ddd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers.CallForSpeeches
{
    public class SubscribeAcceptCallForSpeech : SubscribeBase
    {
        private IZEsCallForSpeechRepository _ZEsCallForSpeechRepository;
        private IMapper _mapper;

        public SubscribeAcceptCallForSpeech(
            IZEsCallForSpeechRepository zEsCallForSpeechRepository,
            IMapper mapper) :
            base()
        {
            _ZEsCallForSpeechRepository = zEsCallForSpeechRepository;
            _mapper = mapper;
        }

        public override string QUEUE_Name => Constants.QUEUE_CALLFORSPEECH_ACCEPT;

        public override DomainEvent DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<CallForSpeechAcceptedEvent>(json);
        }

        public async override Task<ExecutionStatus> HandleEvent(DomainEvent @event)
        {
            CallForSpeechEvaulatedEvent callForSpeechRejectedEvent =
                @event as CallForSpeechEvaulatedEvent;

            var cfs = _mapper.Map<CallForSpeech>(callForSpeechRejectedEvent);

            var execution = await
                _ZEsCallForSpeechRepository
                .SaveEvaluatationAsync
                (cfs.Id, cfs.Score, cfs.Status);

            return execution;
        }
    }
}
