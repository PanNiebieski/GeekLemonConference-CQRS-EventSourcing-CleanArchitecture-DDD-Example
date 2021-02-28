using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Judges.CommandsEs.DeleteJudge
{
    public class EsDeleteJudgeCommandResponse :  BaseResponse
    {
        public EsDeleteJudgeCommandResponse() : base()
        { }

        public EsDeleteJudgeCommandResponse(ExecutionStatus status)
    : base(status)
        {

        }

        public EsDeleteJudgeCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }

        protected EsDeleteJudgeCommandResponse(ResponseStatus status)
        {
            ValidationErrors = new List<string>();
            Success = status != ResponseStatus.Success;
            Status = status;
        }

        public EsDeleteJudgeCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public EsDeleteJudgeCommandResponse(string message)
        : base(message)
        { }

        public EsDeleteJudgeCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
