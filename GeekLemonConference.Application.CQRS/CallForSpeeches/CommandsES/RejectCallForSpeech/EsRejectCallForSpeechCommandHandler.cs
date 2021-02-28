using AutoMapper;
using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
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

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.RejectCallForSpeech
{
    public class EsRejectCallForSpeechCommandHandler
: IRequestHandler<EsRejectCallForSpeechCommand, EsRejectCallForSpeechCommandResponse>
    {


        private readonly ISessionForEventSourcing _sessionForEventSourcing;
        private readonly IMapper _mapper;
        private readonly IZEsJudgeRepository _zEsJudgeRepository;

        public EsRejectCallForSpeechCommandHandler(
            ISessionForEventSourcing sessionForEventSourcing, IMapper mapper,
            IZEsJudgeRepository zEsJudgeRepository)
        {
            _mapper = mapper;
            _sessionForEventSourcing = sessionForEventSourcing;
            _zEsJudgeRepository = zEsJudgeRepository;
        }

        public async Task<EsRejectCallForSpeechCommandResponse> Handle(EsRejectCallForSpeechCommand request, CancellationToken cancellationToken)
        {
            var cfsuniqueId = _mapper.Map<CallForSpeechUniqueId>(request.CallForSpeechUniqueId);
            var judgeId = _mapper.Map<JudgeId>(request.JudgeId);

            var databaseOperationJudge = await _zEsJudgeRepository.GetByIdAsync(judgeId);

            if (!databaseOperationJudge.Success)
                return new EsRejectCallForSpeechCommandResponse(databaseOperationJudge
                    .RemoveGeneric());

            var judge = databaseOperationJudge.Value;

            var eventstoreResult = Get<CallForSpeechAggregate>
                (cfsuniqueId.GetAggregateKey());

            if (!eventstoreResult.Success)
                return new EsRejectCallForSpeechCommandResponse
                    (eventstoreResult.RemoveGeneric());

            var aggregateCallForSpeaker = eventstoreResult.Value;

            if (aggregateCallForSpeaker.Version > request.Version)
                return new EsRejectCallForSpeechCommandResponse
                    (ExecutionStatus.EventStoreConcurrencyError(@$"You sended old version.
                    Yours {request.Version}. In Event database :{aggregateCallForSpeaker.Version}"));

            var csf = _mapper.Map<CallForSpeech>(aggregateCallForSpeaker);



            var domainLogicResult = csf.TryReject(judge);
            if (!domainLogicResult.Success)
                return new EsRejectCallForSpeechCommandResponse(domainLogicResult);

            aggregateCallForSpeaker.Rejected(csf);
            _sessionForEventSourcing.Commit();

            return new EsRejectCallForSpeechCommandResponse();
        }

        private ExecutionStatus<T> Get<T>(AggregateKey id, int? expectedVersion = null) where T : AggregateRoot
        {
            var a = _sessionForEventSourcing.Get<T>(id, expectedVersion);
            return a;
        }
    }
}
