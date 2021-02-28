using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.Domain;
using GeekLemonConference.DomainEvents.CallForSpeeches;
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
    public class SubscribeRejectCallForSpeech : SubscribeBase
    {
        private IZEsCallForSpeechRepository _ZEsCallForSpeechRepository;
        private IMapper _mapper;

        public SubscribeRejectCallForSpeech(
            IZEsCallForSpeechRepository zEsCallForSpeechRepository,
            IMapper mapper) :
            base()
        {
            _ZEsCallForSpeechRepository = zEsCallForSpeechRepository;
            _mapper = mapper;
        }

        public override string QUEUE_Name => Constants.QUEUE_CALLFORSPEECH_REJECTC;

        public override DomainEvent DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<CallForSpeechRejectedEvent>(json);
        }

        public override async Task<ExecutionStatus> HandleEvent(DomainEvent @event)
        {
            CallForSpeechRejectedEvent callForSpeechRejectedEvent =
                @event as CallForSpeechRejectedEvent;

            var cfs = _mapper.Map<CallForSpeech>(callForSpeechRejectedEvent);

            var execution = await
                _ZEsCallForSpeechRepository
                .SaveRejectionAsync(cfs.Id, cfs.FinalDecision.DecisionBy, cfs.Status);

            return execution;
        }
    }
}
