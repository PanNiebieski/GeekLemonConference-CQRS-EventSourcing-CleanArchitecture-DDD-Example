using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Queries.GetCallForSpeech;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Application.Contracts.Persistence.WithES;

namespace GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetCallForSpeech
{
    public class GetCallForSpeechQueryHandler :
                IRequestHandler<GetCallForSpeechQuery, GetCallForSpeechQueryHandlerResponse>
    {

        private readonly ICallForSpeechRepository _callRepository;
        private readonly IMapper _mapper;
        private readonly IZEsCallForSpeechRepository _zEscallRepository;

        public GetCallForSpeechQueryHandler(ICallForSpeechRepository callRepository,
             IZEsCallForSpeechRepository zEscallRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _zEscallRepository = zEscallRepository;
            _callRepository = callRepository;
        }

        public async Task<GetCallForSpeechQueryHandlerResponse> Handle(GetCallForSpeechQuery request, CancellationToken cancellationToken)
        {
            ExecutionStatus<CallForSpeech> databaseOperationCfs = null;


            if (request.CallForSpeechUniqueId != null)
                if (request.queryWitchDataBase == QueryWitchDataBase.WithEventSourcing)
                    databaseOperationCfs = await _zEscallRepository.GetByIdAsync(request.CallForSpeechUniqueId);
                else
                    databaseOperationCfs = await _callRepository.GetByIdAsync(request.CallForSpeechUniqueId);
            else
                if (request.queryWitchDataBase == QueryWitchDataBase.WithEventSourcing)
                databaseOperationCfs = await _zEscallRepository.GetByIdAsync(request.CallForSpeechId);
            else
                databaseOperationCfs = await _callRepository.GetByIdAsync(request.CallForSpeechId);



            if (!databaseOperationCfs.Success)
                return new GetCallForSpeechQueryHandlerResponse(databaseOperationCfs
                    .RemoveGeneric());

            var cfsMaped = _mapper.Map<CallForSpeechViewModel>(databaseOperationCfs.Value);

            return new GetCallForSpeechQueryHandlerResponse(cfsMaped);
        }
    }
}
