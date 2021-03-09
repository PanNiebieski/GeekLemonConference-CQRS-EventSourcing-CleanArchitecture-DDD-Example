using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.Domain;
using GeekLemonConference.DomainEvents.Ddd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using AutoMapper;
using GeekLemonConference.Domain.Entity;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers.CallForSpeeches
{
    public class SubscribePreminalAcceptCallForSpeech : SubscribeBase
    {
        private IZEsCallForSpeechRepository _ZEsCallForSpeechRepository;
        private IMapper _mapper;

        public SubscribePreminalAcceptCallForSpeech(
            IZEsCallForSpeechRepository zEsCallForSpeechRepository,
            IMapper mapper) :
            base()
        {
            _ZEsCallForSpeechRepository = zEsCallForSpeechRepository;
            _mapper = mapper;
        }

        public override string QUEUE_Name => Constants.QUEUE_CALLFORSPEECH_PRELIMINARY_ACCEPT;

        public override DomainEvent DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<CallForSpeechPreliminaryAcceptEvent>(json);
        }

        public async override Task<ExecutionStatus> HandleEvent(DomainEvent @event)
        {
            CallForSpeechPreliminaryAcceptEvent callForSpeechRejectedEvent =
                @event as CallForSpeechPreliminaryAcceptEvent;

            var cfs = _mapper.Map<CallForSpeech>(callForSpeechRejectedEvent);

            var execution = await
                _ZEsCallForSpeechRepository
                .SavePreliminaryAcceptenceAsync
                (cfs.UniqueId, cfs.PreliminaryDecision.DecisionBy, cfs.Status);

            return execution;
        }
    }
}
