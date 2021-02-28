using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Application.CQRS.Queries.Judges.GetAllJudges;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge
{
    public class GetJudgesInListQueryResponse : BaseResponse
    {
        public List<JudgesInListViewModel> List { get; }

        public GetJudgesInListQueryResponse(List<JudgesInListViewModel> list)
            : base()
        {
            List = list;
        }

        public GetJudgesInListQueryResponse() : base()
        { }

        public GetJudgesInListQueryResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public GetJudgesInListQueryResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public GetJudgesInListQueryResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public GetJudgesInListQueryResponse(string message)
        : base(message)
        { }

        public GetJudgesInListQueryResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
