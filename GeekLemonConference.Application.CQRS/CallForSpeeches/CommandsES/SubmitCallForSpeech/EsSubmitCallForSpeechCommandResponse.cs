using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.SubmitCallForSpeech
{
    public class EsSubmitCallForSpeechCommandResponse
        : BaseResponse
    {
        public IdsDto CallForSpeechIds { get; set; }

        public EsSubmitCallForSpeechCommandResponse(IdsDto Ids)
             : base()
        {
            CallForSpeechIds = Ids;
        }
        public EsSubmitCallForSpeechCommandResponse() : base()
        { }

        public EsSubmitCallForSpeechCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public EsSubmitCallForSpeechCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public EsSubmitCallForSpeechCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public EsSubmitCallForSpeechCommandResponse(string message)
        : base(message)
        { }

        public EsSubmitCallForSpeechCommandResponse(string message, bool success)
            : base(message, success)
        { }


    }
}
