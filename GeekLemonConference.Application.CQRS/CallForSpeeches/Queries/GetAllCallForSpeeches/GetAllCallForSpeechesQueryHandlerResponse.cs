using FluentValidation.Results;
using GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetAllCallForSpeeches;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Queries.GetAllCallForSpeeches
{
    public class GetAllCallForSpeechesQueryHandlerResponse : BaseResponse
    {
        public List<CallForSpeechInListViewModel> List { get; }

        public GetAllCallForSpeechesQueryHandlerResponse(List<CallForSpeechInListViewModel> lisy)
    : base()
        {
            List = lisy;
        }

        public GetAllCallForSpeechesQueryHandlerResponse() : base()
        { }

        public GetAllCallForSpeechesQueryHandlerResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public GetAllCallForSpeechesQueryHandlerResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public GetAllCallForSpeechesQueryHandlerResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public GetAllCallForSpeechesQueryHandlerResponse(string message)
        : base(message)
        { }

        public GetAllCallForSpeechesQueryHandlerResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
