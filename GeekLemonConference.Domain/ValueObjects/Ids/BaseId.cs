using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Domain.Ddd;

namespace GeekLemonConference.Domain.ValueObjects.Ids
{
    public abstract class BaseId<T> : ValueObject<T> where T : ValueObject<T>
    {
        public BaseId()
        {

        }
    }
}
