using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Commands.DeleteJudge
{
    public class DeleteJudgeCommandResponse : BaseResponse
    {
        public DeleteJudgeCommandResponse() : base()
        { }

        public DeleteJudgeCommandResponse(ExecutionStatus status)
    : base(status)
        {

        }

        public DeleteJudgeCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }

        protected DeleteJudgeCommandResponse(ResponseStatus status)
        {
            ValidationErrors = new List<string>();
            Success = status != ResponseStatus.Success;
            Status = status;
        }

        public DeleteJudgeCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public DeleteJudgeCommandResponse(string message)
        : base(message)
        { }

        public DeleteJudgeCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
