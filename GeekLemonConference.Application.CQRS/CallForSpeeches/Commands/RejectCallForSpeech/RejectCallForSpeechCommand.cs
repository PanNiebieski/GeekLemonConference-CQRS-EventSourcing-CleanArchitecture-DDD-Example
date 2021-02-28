using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.RejectCallForSpeech
{
    public class RejectCallForSpeechCommand :
        IRequest<RejectCallForSpeechCommandResponse>
    {
        //public CallForSpeechUniqueId CallForSpeechUniqueId { get; set; }
        //public JudgeId JudgeId { get; set; }

        public Guid CallForSpeechUniqueId { get; set; }
        public int JudgeId { get; set; }
    }
}
