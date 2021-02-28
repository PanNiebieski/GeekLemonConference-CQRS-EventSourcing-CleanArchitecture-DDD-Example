using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Repositories.CallForSpeeches
{
    public class CallForSpeechSavePreliminaryAcceptenceDoer : BeforeDoer, ICallForSpeechSavePreliminaryAcceptenceDoer
    {
        private readonly IMapper _mapper;

        public CallForSpeechSavePreliminaryAcceptenceDoer
            (IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus> Run(CallForSpeechUniqueId uniqueId, JudgeId judge, CallForSpeechStatus status)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            var q = @"UPDATE CallForSpeakes
                SET FinalDecision_DecisionBy = @JudgeId,
                FinalDecision_Date = @Date,
                Status = @Status
                WHERE UniqueId = @UniqueId;";

            try
            {
                var result = await connection.ExecuteAsync(q,
                 new
                 {
                     @JudgeId = judge.Value,
                     @Date = AppTime.Now().ToLongDateString(),
                     @UniqueId = uniqueId.Value,
                     @Status = (int)status
                 });

                return ExecutionStatus.DbOk();
            }
            catch (Exception ex)
            {
                if (ExecutionFlow.Options.ThrowExceptions)
                    throw;

                return ExecutionStatus.DbError(ex);
            }
        }

        public async Task<ExecutionStatus> Run(CallForSpeechId id, JudgeId judge, CallForSpeechStatus status)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            var q = @"UPDATE CallForSpeakes
                SET FinalDecision_DecisionBy = @JudgeId,
                FinalDecision_Date = @Date,
                Status = @Status
                WHERE UniqueId = @Id;";

            try
            {
                var result = await connection.ExecuteAsync(q,
                 new
                 {
                     @JudgeId = judge.Value,
                     @Date = AppTime.Now().ToLongDateString(),
                     @Id = id.Value,
                     @Status = (int)status
                 });

                return ExecutionStatus.DbOk();
            }
            catch (Exception ex)
            {
                if (ExecutionFlow.Options.ThrowExceptions)
                    throw;

                return ExecutionStatus.DbError(ex);
            }
        }
    }
}
