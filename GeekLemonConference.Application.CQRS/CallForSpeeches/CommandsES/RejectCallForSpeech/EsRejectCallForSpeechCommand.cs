using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.RejectCallForSpeech
{
    public class EsRejectCallForSpeechCommand
        :
        IRequest<EsRejectCallForSpeechCommandResponse>
    {
        public Guid CallForSpeechUniqueId { get; set; }
        public int JudgeId { get; set; }
        public int Version { get; set; }
    }
}
