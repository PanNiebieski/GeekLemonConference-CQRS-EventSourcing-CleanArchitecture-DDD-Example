using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.AcceptCallForSpeech
{
    public class EsAcceptCallForSpeechComandResponse : BaseResponse
    {
        public EsAcceptCallForSpeechComandResponse()
            : base()
        {

        }

        public EsAcceptCallForSpeechComandResponse(ResponseStatus status)
: base(status)
        {

        }


        public EsAcceptCallForSpeechComandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public EsAcceptCallForSpeechComandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }

        public EsAcceptCallForSpeechComandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public EsAcceptCallForSpeechComandResponse(string message)
        : base(message)
        { }

        public EsAcceptCallForSpeechComandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
