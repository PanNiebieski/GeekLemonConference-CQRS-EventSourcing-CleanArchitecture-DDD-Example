using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Judges;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges
{
    //public class JudgeGetAllDoer : IGetAllDoer<Judge>
    public class JudgeGetAllDoer : BeforeDoer, IJudgeGetAllDoer
    {


        private readonly IMapper _mapper;


        public JudgeGetAllDoer(IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus<IReadOnlyList<Judge>>> Run()
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);

            var q = @$"SELECT j.Id, j.Login,j.Password,j.BirthDate,j.Name_First, j.Name_Last,
                j.CategoryId, j.UniqueId ,j.Version ,
				c.Name AS {nameof(JudgeTemp.Category_Name)},
                c.DisplayName AS {nameof(JudgeTemp.Category_DisplayName)}
                ,c.WhatWeAreLookingFor AS {nameof(JudgeTemp.Category_WhatWeAreLookingFor)}
                FROM Judges AS j
                INNER JOIN Categories as C ON j.CategoryId = C.Id";

            try
            {
                var r = await connection.QueryAsync<JudgeTemp>
                    (q);

                var rmaped = _mapper.Map<IEnumerable<Judge>>(r);

                //var r2 = await connection.QueryAsync<Judge>
                //($"SELECT Id,Login AS {nameof(Judge.Login.Value)}," +
                //$"Password AS {nameof(Judge.Password.Value)}, " +
                //$"BirthDate AS {nameof(Judge.Birthdate)}, " +
                //$"Name_First AS {nameof(Judge.Name.First)}, " +
                //$"Name_Last, AS {nameof(Judge.Name.Last)}" +
                //$"Email_ForeConference AS {nameof(Judg)}" +
                //$",Email_ForSpeakers AS {nameof(Judge.Login.Value)}," +
                //$"Phone_ForConference AS {nameof(Judge.Login.Value)}," +
                //$"Phone_ForSpekers AS {nameof(Judge.Login.Value)}, " +
                //$"CategoryId AS {nameof(Judge.Login.Value)} FROM Judges; ");

                return ExecutionStatus<IReadOnlyList<Judge>>
                    .DbIfDefaultThenError(rmaped.ToList().AsReadOnly());
            }
            catch (Exception ex)
            {
                return ExecutionStatus<IReadOnlyList<Judge>>.DbError(ex.Message);
            }
        }
    }
}
