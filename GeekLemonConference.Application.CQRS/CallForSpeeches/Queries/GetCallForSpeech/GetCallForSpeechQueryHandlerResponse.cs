using FluentValidation.Results;
using GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetCallForSpeech;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Queries.GetCallForSpeech
{
    public class GetCallForSpeechQueryHandlerResponse : BaseResponse
    {
        public CallForSpeechViewModel CallForSpeech { get; }

        public GetCallForSpeechQueryHandlerResponse(CallForSpeechViewModel callForSpeech)
            : base()
        {
            CallForSpeech = callForSpeech;
        }

        public GetCallForSpeechQueryHandlerResponse() : base()
        { }

        public GetCallForSpeechQueryHandlerResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public GetCallForSpeechQueryHandlerResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public GetCallForSpeechQueryHandlerResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public GetCallForSpeechQueryHandlerResponse(string message)
        : base(message)
        { }

        public GetCallForSpeechQueryHandlerResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
