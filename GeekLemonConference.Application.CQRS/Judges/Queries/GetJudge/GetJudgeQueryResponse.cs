using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge
{
    public class GetJudgeQueryResponse : BaseResponse
    {
        public JudgeViewModel Judge { get; }

        public GetJudgeQueryResponse(JudgeViewModel judge)
            : base()
        {
            Judge = judge;
        }

        public GetJudgeQueryResponse() : base()
        { }

        public GetJudgeQueryResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public GetJudgeQueryResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public GetJudgeQueryResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public GetJudgeQueryResponse(string message)
        : base(message)
        { }

        public GetJudgeQueryResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
