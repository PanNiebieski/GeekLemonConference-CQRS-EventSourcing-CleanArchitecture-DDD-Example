using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.EventVersion.Judge.Commands.CreateJudge
{
    public class EsCreateJudgeCommand : IRequest<EsCreateJudgeCommandResponse>
    {
    }
}
