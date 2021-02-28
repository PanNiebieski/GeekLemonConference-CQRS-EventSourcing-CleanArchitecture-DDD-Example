using GeekLemonConference.Application.CQRS.Mapper.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Dto
{
    public class SpeakerDto
    {
        public DateTime Birthdate { get; set; }
        public NameDto Name { get; set; }

        public AddressDto Address { get; set; }
        public SpeakerWebsitesDto SpeakerWebsites { get; set; }

        public string Biography { get; set; }
        public ContactDto Contact { get; set; }

    }
}
