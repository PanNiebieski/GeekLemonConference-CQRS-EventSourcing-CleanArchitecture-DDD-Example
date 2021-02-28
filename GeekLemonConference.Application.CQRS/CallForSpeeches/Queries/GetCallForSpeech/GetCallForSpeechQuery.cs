using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Queries.GetCallForSpeech;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetCallForSpeech
{
    public class GetCallForSpeechQuery : IRequest<GetCallForSpeechQueryHandlerResponse>
    {
        public CallForSpeechId CallForSpeechId { get; set; }
        public CallForSpeechUniqueId CallForSpeechUniqueId { get; set; }

        public QueryWitchDataBase queryWitchDataBase { get; set; }
    }
}
