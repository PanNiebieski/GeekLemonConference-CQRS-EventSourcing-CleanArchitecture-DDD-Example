using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.EventSourcing
{
    public class AggregateKey : ValueObject<AggregateKey>
    {
        public string Type { get; set; }
        public string Id { get; set; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Type;
            yield return Id;
        }

        public static readonly AggregateKey Empty = new AggregateKey();

        public override string ToString()
        {
            return Id;
        }
    }
}

