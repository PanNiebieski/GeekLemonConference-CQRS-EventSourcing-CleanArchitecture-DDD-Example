//using AutoMapper;
//using Dapper;
//using GeekLemon.Persistence.Dapper.SQLite;
//using GeekLemon.Persistence.Dapper.SQLite.Repositories;
//using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
//using GeekLemonConference.Application.Contracts.Repository;
//using GeekLemonConference.Domain;
//using GeekLemonConference.Domain.Entities;
//using GeekLemonConference.Domain.Entity;
//using GeekLemonConference.Domain.ValueObjects;
//using GeekLemonConference.Domain.ValueObjects.Ids;
//using GeekLemonConference.Persistence.Dapper.SQLite.Repositories;
//using Microsoft.Data.Sqlite;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GeekLemon.Infrastructure.Read.SQLite
//{
//    public class CallForSpeechRepository : ICallForSpeechRepository
//    {
//        private IGeekLemonDBContext _geekLemonContext;
//        private readonly IMapper _mapper;

//        public CallForSpeechRepository(IGeekLemonDBContext geekLemonContext,
//            IMapper mapper)
//        {
//            _geekLemonContext = geekLemonContext;
//            _mapper = mapper;
//        }

//        public async Task<ExecutionStatus> SaveAcceptenceAsyncById(CallForSpeechId Id, JudgeId judge,
//            CallForSpeechStatus status)
//        {
//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            var q = @"UPDATE CallForSpeakes
//                SET FinalDecision_DecisionBy = @JudgeId,
//                FinalDecision_Date = @Date,
//                Status = @Status
//                WHERE Id = @Id;";

//            try
//            {
//                var result = await connection.ExecuteAsync(q,
//                 new
//                 {
//                     @JudgeId = judge.Value,
//                     @Date = AppTime.Now().ToLongDateString(),
//                     @Id = Id.Value,
//                     @Status = (int)status
//                 });

//                return ExecutionStatus.DbOk();
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus.DbError(ex);
//            }


//        }

//        public async Task<ExecutionStatus> SaveEvaluatationByIdAsync(CallForSpeechId Id,
//            CallForSpeechScoringResult score, CallForSpeechStatus status)
//        {
//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            var q = @"UPDATE CallForSpeakes
//                SET Score_Score = @Score, 
//                Score_RejectExplanation = @RejectExplanation,
//                Score_WarringExplanation = @WarringExplanation,
//                Status = @Status
//                WHERE Id = @Id;";

//            try
//            {
//                var result = await connection.ExecuteAsync(q,
//                 new
//                 {
//                     @Score = (int)score.Score,
//                     @WarringExplanation = score.WarringExplanation,
//                     @RejectExplanation = score.RejectExplanation,
//                     @Id = Id.Value,
//                     @Status = (int)status
//                 });
//                return ExecutionStatus.DbOk();
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus.DbError(ex);
//            }

//        }

//        public async Task<ExecutionStatus> SaveRejectionByIdAsync(CallForSpeechId Id, JudgeId judge,
//            CallForSpeechStatus status)
//        {
//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            var q = @"UPDATE CallForSpeakes
//                SET PreliminaryDecision_DecisionBy = @JudgeId, 
//                PreliminaryDecision_Date = @Date,
//                Status = @Status
//                WHERE Id = @Id;";
//            try
//            {

//                return ExecutionStatus.DbOk();
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus.DbError(ex);
//            }
//            var result = await connection.ExecuteAsync(q,
//                 new
//                 {
//                     @JudgeId = judge.Value,
//                     @Date = AppTime.Now().ToLongDateString(),
//                     @Id = Id.Value,
//                     @Status = (int)status
//                 });
//        }

//        public async Task<ExecutionStatus<int>> SubmitByIdAsync(CallForSpeech entity)
//        {
//            var temp = _mapper.Map<CallForSpeechTemp>(entity);

//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            var q = SqlQueries.CallForSpeechInsert;

//            try
//            {
//                var result = await connection.QueryAsync<int>(q, temp);

//                int id = result.FirstOrDefault();

//                return ExecutionStatus<int>.DbOk(id);
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus<int>.DbError(ex);
//            }


//            //var result = await connection.QueryAsync<int>(q,
//            //     new
//            //     {
//            //         @Number = entity.Number.Number,
//            //         @Name = entity.Name,
//            //         @DisplayName = entity.DisplayName,
//            //         @WhatWeAreLookingFor = entity.WhatWeAreLookingFor
//            //     });
//        }

//        public async Task<ExecutionStatus<IReadOnlyList<CallForSpeech>>> GetCollectionAsync
//            (FilterCallForSpeechStyles filtrer)
//        {
//            using var connection = new SqliteConnection
//                (_geekLemonContext.ConnectionString);

//            IEnumerable<CallForSpeechTemp> r;


//            var q = @$"SELECT
//                c.UniqueId ,c.Version,
//                c.Id,c.Number,c.Status,c.PreliminaryDecision_DecisionBy,
//                c.PreliminaryDecision_Date,c.FinalDecision_DecisionBy,
//                c.FinalDecision_Date,c.Speaker_Name_First,c.Speaker_Name_Last,
//                c.Speaker_Adress_Country,c.Speaker_Adress_ZipCode, c.Speaker_Adress_City,
//                c.Speaker_Adress_Street,c.Speaker_Websites_Facebook,c.Speaker_Websites_Twitter,
//                c.Speaker_Websites_Instagram,c.Speaker_Websites_LinkedIn,c.Speaker_Websites_TikTok,
//                c.Speaker_Websites_Youtube,c.Speaker_Websites_FanPageOnFacebook,c.Speaker_Websites_GitHub,
//                c.Speaker_Websites_Blog, c.Speaker_BIO,
//                c.Speaker_Contact_Phone,c.Speaker_Contact_Email, c.Speech_Tags,
//                c.Speech_ForWhichAudience,c.Speech_TechnologyOrBussinessStory,c.Registration_RegistrationDate,
//                c.CategoryId,c.Score_Score, c.Score_RejectExplanation,c.Score_WarringExplanation,
//                c.Speaker_Birthdate,c.Speech_Title,c.Speech_Description,
//                k.Name AS {nameof(JudgeTemp.Category_Name)},
//                k.DisplayName AS {nameof(JudgeTemp.Category_DisplayName)}
//                ,k.WhatWeAreLookingFor AS {nameof(JudgeTemp.Category_WhatWeAreLookingFor)}
//                FROM CallForSpeakes as c
//                INNER JOIN Categories as k ON c.CategoryId = k.Id";

//            try
//            {

//                if (filtrer == FilterCallForSpeechStyles.All)
//                {
//                    r = await connection.QueryAsync<CallForSpeechTemp>
//                      (q);
//                }
//                else
//                {
//                    r = await connection.QueryAsync<CallForSpeechTemp>
//                        (q + " WHERE Status = @st;"
//                        , new { st = (int)filtrer });
//                }

//                var rmaped = _mapper.Map<IReadOnlyList<CallForSpeech>>(r);

//                return ExecutionStatus<IReadOnlyList<CallForSpeech>>.DbOk(rmaped.ToList().AsReadOnly());
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus<IReadOnlyList<CallForSpeech>>.DbError(ex);
//            }


//        }

//        public async Task<ExecutionStatus<CallForSpeech>> GetByIdAsync(int id)
//        {
//            using var connection = new SqliteConnection
//    (_geekLemonContext.ConnectionString);

//            var q =
//                @$"SELECT   c.UniqueId ,c.Version, c.Id,c.Number,c.Status,c.PreliminaryDecision_DecisionBy,
//                c.PreliminaryDecision_Date,c.FinalDecision_DecisionBy,
//                c.FinalDecision_Date,c.Speaker_Name_First,c.Speaker_Name_Last,
//                c.Speaker_Adress_Country,c.Speaker_Adress_ZipCode, c.Speaker_Adress_City,
//                c.Speaker_Adress_Street,c.Speaker_Websites_Facebook,c.Speaker_Websites_Twitter,
//                c.Speaker_Websites_Instagram,c.Speaker_Websites_LinkedIn,c.Speaker_Websites_TikTok,
//                c.Speaker_Websites_Youtube,c.Speaker_Websites_FanPageOnFacebook,c.Speaker_Websites_GitHub,
//                c.Speaker_Websites_Blog, c.Speaker_BIO,
//                c.Speaker_Contact_Phone,c.Speaker_Contact_Email, c.Speech_Tags,
//                c.Speech_ForWhichAudience,c.Speech_TechnologyOrBussinessStory,c.Registration_RegistrationDate,
//                c.CategoryId,c.Score_Score, c.Score_RejectExplanation,c.Score_WarringExplanation,
//                c.Speaker_Birthdate,c.Speech_Title,c.Speech_Description,
//                k.Name AS {nameof(JudgeTemp.Category_Name)},
//                k.DisplayName AS {nameof(JudgeTemp.Category_DisplayName)}
//                ,k.WhatWeAreLookingFor AS {nameof(JudgeTemp.Category_WhatWeAreLookingFor)}
//                FROM CallForSpeakes as c
//                INNER JOIN Categories as k ON c.CategoryId = k.Id
//                Where c.Id = @Id";

//            try
//            {
//                var r = await connection.
//                QueryFirstOrDefaultAsync<CallForSpeechTemp>
//                (q, new
//                {
//                    @Id = id,
//                });


//                var rmaped = _mapper.Map<CallForSpeech>(r);

//                return ExecutionStatus<CallForSpeech>.DbOk(rmaped);
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus<CallForSpeech>.DbError(ex);
//            }


//        }

//        public async Task<ExecutionStatus> SavePreliminaryAcceptenceByIdAsync(CallForSpeechId Id, JudgeId judge, CallForSpeechStatus status)
//        {
//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            var q = @"UPDATE CallForSpeakes
//                SET PreliminaryDecision_DecisionBy = @JudgeId,
//                PreliminaryDecision_Date = @Date,
//                Status = @Status
//                WHERE Id = @Id;";

//            try
//            {
//                var result = await connection.ExecuteAsync(q,
//                 new
//                 {
//                     @JudgeId = judge.Value,
//                     @Date = AppTime.Now().ToLongDateString(),
//                     @Id = Id.Value,
//                     @Status = (int)status
//                 });

//                return ExecutionStatus.DbOk();
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus.DbError(ex);
//            }


//        }

//        public Task<ExecutionStatus> SavePreliminaryAcceptenceAsync(CallForSpeechUniqueId id, JudgeId judge, CallForSpeechStatus status)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ExecutionStatus> SaveAcceptenceAsync(CallForSpeechUniqueId id, JudgeId judge, CallForSpeechStatus status)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ExecutionStatus> SaveRejectionAsync(CallForSpeechUniqueId id, JudgeId judge, CallForSpeechStatus status)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ExecutionStatus> SaveEvaluatationAsync(CallForSpeechUniqueId id, CallForSpeechScoringResult score, CallForSpeechStatus status)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ExecutionStatus> SaveAcceptenceByIdAsync(CallForSpeechId id, JudgeId judge, CallForSpeechStatus status)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ExecutionStatus<CallForSpeech>> GetByUniqueIdAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ExecutionStatus<Ids>> SubmitAsync(CallForSpeech callForSpeech)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
