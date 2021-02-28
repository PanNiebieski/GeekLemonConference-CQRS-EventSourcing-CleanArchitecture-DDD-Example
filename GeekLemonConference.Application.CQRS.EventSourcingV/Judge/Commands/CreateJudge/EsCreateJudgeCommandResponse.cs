using FluentValidation.Results;
using GeekLemonConference.Application.CQRS.Responses;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.EventVersion.Judge.Commands.CreateJudge
{
    public class EsCreateJudgeCommandResponse : BaseResponse
    {
        public JudgeIds JudgeIds { get; set; }

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

        public EsCreateJudgeCommandResponse(JudgeIds judgeIds)
        {
            JudgeIds = judgeIds;
            JudgeIds.Status = IdsStatus.DudeYouCantReturnCreatedIdWhenYouAreEventSourcing;
        }
    }
}
