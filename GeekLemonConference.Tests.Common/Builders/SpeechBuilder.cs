using GeekLemonConference.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Tests.Builders
{
    public class SpeechBuilder
    {
        private ForWhichAudience forWhichAudience = ForWhichAudience.Beginers;
        private TechnologyOrBussinessStory technologyOrBussinessStory
            = TechnologyOrBussinessStory.InTheMiddle;

        private string[] tags = new string[] { "Kuberetes", "Docker", ".NET" };
        private string title = "Mowa tytuł";
        private string description = "Opis Opis";

        public static SpeechBuilder GivenSpeech() => new SpeechBuilder();

        public SpeechBuilder WithTitle(string newtitle)
        {
            title = newtitle;
            return this;
        }

        public SpeechBuilder WithDescription(string newdescritpion)
        {
            description = newdescritpion;
            return this;
        }

        public SpeechBuilder WithTags(string[] newtags)
        {
            tags = newtags;
            return this;
        }

        public SpeechBuilder WithForWhichAudience(ForWhichAudience newforWhichAudience)
        {
            forWhichAudience = newforWhichAudience;
            return this;
        }
        public SpeechBuilder WithTechnologyOrBussinessStory(TechnologyOrBussinessStory newtechnologyOrBussinessStory)
        {
            technologyOrBussinessStory = newtechnologyOrBussinessStory;
            return this;
        }

        public Speech Build()
        {
            return new Speech
            (
                title,
                description,
                tags,
                forWhichAudience,
                technologyOrBussinessStory
            );
        }
    }
}
