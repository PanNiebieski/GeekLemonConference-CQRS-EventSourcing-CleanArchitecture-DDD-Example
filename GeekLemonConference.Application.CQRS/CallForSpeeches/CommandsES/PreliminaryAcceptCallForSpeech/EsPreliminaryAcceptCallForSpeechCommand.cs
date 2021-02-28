using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.PreliminaryAcceptCallForSpeech
{
    public class EsPreliminaryAcceptCallForSpeechCommand
:
        IRequest<EsPreliminaryAcceptCallForSpeechCommandResponse>
    {
        public Guid CallForSpeechUniqueId { get; set; }
        public int JudgeId { get; set; }
        public int Version { get; set; }
    }
}
