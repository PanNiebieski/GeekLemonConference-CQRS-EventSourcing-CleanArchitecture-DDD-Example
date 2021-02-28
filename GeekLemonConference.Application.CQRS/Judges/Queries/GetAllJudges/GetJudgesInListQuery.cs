using GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.Judges.GetAllJudges
{
    public class GetJudgesInListQuery
         : IRequest<GetJudgesInListQueryResponse>
    {
        public QueryWitchDataBase queryWitchDataBase { get; set; }
    }
}
