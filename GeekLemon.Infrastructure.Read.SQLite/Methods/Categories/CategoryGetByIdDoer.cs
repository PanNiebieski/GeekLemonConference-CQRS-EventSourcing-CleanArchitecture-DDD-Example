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

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories
{
    public class CategoryGetByIdDoer : BeforeDoer, ICategoryGetByIdDoer
    {

        private readonly IMapper _mapper;

        public CategoryGetByIdDoer
            (IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus<Category>> Run(CategoryUniqueId categoryId)
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);

            var q = @"SELECT Id, DisplayName, Name,
                WhatWeAreLookingFor,UniqueId ,Version  FROM Categories
                Where UniqueId = @UniqueId";

            try
            {
                var r = await connection.
                QueryFirstOrDefaultAsync<CategoryTemp>
                (q, new
                {
                    @UniqueId = categoryId.Value.ToString(),
                });

                var rmaped = _mapper.Map<Category>(r);

                return ExecutionStatus<Category>.
                    DbIfDefaultThenError(rmaped);
            }
            catch (Exception ex)
            {
                return ExecutionStatus<Category>.DbError(ex);
            }
        }

        public async Task<ExecutionStatus<Category>> Run(CategoryId categoryId)
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);

            var q = @"SELECT Id, DisplayName, Name,
                WhatWeAreLookingFor,UniqueId ,Version  FROM Categories
                Where Id = @Id";

            try
            {
                var r = await connection.
                QueryFirstOrDefaultAsync<CategoryTemp>
                (q, new
                {
                    @Id = categoryId.Value,
                });

                var rmaped = _mapper.Map<Category>(r);

                return ExecutionStatus<Category>.DbIfDefaultThenError(rmaped);
            }
            catch (Exception ex)
            {
                return ExecutionStatus<Category>.DbError(ex);
            }
        }
    }
}
