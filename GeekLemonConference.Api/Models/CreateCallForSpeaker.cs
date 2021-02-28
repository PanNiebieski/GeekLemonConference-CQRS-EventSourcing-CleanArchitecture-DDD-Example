using GeekLemonConference.Application.CQRS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemonConference.Api.Models
{
    public class CreateCallForSpeaker
    {
        public int CategoryId { get; set; }


        public CreateCallForSpeaker()
        {

        }

        public SpeakerDto Speaker { get; set; }
        public SpeechDto Speech { get; set; }
        public string Number { get; set; }
        public RegistrationDto Registration { get; set; }
    }
}
