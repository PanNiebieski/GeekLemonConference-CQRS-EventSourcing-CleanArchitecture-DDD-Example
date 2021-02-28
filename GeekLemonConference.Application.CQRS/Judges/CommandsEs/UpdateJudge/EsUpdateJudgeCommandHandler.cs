using AutoMapper;
using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Judges.Commands.UpdateJudge;
using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Application.EventSourcing.Aggregates;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.CommandsEs.UpdateJudge
{
    public class EsUpdateJudgeCommandHandler :
        IRequestHandler<EsUpdateJudgeCommand, EsUpdateJudgeCommandResponse>
    {
        private readonly ISessionForEventSourcing _sessionForEventSourcing;
        private readonly IMapper _mapper;

        public EsUpdateJudgeCommandHandler(
            ISessionForEventSourcing sessionForEventSourcing, IMapper mapper)
        {
            _mapper = mapper;
            _sessionForEventSourcing = sessionForEventSourcing;
        }

        public async Task<EsUpdateJudgeCommandResponse> Handle(EsUpdateJudgeCommand request, CancellationToken cancellationToken)
        {
            var validator = new EsUpdateJudgeCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new EsUpdateJudgeCommandResponse(validatorResult);

            var judgedto = _mapper.Map<JudgeDto>(request);
            var judge = _mapper.Map<Judge>(judgedto);

            var eventstoreResult = Get<JudgeAggregate>(judge.UniqueId.GetAggregateKey());

            if (!eventstoreResult.Success)
                return await Task.FromResult(new
                    EsUpdateJudgeCommandResponse(eventstoreResult.RemoveGeneric()));

            if (eventstoreResult.Value.Version > judge.Version)
                return new EsUpdateJudgeCommandResponse
                    (ExecutionStatus.EventStoreConcurrencyError
                    (@$"You sended old version. Your version {judge.Version}. In Event database version :{eventstoreResult.Value.Version}"));

            eventstoreResult.Value.Update(judge);
            _sessionForEventSourcing.Commit();

            return new EsUpdateJudgeCommandResponse();
        }



        private ExecutionStatus<T> Get<T>(AggregateKey id, int? expectedVersion = null) where T : AggregateRoot
        {
            return _sessionForEventSourcing.Get<T>(id, expectedVersion);
        }
    }

}