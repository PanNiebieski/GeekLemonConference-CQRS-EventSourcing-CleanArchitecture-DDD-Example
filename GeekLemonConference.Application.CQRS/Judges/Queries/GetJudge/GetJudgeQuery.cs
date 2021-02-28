using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge
{
    public class GetJudgeQuery : IRequest<GetJudgeQueryResponse>
    {
        public JudgeId JudeId { get; set; }

        public JudgeUniqueId JudgeUniqueId { get; set; }

        public QueryWitchDataBase queryWitchDataBase { get; set; }
    }
}
