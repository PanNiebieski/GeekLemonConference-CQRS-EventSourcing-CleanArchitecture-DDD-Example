using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.PreliminaryAcceptCallForSpeech
{
    public class EsPreliminaryAcceptCallForSpeechCommandResponse
: BaseResponse
    {
        public EsPreliminaryAcceptCallForSpeechCommandResponse()
            : base()
        {

        }

        public EsPreliminaryAcceptCallForSpeechCommandResponse(ResponseStatus status)
    : base(status)
        {

        }

        public EsPreliminaryAcceptCallForSpeechCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public EsPreliminaryAcceptCallForSpeechCommandResponse(ExecutionStatus status, string message)
: base(status, message)
        {

        }

        public EsPreliminaryAcceptCallForSpeechCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public EsPreliminaryAcceptCallForSpeechCommandResponse(string message)
        : base(message)
        { }

        public EsPreliminaryAcceptCallForSpeechCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
