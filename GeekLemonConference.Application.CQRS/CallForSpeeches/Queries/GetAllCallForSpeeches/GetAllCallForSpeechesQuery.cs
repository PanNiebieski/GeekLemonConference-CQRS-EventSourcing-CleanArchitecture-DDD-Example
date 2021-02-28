using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Queries.GetAllCallForSpeeches;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetAllCallForSpeeches
{
    public class GetAllCallForSpeechesQuery : IRequest<GetAllCallForSpeechesQueryHandlerResponse>
    {
        public FilterCallForSpeechStyles Filter { get; set; }

        public QueryWitchDataBase queryWitchDataBase { get; set; }
    }
}
