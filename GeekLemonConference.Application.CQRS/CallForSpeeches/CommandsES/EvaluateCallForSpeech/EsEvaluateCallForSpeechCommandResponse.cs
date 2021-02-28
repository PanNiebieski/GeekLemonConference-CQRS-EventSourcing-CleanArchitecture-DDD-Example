using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.EvaluateCallForSpeech
{
    public class EsEvaluateCallForSpeechCommandResponse
: BaseResponse
    {
        public EsEvaluateCallForSpeechCommandResponse()
    : base()
        {

        }

        public ScoreDto Score { get; set; }

        public EsEvaluateCallForSpeechCommandResponse(ScoreDto score) : base()
        {
            Score = score;
        }


        public EsEvaluateCallForSpeechCommandResponse(ResponseStatus status)
            : base(status)
        {

        }

        public EsEvaluateCallForSpeechCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public EsEvaluateCallForSpeechCommandResponse(ExecutionStatus status, string message)
: base(status, message)
        {

        }

        public EsEvaluateCallForSpeechCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public EsEvaluateCallForSpeechCommandResponse(string message)
        : base(message)
        { }

        public EsEvaluateCallForSpeechCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
