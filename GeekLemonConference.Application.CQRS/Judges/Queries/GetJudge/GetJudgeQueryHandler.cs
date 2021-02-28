using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;

namespace GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge
{
    public class GetJudgeQueryHandler :
        IRequestHandler<GetJudgeQuery, GetJudgeQueryResponse>
    {
        private readonly IJudgeRepository _judgeRepository;
        private readonly IMapper _mapper;
        private readonly IZEsJudgeRepository _zEsjudgeRepository;

        public GetJudgeQueryHandler(IMapper mapper,
            IJudgeRepository judgeRepository,
            IZEsJudgeRepository zEsjudgeRepository)
        {
            _mapper = mapper;
            _judgeRepository = judgeRepository;
            _zEsjudgeRepository = zEsjudgeRepository;
        }



        public async Task<GetJudgeQueryResponse> Handle(GetJudgeQuery request, CancellationToken cancellationToken)
        {
            ExecutionStatus<Judge> databaseoperation;
            if (request.JudgeUniqueId != null)
                if (request.queryWitchDataBase == QueryWitchDataBase.WithEventSourcing)
                    databaseoperation = await _zEsjudgeRepository.GetByIdAsync(request.JudgeUniqueId);
                else
                    databaseoperation = await _judgeRepository.GetByIdAsync(request.JudgeUniqueId);
            else
                if (request.queryWitchDataBase == QueryWitchDataBase.WithEventSourcing)
                databaseoperation = await _zEsjudgeRepository.GetByIdAsync(request.JudeId);
            else
                databaseoperation = await _judgeRepository.GetByIdAsync(request.JudeId);

            if (!databaseoperation.Success)
                return new GetJudgeQueryResponse(databaseoperation.RemoveGeneric());

            var judgeViewModel = _mapper.Map<JudgeViewModel>(databaseoperation.Value);

            return new GetJudgeQueryResponse(judgeViewModel);
        }
    }
}
