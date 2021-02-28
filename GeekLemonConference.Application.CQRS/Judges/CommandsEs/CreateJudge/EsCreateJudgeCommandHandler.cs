using AutoMapper;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Application.EventSourcing.Aggregates;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.CommandsEs.CreateJudge
{
    public class EsCreateJudgeCommandHandler
          : IRequestHandler<EsCreateJudgeCommand, EsCreateJudgeCommandResponse>
    {
        private readonly ISessionForEventSourcing _sessionForEventSourcing;
        private readonly IMapper _mapper;

        public EsCreateJudgeCommandHandler(
            ISessionForEventSourcing sessionForEventSourcing, IMapper mapper)
        {
            _mapper = mapper;
            _sessionForEventSourcing = sessionForEventSourcing;
        }

        public async Task<EsCreateJudgeCommandResponse> Handle
            (EsCreateJudgeCommand request, CancellationToken cancellationToken)
        {
            var validator = new EsCreateJudgeCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new EsCreateJudgeCommandResponse(validatorResult);

            var judge = _mapper.Map<Judge>(request);

            var item = new JudgeAggregate(judge);

            _sessionForEventSourcing.Add<JudgeAggregate>(item);
            _sessionForEventSourcing.Commit();

            var ids = _mapper.Map<IdsDto>(judge.Ids());
            return new EsCreateJudgeCommandResponse(ids);
        }
    }
}
