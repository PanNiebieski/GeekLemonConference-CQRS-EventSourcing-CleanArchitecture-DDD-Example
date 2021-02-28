using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class Speech : ValueObject<Speech>
    {
        public string Title { get; init; }

        public string Description { get; init; }

        public string[] Tags { get; init; }

        public ForWhichAudience ForWhichAudience { get; init; }

        public TechnologyOrBussinessStory TechnologyOrBussinessStory { get; init; }

        protected Speech()
        {

        }

        public Speech(string title, string description, string[] tags,
            ForWhichAudience forWhichAudience, TechnologyOrBussinessStory technologyOrBussinessStory)
        {
            Title = title;
            Description = description;
            Tags = tags;
            ForWhichAudience = forWhichAudience;
            TechnologyOrBussinessStory = technologyOrBussinessStory;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>
            {
                Title,
                Description,
                Tags
            };

        }
    }

    public enum ForWhichAudience
    {
        Beginers = 0,
        Intermediate = 1,
        Experts = 2,
        GrandMasters = 3
    }

    public enum TechnologyOrBussinessStory
    {
        OnlyAboutTechnologyAndTricks = 0,
        MoreAboutTechnologyAndTricksAndLessMyPersonalBussinessStory = 1,
        InTheMiddle = 2,
        LessAboutTechnologyAndTricksAndMoreMyPersonalBussinessStory = 3,
        OnlyMyPersonalBussinessStory = 4
    }

}
