using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Judges.CommandsEs.UpdateJudge
{
    public class EsUpdateJudgeCommandResponse : BaseResponse
    {
        public EsUpdateJudgeCommandResponse() : base()
        { }

        public EsUpdateJudgeCommandResponse(ExecutionStatus status)
    : base(status)
        {

        }

        public EsUpdateJudgeCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }

        protected EsUpdateJudgeCommandResponse(ResponseStatus status)
        {
            ValidationErrors = new List<string>();
            Success = status != ResponseStatus.Success;
            Status = status;
        }

        public EsUpdateJudgeCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public EsUpdateJudgeCommandResponse(string message)
        : base(message)
        { }

        public EsUpdateJudgeCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
