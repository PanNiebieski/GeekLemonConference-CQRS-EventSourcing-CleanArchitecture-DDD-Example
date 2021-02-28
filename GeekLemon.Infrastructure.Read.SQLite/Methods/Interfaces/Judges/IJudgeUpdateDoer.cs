using GeekLemonConference.Application.Contracts.Persistence;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Judges
{
    public interface IJudgeUpdateDoer : IBeforeDoer
    {
        Task<ExecutionStatus> Run(Judge entity, ByWhatId byWhatId);
    }
}
