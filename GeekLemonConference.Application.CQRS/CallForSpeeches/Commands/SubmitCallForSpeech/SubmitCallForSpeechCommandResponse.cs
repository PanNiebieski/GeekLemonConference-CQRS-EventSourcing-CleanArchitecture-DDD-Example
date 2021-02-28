using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.SubmitCallForSpeech
{
    public class SubmitCallForSpeechCommandResponse : BaseResponse
    {
        public CallForSpeechIds CallForSpeechCommandIds { get; set; }

        public SubmitCallForSpeechCommandResponse() : base()
        { }

        public SubmitCallForSpeechCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public SubmitCallForSpeechCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public SubmitCallForSpeechCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public SubmitCallForSpeechCommandResponse(string message)
        : base(message)
        { }

        public SubmitCallForSpeechCommandResponse(string message, bool success)
            : base(message, success)
        { }

        public SubmitCallForSpeechCommandResponse(CallForSpeechIds callForSpeechid)
        {
            CallForSpeechCommandIds = callForSpeechid;
        }
    }
}
