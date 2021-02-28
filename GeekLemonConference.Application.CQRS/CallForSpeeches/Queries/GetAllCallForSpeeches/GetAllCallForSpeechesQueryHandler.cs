using AutoMapper;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Queries.GetAllCallForSpeeches;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetAllCallForSpeeches
{
    public class GetAllCallForSpeechesQueryHandler :
             IRequestHandler<GetAllCallForSpeechesQuery, GetAllCallForSpeechesQueryHandlerResponse>
    {
        private readonly ICallForSpeechRepository _callRepository;
        private readonly IZEsCallForSpeechRepository _zEscallRepository;

        private readonly IMapper _mapper;

        public GetAllCallForSpeechesQueryHandler(ICallForSpeechRepository callRepository,
            IZEsCallForSpeechRepository ZEscallRepository,

            IMapper mapper)
        {
            _mapper = mapper;
            _zEscallRepository = ZEscallRepository;
            _callRepository = callRepository;
        }

        public async Task<GetAllCallForSpeechesQueryHandlerResponse> Handle(GetAllCallForSpeechesQuery request, CancellationToken cancellationToken)
        {
            ExecutionStatus<IReadOnlyList<CallForSpeech>> databaseresult = null;

            if (request.queryWitchDataBase == QueryWitchDataBase.WithEventSourcing)
                databaseresult = await _zEscallRepository.GetCollectionAsync(request.Filter);
            else
                databaseresult = await _callRepository.GetCollectionAsync(request.Filter);

            if (databaseresult.Success)
            {
                var allordered = databaseresult.Value.OrderBy(x => x.Id);
                var allmaped = _mapper.Map<List<CallForSpeechInListViewModel>>(databaseresult.Value);
                return new GetAllCallForSpeechesQueryHandlerResponse(allmaped);
            }

            return new GetAllCallForSpeechesQueryHandlerResponse(databaseresult.RemoveGeneric());
        }
    }
}
