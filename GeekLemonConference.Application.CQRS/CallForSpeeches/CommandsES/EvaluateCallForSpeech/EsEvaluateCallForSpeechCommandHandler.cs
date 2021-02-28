using AutoMapper;
using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Application.Contracts;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.EvaluateCallForSpeech;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Application.EventSourcing.Aggregates;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.EvaluateCallForSpeech
{
    class EsEvaluateCallForSpeechCommandHandler
        : IRequestHandler<EsEvaluateCallForSpeechCommand, EsEvaluateCallForSpeechCommandResponse>
    {
        private readonly ISessionForEventSourcing _sessionForEventSourcing;
        private readonly IMapper _mapper;
        private readonly IScoringRulesFactory _scoringRulesFactory;

        public EsEvaluateCallForSpeechCommandHandler(
            ISessionForEventSourcing sessionForEventSourcing, IMapper mapper,
            IScoringRulesFactory scoringRulesFactory)
        {
            _mapper = mapper;
            _sessionForEventSourcing = sessionForEventSourcing;
            _scoringRulesFactory = scoringRulesFactory;
        }


        public async Task<EsEvaluateCallForSpeechCommandResponse> Handle(EsEvaluateCallForSpeechCommand request, CancellationToken cancellationToken)
        {
            var cfsuniqueId = _mapper.Map<CallForSpeechUniqueId>
                (request.CallForSpeechIdUnique);

            var eventstoreResult = Get<CallForSpeechAggregate>
                (cfsuniqueId.GetAggregateKey());

            if (!eventstoreResult.Success)
                return await Task.FromResult(new EsEvaluateCallForSpeechCommandResponse());

            var aggregateCallForSpeaker = eventstoreResult.Value;

            if (aggregateCallForSpeaker.Version > request.Version)
                return new EsEvaluateCallForSpeechCommandResponse
                    (ExecutionStatus.EventStoreConcurrencyError(@$"You sended old version.
                    Yours {request.Version}. In Event database :{aggregateCallForSpeaker.Version}"));


            var csf = _mapper.Map<CallForSpeech>(aggregateCallForSpeaker);

            var domainLogicResult = csf.TryEvaluate(_scoringRulesFactory.DefaultSet);
            if (!domainLogicResult.Success)
                return new EsEvaluateCallForSpeechCommandResponse(domainLogicResult);

            aggregateCallForSpeaker.Evaulated(csf);
            _sessionForEventSourcing.Commit();

            var scoredto = _mapper.Map<ScoreDto>(csf.Score);
            return new EsEvaluateCallForSpeechCommandResponse(scoredto);
        }

        private ExecutionStatus<T> Get<T>(AggregateKey id, int? expectedVersion = null) where T : AggregateRoot
        {
            var a = _sessionForEventSourcing.Get<T>(id, expectedVersion);
            return a;
        }
    }
}
