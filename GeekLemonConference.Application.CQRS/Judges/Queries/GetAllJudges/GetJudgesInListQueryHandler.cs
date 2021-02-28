using AutoMapper;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.Judges.GetAllJudges
{
    public class GetJudgesInListQueryHandler :
        IRequestHandler<GetJudgesInListQuery, GetJudgesInListQueryResponse>
    {
        private readonly IJudgeRepository _judgeRepository;
        private readonly IZEsJudgeRepository _zEsjudgeRepository;
        private readonly IMapper _mapper;

        public GetJudgesInListQueryHandler(IMapper mapper,
            IJudgeRepository judgeRepository,
            IZEsJudgeRepository zEsjudgeRepository)
        {
            _mapper = mapper;
            _judgeRepository = judgeRepository;
            _zEsjudgeRepository = zEsjudgeRepository;
        }


        public async Task<GetJudgesInListQueryResponse> Handle
            (GetJudgesInListQuery request, CancellationToken cancellationToken)
        {
            ExecutionStatus<IReadOnlyList<Judge>> databaseoperation =
                null;

            if (request.queryWitchDataBase == QueryWitchDataBase.WithEventSourcing)
                databaseoperation = await _zEsjudgeRepository.GetAllAsync();
            else
                databaseoperation = await _judgeRepository.GetAllAsync();

            if (!databaseoperation.Success)
                return new GetJudgesInListQueryResponse
                    (databaseoperation.RemoveGeneric());

            var ordered = databaseoperation.Value.OrderBy(a => a.Name.Last);
            var maped = _mapper.Map<List<JudgesInListViewModel>>(ordered);

            return new GetJudgesInListQueryResponse(maped);
        }
    }
}
