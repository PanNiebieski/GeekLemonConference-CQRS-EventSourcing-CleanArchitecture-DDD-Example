using AutoMapper;
using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Application.EventSourcing.Aggregates;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.SubmitCallForSpeech
{
    public class EsSubmitCallForSpeechCommandHandler
      : IRequestHandler<EsSubmitCallForSpeechCommand, EsSubmitCallForSpeechCommandResponse>
    {
        private readonly ISessionForEventSourcing _sessionForEventSourcing;
        private readonly IMapper _mapper;


        public EsSubmitCallForSpeechCommandHandler(
            ISessionForEventSourcing sessionForEventSourcing, IMapper mapper)
        {
            _mapper = mapper;
            _sessionForEventSourcing = sessionForEventSourcing;
        }


        public async Task<EsSubmitCallForSpeechCommandResponse> Handle
            (EsSubmitCallForSpeechCommand request, CancellationToken cancellationToken)
        {
            var validator = new EsSubmitCallForSpeechCommandValidation();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new EsSubmitCallForSpeechCommandResponse(validatorResult);

            var csf = _mapper.Map<CallForSpeech>(request);

            var item = new CallForSpeechAggregate(csf);

            _sessionForEventSourcing.Add<CallForSpeechAggregate>(item);
            _sessionForEventSourcing.Commit();

            var ids = _mapper.Map<IdsDto>(csf.Ids());
            return new EsSubmitCallForSpeechCommandResponse(ids);
        }


    }
}
