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
    public class SubscribeSubmitCallForSpeech : SubscribeBase
    {
        private IZEsCallForSpeechRepository _ZEsCallForSpeechRepository;
        private IMapper _mapper;

        public SubscribeSubmitCallForSpeech(IZEsCallForSpeechRepository zEsCallForSpeechRepository,
            IMapper mapper) :
            base()
        {
            _ZEsCallForSpeechRepository = zEsCallForSpeechRepository;
            _mapper = mapper;
        }


        public override string QUEUE_Name => Constants.QUEUE_CALLFORSPEECH_SUBMITC;

        public override DomainEvent DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<CallForSpeechSubmitedEvent>(json);
        }

        public async override Task<ExecutionStatus> HandleEvent(DomainEvent @event)
        {
            CallForSpeechSubmitedEvent callForSpeechSubmitEvent = @event as CallForSpeechSubmitedEvent;

            var cfs = _mapper.Map<CallForSpeech>(callForSpeechSubmitEvent);

            var execution = await
                _ZEsCallForSpeechRepository.SubmitAsync(cfs);

            return execution.RemoveGeneric();
        }
    }
}
