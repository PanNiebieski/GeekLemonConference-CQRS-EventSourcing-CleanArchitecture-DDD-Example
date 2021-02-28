using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Repositories;
using GeekLemonConference.Persistence.Dapper.SQLite.Repositories.CallForSpeeches;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.CallForSpeeches
{
    public class CallForSpeechSubmitDoer : BeforeDoer, ICallForSpeechSubmitDoer
    {

        private readonly IMapper _mapper;

        public CallForSpeechSubmitDoer
            (IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus<CallForSpeechIds>>
            Run(CallForSpeech callForSpeech)
        {
            var temp = _mapper.Map<CallForSpeechTemp>(callForSpeech);

            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            var q = SqlQueries.CallForSpeechInsert;

            try
            {
                var result = await connection.QueryAsync<int>(q, temp);

                int createdId = result.FirstOrDefault();

                CallForSpeechIds ids = new CallForSpeechIds()
                {
                    CreatedId = new CallForSpeechId(createdId),
                    UniqueId = callForSpeech.UniqueId
                };

                return ExecutionStatus<CallForSpeechIds>.DbOk(ids);
            }
            catch (Exception ex)
            {
                return ExecutionStatus<CallForSpeechIds>.DbError(ex);
            }

            //var result = await connection.QueryAsync<int>(q,
            //     new
            //     {
            //         @Number = entity.Number.Number,
            //         @Name = entity.Name,
            //         @DisplayName = entity.DisplayName,
            //         @WhatWeAreLookingFor = entity.WhatWeAreLookingFor
            //     });
        }
    }
}
