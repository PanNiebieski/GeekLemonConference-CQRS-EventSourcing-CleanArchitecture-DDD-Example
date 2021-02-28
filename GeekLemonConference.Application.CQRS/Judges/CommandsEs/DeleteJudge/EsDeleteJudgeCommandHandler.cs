using AutoMapper;
using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Application.EventSourcing.Aggregates;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.CommandsEs.DeleteJudge
{
    public class EsDeleteJudgeCommandHandler
          : IRequestHandler<EsDeleteJudgeCommand, EsDeleteJudgeCommandResponse>
    {
        private readonly ISessionForEventSourcing _sessionForEventSourcing;
        private readonly IMapper _mapper;

        public EsDeleteJudgeCommandHandler(
            ISessionForEventSourcing sessionForEventSourcing, IMapper mapper)
        {
            _mapper = mapper;
            _sessionForEventSourcing = sessionForEventSourcing;
        }

        public Task<EsDeleteJudgeCommandResponse> Handle(EsDeleteJudgeCommand request, CancellationToken cancellationToken)
        {
            var eventstoreResult = Get<JudgeAggregate>(request.
                UniqueId.GetAggregateKey());

            if (!eventstoreResult.Success)
                return Task.FromResult(new
                    EsDeleteJudgeCommandResponse(eventstoreResult.RemoveGeneric()));

            eventstoreResult.Value.Delete(request.UniqueId, eventstoreResult.Value.Version);
            _sessionForEventSourcing.Commit();

            return Task.FromResult(new EsDeleteJudgeCommandResponse());
        }

        private ExecutionStatus<T> Get<T>(AggregateKey id, int? expectedVersion = null) where T : AggregateRoot
        {
            var a = _sessionForEventSourcing.Get<T>(id, expectedVersion);
            return a;
        }
    }
}
