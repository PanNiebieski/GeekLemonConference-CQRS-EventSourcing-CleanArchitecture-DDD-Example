using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.EvaluateCallForSpeech
{
    public class EvaluateCallForSpeechCommand
        : IRequest<EvaluateCallForSpeechCommandResponse>
    {


        public Guid CallForSpeechUniqueId { get; set; }
    }
}
