using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Judges.CommandsEs.CreateJudge
{
    public class EsCreateJudgeCommandResponse : BaseResponse
    {

        public IdsDto JudgeIds { get; set; }

        public EsCreateJudgeCommandResponse() : base()
        { }

        public EsCreateJudgeCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public EsCreateJudgeCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public EsCreateJudgeCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public EsCreateJudgeCommandResponse(string message)
        : base(message)
        { }

        public EsCreateJudgeCommandResponse(string message, bool success)
            : base(message, success)
        { }

        public EsCreateJudgeCommandResponse(IdsDto judgeIds)
        {
            JudgeIds = judgeIds;
        }
    }
}
