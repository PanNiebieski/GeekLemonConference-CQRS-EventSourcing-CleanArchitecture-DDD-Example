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

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.SubmitCallForSpeech
{
    public class SubmitCallForSpeechCommand : IRequest<SubmitCallForSpeechCommandResponse>
    {
        public int CategoryId { get; set; }

        public CategoryDto Category
        {
            get
            {
                return new CategoryDto() { Id = CategoryId };
            }
        }

        internal int Version { get; private set; }
        internal CallForSpeechUniqueId UniqueId { get; set; }

        public SubmitCallForSpeechCommand()
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
