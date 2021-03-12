using AutoMapper;
using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Application.Contracts;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.EvaluateCallForSpeech;
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

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.PreliminaryAcceptCallForSpeech
{
    public class EsPreliminaryAcceptCallForSpeechCommandHandler
        : IRequestHandler<EsPreliminaryAcceptCallForSpeechCommand, EsPreliminaryAcceptCallForSpeechCommandResponse>
    {
        private readonly ISessionForEventSourcing _sessionForEventSourcing;
        private readonly IMapper _mapper;
        private readonly IZEsJudgeRepository _zEsJudgeRepository;

        public EsPreliminaryAcceptCallForSpeechCommandHandler(
            ISessionForEventSourcing sessionForEventSourcing, IMapper mapper,
            IZEsJudgeRepository zEsJudgeRepository)
        {
            _mapper = mapper;
            _sessionForEventSourcing = sessionForEventSourcing;
            _zEsJudgeRepository = zEsJudgeRepository;
        }


        public async Task<EsPreliminaryAcceptCallForSpeechCommandResponse> Handle(EsPreliminaryAcceptCallForSpeechCommand request, CancellationToken cancellationToken)
        {
            var cfsuniqueId = _mapper.Map<CallForSpeechUniqueId>(request.CallForSpeechUniqueId);
            var judgeId = _mapper.Map<JudgeId>(request.JudgeId);

            var databaseOperationJudge = await _zEsJudgeRepository.GetByIdAsync(judgeId);

            if (!databaseOperationJudge.Success)
                return new EsPreliminaryAcceptCallForSpeechCommandResponse
                    (databaseOperationJudge.RemoveGeneric(),
                    "CallForSpeech Problem");


            var judge = databaseOperationJudge.Value;

            var eventstoreResult = Get<CallForSpeechAggregate>
                (cfsuniqueId.GetAggregateKey());

            if (!eventstoreResult.Success)
                return new EsPreliminaryAcceptCallForSpeechCommandResponse
                    (eventstoreResult.RemoveGeneric());

            var aggregateCallForSpeaker = eventstoreResult.Value;

            if ((eventstoreResult.Value.Version - 1) > (request.Version))
                return new EsPreliminaryAcceptCallForSpeechCommandResponse
                    (ExecutionStatus.EventStoreConcurrencyError(@$"You sended old version.
                    Yours {request.Version}. Should be :{aggregateCallForSpeaker.Version - 1}"));


            var csf = _mapper.Map<CallForSpeech>(aggregateCallForSpeaker);

            var domainLogicResult = csf.TryPreliminaryAccept(judge);
            if (!domainLogicResult.Success)
                return new EsPreliminaryAcceptCallForSpeechCommandResponse(domainLogicResult);

            aggregateCallForSpeaker.PreliminaryAccepted(csf);
            _sessionForEventSourcing.Commit();

            return new EsPreliminaryAcceptCallForSpeechCommandResponse();
        }

        private ExecutionStatus<T> Get<T>(AggregateKey id, int? expectedVersion = null) where T : AggregateRoot
        {
            var a = _sessionForEventSourcing.Get<T>(id, expectedVersion);
            return a;
        }
    }
}
