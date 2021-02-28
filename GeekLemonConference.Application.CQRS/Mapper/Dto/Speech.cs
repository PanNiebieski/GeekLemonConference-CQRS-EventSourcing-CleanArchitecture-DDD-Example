using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Dto
{
    public class SpeechDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string[] Tags { get; set; }

        public string ForWhichAudience { get; set; }

        public string TechnologyOrBussinessStory { get; set; }
    }
}
