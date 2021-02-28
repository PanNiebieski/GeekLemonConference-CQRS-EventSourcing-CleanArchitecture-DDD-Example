using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.SubmitCallForSpeech
{
    public class EsSubmitCallForSpeechCommand : IRequest<EsSubmitCallForSpeechCommandResponse>
    {
        public int CategoryId { get; set; }

        public CategoryDto Category
        {
            get
            {
                return new CategoryDto() { Id = CategoryId };
            }
        }

        public int Version { get; }

        public CallForSpeechUniqueId UniqueId { get; }

        public EsSubmitCallForSpeechCommand()
        {
            UniqueId = CallForSpeechUniqueId.NewUniqueId();
            Version = 0;
        }

        public SpeakerDto Speaker { get; set; }

        public SpeechDto Speech { get; set; }
        public string Number { get; set; }
        public RegistrationDto Registration
        {
            get
            {
                return new RegistrationDto()
                {
                    RegistrationDate = AppTime.Now()
                };
            }
        }
    }
}



