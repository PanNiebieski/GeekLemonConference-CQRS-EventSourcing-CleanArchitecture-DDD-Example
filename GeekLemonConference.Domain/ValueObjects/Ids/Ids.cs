using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Domain.ValueObjects.Ids
{
    public class Ids<T1, UniqueT2> where T1 : ValueObject<T1>
        where UniqueT2 : ValueObject<UniqueT2>
    {

        public BaseId<T1> CreatedId { get; set; }
        public BaseUniqueId<UniqueT2> UniqueId { get; set; }

        public IdsStatus Status { get; set; }
    }

    public enum IdsStatus
    {
        CreateIdReturned = 0,
        DudeYouCantReturnCreatedIdWhenYouAreEventSourcing = 1
    }

    public class CallForSpeechIds : Ids<CallForSpeechId, CallForSpeechUniqueId>
    {

    }

    public class JudgeIds : Ids<JudgeId, JudgeUniqueId>
    {

    }

    public class CategoryIds : Ids<CategoryId, CategoryUniqueId>
    {

    }
}
