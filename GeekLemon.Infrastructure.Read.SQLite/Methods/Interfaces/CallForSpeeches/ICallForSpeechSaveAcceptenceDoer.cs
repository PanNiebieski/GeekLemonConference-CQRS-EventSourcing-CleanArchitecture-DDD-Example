using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Repositories.CallForSpeeches
{
    public interface ICallForSpeechSaveAcceptenceDoer : IBeforeDoer
    {
        Task<ExecutionStatus> Run(CallForSpeechUniqueId id,
            JudgeId judge, CallForSpeechStatus status);

        Task<ExecutionStatus> Run(CallForSpeechId id,
            JudgeId judge, CallForSpeechStatus status);
    }
}
