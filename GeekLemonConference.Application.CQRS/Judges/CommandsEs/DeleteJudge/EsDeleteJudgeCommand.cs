using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Judges.CommandsEs.DeleteJudge
{
    public class EsDeleteJudgeCommand : IRequest<EsDeleteJudgeCommandResponse>
    {
        public JudgeUniqueId UniqueId { get; set; }
    }
}
