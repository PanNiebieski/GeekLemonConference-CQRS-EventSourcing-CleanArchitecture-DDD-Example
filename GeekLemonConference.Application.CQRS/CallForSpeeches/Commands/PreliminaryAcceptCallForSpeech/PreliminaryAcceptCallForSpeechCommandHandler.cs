using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.PreliminaryAcceptCallForSpeech
{
    public class PreliminaryAcceptCallForSpeechCommandHandler :
          IRequestHandler<PreliminaryAcceptCallForSpeechCommand, PreliminaryAcceptCallForSpeechCommandResponse>
    {
        private readonly ICallForSpeechRepository _callRepository;
        private readonly IJudgeRepository _judegRepository;
        private readonly IMapper _mapper;

        public PreliminaryAcceptCallForSpeechCommandHandler(ICallForSpeechRepository callRepository,
            IJudgeRepository judegRepository, IMapper mapper
           )
        {
            _callRepository = callRepository;
            _judegRepository = judegRepository;
            _mapper = mapper;
        }

        public async Task<PreliminaryAcceptCallForSpeechCommandResponse> Handle(PreliminaryAcceptCallForSpeechCommand request, CancellationToken cancellationToken)
        {
            var cfsuniqueId = _mapper.Map<CallForSpeechUniqueId>(request.CallForSpeechUniqueId);
            var judgeId = _mapper.Map<JudgeId>(request.JudgeId);

            var databaseOperationCfs = await _callRepository.GetByIdAsync(cfsuniqueId);
            var databaseOperationJudge = await _judegRepository.GetByIdAsync(judgeId);

            if (!databaseOperationCfs.Success)
                return new PreliminaryAcceptCallForSpeechCommandResponse(databaseOperationCfs
                                       .RemoveGeneric(), "CallForSpeech Problem");

            if (!databaseOperationJudge.Success)
                return new PreliminaryAcceptCallForSpeechCommandResponse(databaseOperationJudge
                                       .RemoveGeneric(), "Judge Problem");

            var cfs = databaseOperationCfs.Value;
            var result = cfs.TryPreliminaryAccept(databaseOperationJudge.Value);

            if (!result.Success)
                return new PreliminaryAcceptCallForSpeechCommandResponse(result);

            await _callRepository.SavePreliminaryAcceptenceAsync(cfsuniqueId, judgeId, cfs.Status);

            return new PreliminaryAcceptCallForSpeechCommandResponse();

        }



    }
}
