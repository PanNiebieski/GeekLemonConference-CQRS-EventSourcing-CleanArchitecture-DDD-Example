using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.AcceptCallForSpeech;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.AcceptCallForSpeech
{
    public class EsAcceptCallForSpeechComand :
        IRequest<EsAcceptCallForSpeechComandResponse>
    {
        public Guid CallForSpeechIdUnique { get; set; }
        public int JudgeId { get; set; }
        public int Version { get; set; }

    }
}
