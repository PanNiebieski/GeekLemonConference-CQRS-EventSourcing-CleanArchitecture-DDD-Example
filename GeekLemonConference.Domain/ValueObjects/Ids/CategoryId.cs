using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects.Ids
{
    public class CategoryId : BaseId<CategoryId>
    {
        public int Value { get; set; }

        public CategoryId(int value)
        {
            Value = value;
        }

        public CategoryId()
        {

        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }


        public static CategoryId Empty()
        {
            return new CategoryId(0);
        }


    }
}
