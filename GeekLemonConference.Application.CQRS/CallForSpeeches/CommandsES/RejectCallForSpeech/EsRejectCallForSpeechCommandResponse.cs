using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.RejectCallForSpeech
{
    public class EsRejectCallForSpeechCommandResponse : BaseResponse
    {
        public EsRejectCallForSpeechCommandResponse()
            : base()
        {

        }

        public EsRejectCallForSpeechCommandResponse(ResponseStatus status)
: base(status)
        {

        }

        public EsRejectCallForSpeechCommandResponse(ExecutionStatus status)
            : base(status)
        {



        }

        public EsRejectCallForSpeechCommandResponse(ExecutionStatus status, string message)
: base(status, message)
        {

        }

        public EsRejectCallForSpeechCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public EsRejectCallForSpeechCommandResponse(string message)
        : base(message)
        { }

        public EsRejectCallForSpeechCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
