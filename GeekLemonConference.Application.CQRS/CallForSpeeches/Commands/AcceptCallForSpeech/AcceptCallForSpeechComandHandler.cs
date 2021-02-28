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

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.AcceptCallForSpeech
{
    public class AcceptCallForSpeechComandHandler
        : IRequestHandler<AcceptCallForSpeechCommand, AcceptCallForSpeechCommandResponse>
    {
        private readonly ICallForSpeechRepository _callRepository;
        private readonly IJudgeRepository _judegRepository;
        private readonly IMapper _mapper;

        public AcceptCallForSpeechComandHandler(ICallForSpeechRepository callRepository,
            IJudgeRepository judegRepository, IMapper mapper
           )
        {
            _callRepository = callRepository;
            _judegRepository = judegRepository;
            _mapper = mapper;
        }


        public async Task<AcceptCallForSpeechCommandResponse> Handle(AcceptCallForSpeechCommand request, CancellationToken cancellationToken)
        {
            var cfsuniqueId = _mapper.Map<CallForSpeechUniqueId>(request.CallForSpeechUniqueId);
            var judgeId = _mapper.Map<JudgeId>(request.JudgeId);

            var databaseOperationCfs = await _callRepository.GetByIdAsync(cfsuniqueId);
            var databaseOperationJudge = await _judegRepository.GetByIdAsync(judgeId);

            if (!databaseOperationCfs.Success)
            {
                if (databaseOperationCfs.Reason == Reason.ReturnedNull)
                    return new AcceptCallForSpeechCommandResponse(ResponseStatus.NotFoundInDataBase);
                if (databaseOperationCfs.Reason == Reason.Error)
                    return new AcceptCallForSpeechCommandResponse(ResponseStatus.DataBaseError);
            }

            if (!databaseOperationJudge.Success)
            {
                if (databaseOperationJudge.Reason == Reason.ReturnedNull)
                    return new AcceptCallForSpeechCommandResponse(ResponseStatus.NotFoundInDataBase);
                if (databaseOperationJudge.Reason == Reason.Error)
                    return new AcceptCallForSpeechCommandResponse(ResponseStatus.DataBaseError);
            }

            var cfs = databaseOperationCfs.Value;
            var judge = databaseOperationJudge.Value;

            var result = cfs.TryAccept(judge);
            if (!result.Success)
                return new AcceptCallForSpeechCommandResponse(result);

            var saveop = await _callRepository.SaveAcceptenceAsync(cfsuniqueId, judgeId, cfs.Status);

            return new AcceptCallForSpeechCommandResponse(saveop);
        }


    }
}
