using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.CallForSpeeches;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Repositories.CallForSpeeches
{
    public class CallForSpeechGetCollectionDoer : BeforeDoer, ICallForSpeechGetCollectionDoer
    {
        private readonly IMapper _mapper;

        public CallForSpeechGetCollectionDoer
            (IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus<IReadOnlyList<CallForSpeech>>> Run(FilterCallForSpeechStyles filtrer)
        {
            using var connection = new SqliteConnection
                           (_geekLemonContext.ConnectionString);

            IEnumerable<CallForSpeechTemp> r;

            var q = @$"SELECT
                c.UniqueId ,c.Version,
                c.Id,c.Number,c.Status,c.PreliminaryDecision_DecisionBy,
                c.PreliminaryDecision_Date,c.FinalDecision_DecisionBy,
                c.FinalDecision_Date,c.Speaker_Name_First,c.Speaker_Name_Last,
                c.Speaker_Adress_Country,c.Speaker_Adress_ZipCode, c.Speaker_Adress_City,
                c.Speaker_Adress_Street,c.Speaker_Websites_Facebook,c.Speaker_Websites_Twitter,
                c.Speaker_Websites_Instagram,c.Speaker_Websites_LinkedIn,c.Speaker_Websites_TikTok,
                c.Speaker_Websites_Youtube,c.Speaker_Websites_FanPageOnFacebook,c.Speaker_Websites_GitHub,
                c.Speaker_Websites_Blog, c.Speaker_BIO,
                c.Speaker_Contact_Phone,c.Speaker_Contact_Email, c.Speech_Tags,
                c.Speech_ForWhichAudience,c.Speech_TechnologyOrBussinessStory,c.Registration_RegistrationDate,
                c.CategoryId,c.Score_Score, c.Score_RejectExplanation,c.Score_WarringExplanation,
                c.Speaker_Birthdate,c.Speech_Title,c.Speech_Description,
                k.Name AS {nameof(JudgeTemp.Category_Name)},
                k.DisplayName AS {nameof(JudgeTemp.Category_DisplayName)}
                ,k.WhatWeAreLookingFor AS {nameof(JudgeTemp.Category_WhatWeAreLookingFor)}
                FROM CallForSpeakes as c
                INNER JOIN Categories as k ON c.CategoryId = k.Id";

            try
            {

                if (filtrer == FilterCallForSpeechStyles.All)
                {
                    r = await connection.QueryAsync<CallForSpeechTemp>
                      (q);
                }
                else
                {
                    r = await connection.QueryAsync<CallForSpeechTemp>
                        (q + " WHERE Status = @st;"
                        , new { st = (int)filtrer });
                }

                var rmaped = _mapper.Map<IReadOnlyList<CallForSpeech>>(r);

                return ExecutionStatus<IReadOnlyList<CallForSpeech>>.
                    DbIfDefaultThenError(rmaped.ToList().AsReadOnly());
            }
            catch (Exception ex)
            {
                return ExecutionStatus<IReadOnlyList<CallForSpeech>>.DbError(ex);
            }
        }
    }
}
