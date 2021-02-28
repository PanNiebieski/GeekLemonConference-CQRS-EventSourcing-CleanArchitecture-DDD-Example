using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.EvaluateCallForSpeech
{
    public class EvaluateCallForSpeechCommandResponse : BaseResponse
    {
        public EvaluateCallForSpeechCommandResponse()
    : base()
        {

        }

        public ScoreDto Score { get; set; }

        public EvaluateCallForSpeechCommandResponse(ScoreDto score) : base()
        {
            Score = score;
        }


        public EvaluateCallForSpeechCommandResponse(ResponseStatus status)
            : base(status)
        {

        }

        public EvaluateCallForSpeechCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public EvaluateCallForSpeechCommandResponse(ExecutionStatus status, string message)
: base(status, message)
        {

        }

        public EvaluateCallForSpeechCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public EvaluateCallForSpeechCommandResponse(string message)
        : base(message)
        { }

        public EvaluateCallForSpeechCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
