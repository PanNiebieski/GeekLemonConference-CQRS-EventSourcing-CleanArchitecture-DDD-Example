using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.EvaluateCallForSpeech
{
    public class EsEvaluateCallForSpeechCommand
        : IRequest<EsEvaluateCallForSpeechCommandResponse>
    {
        public Guid CallForSpeechIdUnique { get; set; }
        public int Version { get; set; }
    }

}
