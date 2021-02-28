using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges
{
    public interface IJudgeGetByIdDoer : IBeforeDoer
    {
        Task<ExecutionStatus<Judge>> Run(JudgeId id);

        Task<ExecutionStatus<Judge>> Run(JudgeUniqueId id);
    }
}
