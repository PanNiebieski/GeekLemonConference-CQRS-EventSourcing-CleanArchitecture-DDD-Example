using AutoMapper;
using GeekLemonConference.Application.Contracts;
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

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.RejectCallForSpeech
{
    public class RejectCallForSpeechCommandHandler :
        IRequestHandler<RejectCallForSpeechCommand, RejectCallForSpeechCommandResponse>
    {
        private readonly ICallForSpeechRepository _callRepository;
        private readonly IJudgeRepository _judegRepository;
        private readonly IMapper _mapper;

        public RejectCallForSpeechCommandHandler(ICallForSpeechRepository callRepository,
            IJudgeRepository judegRepository, IMapper mapper
           )
        {
            _callRepository = callRepository;
            _judegRepository = judegRepository;
            _mapper = mapper;
        }


        public async Task<RejectCallForSpeechCommandResponse> Handle(RejectCallForSpeechCommand request, CancellationToken cancellationToken)
        {
            var cfsuniqueId = _mapper.Map<CallForSpeechUniqueId>(request.CallForSpeechUniqueId);
            var judgeId = _mapper.Map<JudgeId>(request.JudgeId);

            var databaseOperationCfs = await _callRepository.GetByIdAsync(cfsuniqueId);
            var databaseOperationJudge = await _judegRepository.GetByIdAsync(judgeId);

            if (!databaseOperationCfs.Success)
            {
                if (databaseOperationCfs.Reason == Reason.ReturnedNull)
                    return new RejectCallForSpeechCommandResponse(ResponseStatus.NotFoundInDataBase);
                if (databaseOperationCfs.Reason == Reason.Error)
                    return new RejectCallForSpeechCommandResponse(ResponseStatus.DataBaseError);
            }

            if (!databaseOperationJudge.Success)
            {
                if (databaseOperationJudge.Reason == Reason.ReturnedNull)
                    return new RejectCallForSpeechCommandResponse(ResponseStatus.NotFoundInDataBase);
                if (databaseOperationJudge.Reason == Reason.Error)
                    return new RejectCallForSpeechCommandResponse(ResponseStatus.DataBaseError);
            }

            var cfs = databaseOperationCfs.Value;
            var judge = databaseOperationJudge.Value;

            var result = cfs.TryReject(judge);
            if (!result.Success)
                return new RejectCallForSpeechCommandResponse(result);

            await _callRepository.SaveRejectionAsync(cfsuniqueId, judgeId, cfs.Status);

            return new RejectCallForSpeechCommandResponse();
        }




    }
}
