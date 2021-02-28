using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Commands.UpdateJudge
{
    public class UpdateJudgeCommandResponse : BaseResponse
    {

        public UpdateJudgeCommandResponse() : base()
        { }

        public UpdateJudgeCommandResponse(ExecutionStatus status)
    : base(status)
        {

        }

        public UpdateJudgeCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }

        protected UpdateJudgeCommandResponse(ResponseStatus status)
        {
            ValidationErrors = new List<string>();
            Success = status != ResponseStatus.Success;
            Status = status;
        }

        public UpdateJudgeCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public UpdateJudgeCommandResponse(string message)
        : base(message)
        { }

        public UpdateJudgeCommandResponse(string message, bool success)
            : base(message, success)
        { }


    }
}
