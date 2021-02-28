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
    public class AcceptCallForSpeechComandValidator : BaseResponse
    {
        public AcceptCallForSpeechComandValidator() : base()
        { }

        public AcceptCallForSpeechComandValidator(ExecutionStatus status)
            : base(status)
        {

        }

        public AcceptCallForSpeechComandValidator(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public AcceptCallForSpeechComandValidator(ValidationResult validationResult)
            : base(validationResult)
        { }

        public AcceptCallForSpeechComandValidator(string message)
        : base(message)
        { }

        public AcceptCallForSpeechComandValidator(string message, bool success)
            : base(message, success)
        { }
    }
}
