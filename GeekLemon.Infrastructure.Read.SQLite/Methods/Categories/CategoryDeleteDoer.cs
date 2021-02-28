using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories
{
    public class CategoryDeleteDoer : BeforeDoer, ICategoryDeleteDoer
    {

        private readonly IMapper _mapper;

        public CategoryDeleteDoer
            (IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus> Run(CategoryId categoryId)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            var q = @"DELETE FROM Categories WHERE Id=@Id;";

            try
            {
                var result = await connection.QueryAsync<int>(q,
                 new
                 {
                     @Id = categoryId.Value
                 });
            }
            catch (Exception ex)
            {
                if (ExecutionFlow.Options.ThrowExceptions)
                    throw;

                return ExecutionStatus.DbError(ex);
            }

            return ExecutionStatus.DbOk();
        }

        public async Task<ExecutionStatus> Run(CategoryUniqueId categoryId)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            var q = @"DELETE FROM Categories WHERE UniqueId=@UniqueId;";

            try
            {
                var result = await connection.QueryAsync<int>(q,
                 new
                 {
                     @UniqueId = categoryId.Value.ToString()
                 });
            }
            catch (Exception ex)
            {
                if (ExecutionFlow.Options.ThrowExceptions)
                    throw;

                return ExecutionStatus.DbError(ex);
            }

            return ExecutionStatus.DbOk();
        }
    }
}
