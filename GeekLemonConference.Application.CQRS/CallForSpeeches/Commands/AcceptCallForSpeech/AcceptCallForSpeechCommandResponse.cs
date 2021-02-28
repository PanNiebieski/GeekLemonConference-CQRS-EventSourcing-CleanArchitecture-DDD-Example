using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.AcceptCallForSpeech
{
    public class AcceptCallForSpeechCommandResponse : BaseResponse
    {
        public AcceptCallForSpeechCommandResponse()
            : base()
        {

        }

        public AcceptCallForSpeechCommandResponse(ResponseStatus status)
: base(status)
        {

        }


        public AcceptCallForSpeechCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public AcceptCallForSpeechCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }

        public AcceptCallForSpeechCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public AcceptCallForSpeechCommandResponse(string message)
        : base(message)
        { }

        public AcceptCallForSpeechCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
