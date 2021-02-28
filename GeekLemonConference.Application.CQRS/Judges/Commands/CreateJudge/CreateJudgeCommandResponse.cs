using FluentValidation.Results;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekLemonConference.Application.CQRS.Mapper.Dto;

namespace GeekLemonConference.Application.CQRS.Judges.Commands.CreateJudge
{
    public class CreateJudgeCommandResponse : BaseResponse
    {
        public IdsDto JudgeIds { get; set; }

        public CreateJudgeCommandResponse() : base()
        { }

        public CreateJudgeCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public CreateJudgeCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public CreateJudgeCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public CreateJudgeCommandResponse(string message)
        : base(message)
        { }

        public CreateJudgeCommandResponse(string message, bool success)
            : base(message, success)
        { }

        public CreateJudgeCommandResponse(IdsDto judgeIds)
        {
            JudgeIds = judgeIds;
        }
    }
}
