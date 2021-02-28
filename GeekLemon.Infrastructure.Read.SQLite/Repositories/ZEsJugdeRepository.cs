using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Judges;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemon.Infrastructure.Read.SQLite
{
    public class ZEsJugdeRepository : JugdeRepository, IZEsJudgeRepository
    {

        public ZEsJugdeRepository(IJudgeAddDoer judgeAddDoer,
            IJudgeUpdateDoer judgeUpdateDoer, IJudgeDeleteDoer judgeDeleteDoer,
            IJudgeGetAllDoer judgeGetAllDoer, IJudgeGetByIdDoer judgeGetByIdDoer,
            IZEsGeekLemonDBContext zEsGeekLemonDBContext)
            : base(judgeAddDoer, judgeUpdateDoer, judgeDeleteDoer, judgeGetAllDoer,
                  judgeGetByIdDoer)
        {
            GeekLemonDBContext context =
                new GeekLemonDBContext(zEsGeekLemonDBContext.ConnectionString);

            this.ChangeContext(context);
        }

    }
}
