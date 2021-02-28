using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
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
    public class JudgeDeleteDoer : BeforeDoer, IJudgeDeleteDoer
    {


        private readonly IMapper _mapper;


        public JudgeDeleteDoer(IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus> Run(JudgeId id)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            var q = "DELETE FROM Judges WHERE Id=@Id;";

            try
            {
                var result = await connection.QueryAsync<int>(q,
                 new
                 {
                     @Id = id.Value
                 });

                return ExecutionStatus.DbOk();
            }
            catch (Exception ex)
            {
                return ExecutionStatus.DbError(ex.Message);
            }
        }

        public async Task<ExecutionStatus> Run(JudgeUniqueId id)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            var q = "DELETE FROM Judges WHERE UniqueId=@UniqueId;";

            try
            {
                var result = await connection.QueryAsync<int>(q,
                 new
                 {
                     @UniqueId = id.Value.ToString()
                 });

                return ExecutionStatus.DbOk();
            }
            catch (Exception ex)
            {
                return ExecutionStatus.DbError(ex.Message);
            }
        }
    }
}
