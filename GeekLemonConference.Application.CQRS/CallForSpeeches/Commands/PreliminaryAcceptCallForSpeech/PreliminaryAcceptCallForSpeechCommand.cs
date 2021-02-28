using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.PreliminaryAcceptCallForSpeech
{
    public class PreliminaryAcceptCallForSpeechCommand
        :
        IRequest<PreliminaryAcceptCallForSpeechCommandResponse>
    {
        //public CallForSpeechUniqueId CallForSpeechIdUnique { get; set; }
        //public JudgeId JudgeId { get; set; }

        public Guid CallForSpeechUniqueId { get; set; }
        public int JudgeId { get; set; }
    }
}
