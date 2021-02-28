using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Commands.DeleteJudge
{
    public class DeleteJudgeCommand : IRequest<DeleteJudgeCommandResponse>
    {
        public JudgeUniqueId UniqueId { get; set; }

        public JudgeId Id { get; set; }
    }
}
