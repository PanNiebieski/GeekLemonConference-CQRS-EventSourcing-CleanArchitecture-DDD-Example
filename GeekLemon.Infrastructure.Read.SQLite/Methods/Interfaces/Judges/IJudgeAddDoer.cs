using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories
{
    //public interface IJudgeAddDoer : IAddDoer<Judge, JudgeId, JudgeUniqueId>
    //{

    //}

    public interface IJudgeAddDoer : IBeforeDoer
    {
        Task<ExecutionStatus<JudgeIds>> Run(Judge entity);
    }


}
