using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges
{
    public class JudgeGetByIdDoer : BeforeDoer, IJudgeGetByIdDoer
    {


        private readonly IMapper _mapper;


        public JudgeGetByIdDoer(IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus<Judge>> Run(JudgeId id)
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);

            var q = @$"SELECT j.Id,j.Login,j.Password,j.BirthDate,j.Name_First, j.Name_Last,
                j.CategoryId ,j.UniqueId ,j.Version ,
                c.Name AS {nameof(JudgeTemp.Category_Name)},
                c.DisplayName AS {nameof(JudgeTemp.Category_DisplayName)}
                ,c.WhatWeAreLookingFor AS {nameof(JudgeTemp.Category_WhatWeAreLookingFor)}
                FROM Judges AS j
                INNER JOIN Categories as C ON j.CategoryId = C.Id
                WHERE j.Id = @Id;";

            try
            {
                var r = await connection.
                 QueryFirstOrDefaultAsync<JudgeTemp>
                 (q, new
                 {
                     @Id = id.Value,
                 });

                var rmaped = _mapper.Map<Judge>(r);
                return ExecutionStatus<Judge>.
                    DbIfDefaultThenError(rmaped);
            }
            catch (Exception ex)
            {
                return ExecutionStatus<Judge>.DbError(ex);
            }
        }

        public async Task<ExecutionStatus<Judge>> Run(JudgeUniqueId id)
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);

            var q = @$"SELECT j.Id,j.Login,j.Password,j.BirthDate,j.Name_First, j.Name_Last,
                j.CategoryId ,j.UniqueId ,j.Version ,
                c.Name AS {nameof(JudgeTemp.Category_Name)},
                c.DisplayName AS {nameof(JudgeTemp.Category_DisplayName)}
                ,c.WhatWeAreLookingFor AS {nameof(JudgeTemp.Category_WhatWeAreLookingFor)}
                FROM Judges AS j
                INNER JOIN Categories as C ON j.CategoryId = C.Id
                WHERE j.UniqueId = @UniqueId;";

            try
            {
                var r = await connection.
                 QueryFirstOrDefaultAsync<JudgeTemp>
                 (q, new
                 {
                     @UniqueId = id.Value.ToString(),
                 });

                var rmaped = _mapper.Map<Judge>(r);
                return ExecutionStatus<Judge>.DbIfDefaultThenError(rmaped);
            }
            catch (Exception ex)
            {
                return ExecutionStatus<Judge>.DbError(ex);
            }
        }
    }
}
