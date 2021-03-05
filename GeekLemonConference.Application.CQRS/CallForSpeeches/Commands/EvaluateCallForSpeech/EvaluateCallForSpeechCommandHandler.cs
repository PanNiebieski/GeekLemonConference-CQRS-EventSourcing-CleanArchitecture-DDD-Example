using AutoMapper;
using GeekLemonConference.Application.Contracts;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeekLemonConference.Application.CQRS.Dto;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.EvaluateCallForSpeech
{
    public class EvaluateCallForSpeechCommandHandler :
           IRequestHandler<EvaluateCallForSpeechCommand,
               EvaluateCallForSpeechCommandResponse>
    {
        private readonly ICallForSpeechRepository _callRepository;
        private readonly IScoringRulesFactory _scoringRulesFactory;
        private readonly IMapper _mapper;

        public EvaluateCallForSpeechCommandHandler(ICallForSpeechRepository callRepository,
             IScoringRulesFactory scoringRulesFactory, IMapper mapper)
        {
            _scoringRulesFactory = scoringRulesFactory;
            _callRepository = callRepository;
            _mapper = mapper;
        }

        public async Task<EvaluateCallForSpeechCommandResponse> Handle(EvaluateCallForSpeechCommand request,
            CancellationToken cancellationToken)
        {
            var idc = new CallForSpeechUniqueId(request.CallForSpeechUniqueId);
            var databaseOperation = await _callRepository.GetByIdAsync(idc);

            if (databaseOperation.Success == false)
            {
                if (databaseOperation.Reason == Reason.ReturnedNull)
                    return new EvaluateCallForSpeechCommandResponse(ResponseStatus.BadQuery);
                if (databaseOperation.Reason == Reason.Error)
                    return new EvaluateCallForSpeechCommandResponse(ResponseStatus.DataBaseError);
            }

            //try
            //{
            //    cfs.Evaluate(_scoringRulesFactory.DefaultSet);
            //}
            //catch (Exception ex)
            //{
            //    return new EvaluateCallForSpeechCommandResponse();
            //}
            var cfs = databaseOperation.Value;
            var result = cfs.TryEvaluate(_scoringRulesFactory.DefaultSet);
            if (!result.Success)
                return new EvaluateCallForSpeechCommandResponse(result);

            var saveop = await _callRepository.SaveEvaluatationAsync(idc, cfs.ScoreResult, cfs.Status);

            if (!saveop.Success)
                return new EvaluateCallForSpeechCommandResponse(saveop);

            var scoredto = _mapper.Map<ScoreDto>(cfs.ScoreResult);
            return new EvaluateCallForSpeechCommandResponse(scoredto);
        }

    }
}
