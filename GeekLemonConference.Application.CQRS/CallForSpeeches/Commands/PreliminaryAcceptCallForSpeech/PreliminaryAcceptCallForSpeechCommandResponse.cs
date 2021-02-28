using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.PreliminaryAcceptCallForSpeech
{
    public class PreliminaryAcceptCallForSpeechCommandResponse : BaseResponse
    {
        public PreliminaryAcceptCallForSpeechCommandResponse()
            : base()
        {

        }

        public PreliminaryAcceptCallForSpeechCommandResponse(ResponseStatus status)
    : base(status)
        {

        }

        public PreliminaryAcceptCallForSpeechCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public PreliminaryAcceptCallForSpeechCommandResponse(ExecutionStatus status, string message)
: base(status, message)
        {

        }

        public PreliminaryAcceptCallForSpeechCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public PreliminaryAcceptCallForSpeechCommandResponse(string message)
        : base(message)
        { }

        public PreliminaryAcceptCallForSpeechCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
