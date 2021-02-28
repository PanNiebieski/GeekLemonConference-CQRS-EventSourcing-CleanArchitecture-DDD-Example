using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges
{
    public interface IJudgeDeleteDoer : IBeforeDoer
    {
        Task<ExecutionStatus> Run(JudgeId entity);

        Task<ExecutionStatus> Run(JudgeUniqueId id);
    }
}
