using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Repositories.CallForSpeeches
{
    public interface ICallForSpeechSubmitDoer : IBeforeDoer
    {
        Task<ExecutionStatus<CallForSpeechIds>>
            Run(CallForSpeech callForSpeech);
    }
}
