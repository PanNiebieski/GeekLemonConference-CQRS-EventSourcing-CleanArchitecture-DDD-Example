using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.RejectCallForSpeech
{
    public class RejectCallForSpeechCommandResponse : BaseResponse
    {
        public RejectCallForSpeechCommandResponse()
            : base()
        {

        }

        public RejectCallForSpeechCommandResponse(ResponseStatus status)
: base(status)
        {

        }

        public RejectCallForSpeechCommandResponse(ExecutionStatus status)
            : base(status)
        {



        }

        public RejectCallForSpeechCommandResponse(ExecutionStatus status, string message)
: base(status, message)
        {

        }

        public RejectCallForSpeechCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public RejectCallForSpeechCommandResponse(string message)
        : base(message)
        { }

        public RejectCallForSpeechCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
