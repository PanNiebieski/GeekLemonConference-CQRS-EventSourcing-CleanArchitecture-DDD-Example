using GeekLemon.Persistence.Dapper.SQLite;
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
    public interface ICallForSpeechSaveEvaluatationDoer : IBeforeDoer
    {
        Task<ExecutionStatus> Run(CallForSpeechUniqueId id,
            CallForSpeechScoringResult score, CallForSpeechStatus status);

        Task<ExecutionStatus> Run(CallForSpeechId id,
            CallForSpeechScoringResult score, CallForSpeechStatus status);
    }
}
