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
    public class CallForSpeechSaveEvaluatationDoer : BeforeDoer, ICallForSpeechSaveEvaluatationDoer
    {
        private readonly IMapper _mapper;

        public CallForSpeechSaveEvaluatationDoer
            (IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus> Run(CallForSpeechUniqueId id,
            CallForSpeechScoringResult score, CallForSpeechStatus status)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            var q = @"UPDATE CallForSpeakes
                SET Score_Score = @Score, 
                Score_RejectExplanation = @RejectExplanation,
                Score_WarringExplanation = @WarringExplanation,
                Status = @Status
                WHERE UniqueId = @UniqueId;";

            try
            {
                var result = await connection.ExecuteAsync(q,
                 new
                 {
                     @Score = (int)score.Score,
                     @WarringExplanation = score.WarringExplanation,
                     @RejectExplanation = score.RejectExplanation,
                     @UniqueId = id.Value.ToString(),
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

        public async Task<ExecutionStatus> Run(CallForSpeechId id,
            CallForSpeechScoringResult score, CallForSpeechStatus status)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            var q = @"UPDATE CallForSpeakes
                SET Score_Score = @Score, 
                Score_RejectExplanation = @RejectExplanation,
                Score_WarringExplanation = @WarringExplanation,
                Status = @Status
                WHERE Id = @Id;";

            try
            {
                var result = await connection.ExecuteAsync(q,
                 new
                 {
                     @Score = (int)score.Score,
                     @WarringExplanation = score.WarringExplanation,
                     @RejectExplanation = score.RejectExplanation,
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
