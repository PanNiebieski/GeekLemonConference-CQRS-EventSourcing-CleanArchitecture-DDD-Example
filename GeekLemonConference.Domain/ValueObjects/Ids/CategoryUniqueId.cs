using GeekLemonConference.Domain.Ddd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Domain.ValueObjects.Ids
{
    public class CategoryUniqueId : BaseUniqueId<CategoryUniqueId>
    {
        public Guid Value { get; set; }


        public CategoryUniqueId(Guid value)
        {
            Value = value;
        }

        [JsonConstructor]
        public CategoryUniqueId()
        {
            Value = Guid.NewGuid();
        }

        public static CategoryUniqueId Empty()
        {
            return new CategoryUniqueId(Guid.Empty);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }

        public static CategoryUniqueId NewUniqueId()
        {
            return new CategoryUniqueId();
        }

        protected override string GetName()
        {
            return "Category";
        }

        public override string ValueInString() => Value.ToString();

    }
}
