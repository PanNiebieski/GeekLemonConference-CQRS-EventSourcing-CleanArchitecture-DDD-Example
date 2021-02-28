using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.Repositories;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.CallForSpeeches;
using GeekLemonConference.Persistence.Dapper.SQLite.Repositories;
using GeekLemonConference.Persistence.Dapper.SQLite.Repositories.CallForSpeeches;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemon.Infrastructure.Read.SQLite
{
    public class ZEsCallForSpeechRepository : CallForSpeechRepository,
        IZEsCallForSpeechRepository
    {

        public ZEsCallForSpeechRepository(ICallForSpeechGetByIdDoer callForSpeechGetByIdDoer,
            ICallForSpeechGetCollectionDoer callForSpeechGetCollectionDoer,
            ICallForSpeechSaveAcceptenceDoer callForSpeechSaveAcceptenceDoer,
            ICallForSpeechSaveEvaluatationDoer callForSpeechSaveEvaluatationDoer,
            ICallForSpeechSavePreliminaryAcceptenceDoer callForSpeechSavePreliminaryAcceptenceDoer,
            ICallForSpeechSaveRejectionDoer callForSpeechSaveRejectionDoer,
            ICallForSpeechSubmitDoer callForSpeechSubmitDoer,
            IZEsGeekLemonDBContext _zEsGeekLemonDBContext)
            : base(callForSpeechGetByIdDoer, callForSpeechGetCollectionDoer,
                  callForSpeechSaveAcceptenceDoer, callForSpeechSaveEvaluatationDoer,
                  callForSpeechSavePreliminaryAcceptenceDoer, callForSpeechSaveRejectionDoer,
                  callForSpeechSubmitDoer)
        {
            GeekLemonDBContext context =
                new GeekLemonDBContext(_zEsGeekLemonDBContext.ConnectionString);

            this.ChangeContext(context);

        }

    }
}
