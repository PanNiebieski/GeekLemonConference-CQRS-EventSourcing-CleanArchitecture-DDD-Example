using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Categories;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories
{
    //public class CategoryAddDoer
    //    : IAddDoer<Category, CategoryId, CategoryUniqueId>
    public class CategoryAddDoer
        : BeforeDoer, ICategoryAddDoer
    {

        private readonly IMapper _mapper;

        public CategoryAddDoer
            (IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus<CategoryIds>> Run(Category entity)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            //Nie ma SQL INjection
            var q = @"INSERT INTO Categories(Name, DisplayName, WhatWeAreLookingFor
                ,UniqueId, Version)
                VALUES (@Name, @DisplayName, @WhatWeAreLookingFor,@UniqueId, @Version);
                SELECT seq From sqlite_sequence Where Name='Categories'";

            try
            {
                var result = await connection.QueryAsync<int>(q,
                new
                {
                    @Name = entity.Name,
                    @DisplayName = entity.DisplayName,
                    @WhatWeAreLookingFor = entity.WhatWeAreLookingFor,
                    @UniqueId = entity.UniqueId.Value.ToString(),
                    @Version = entity.Version,
                });

                //var cmd = connection.CreateCommand();
                //cmd.CommandText = "SELECT last_insert_rowid()";
                //Int64 i = (Int64)cmd.ExecuteScalar();

                int createdId = result.FirstOrDefault();

                CategoryIds ids = new CategoryIds()
                {
                    CreatedId = new CategoryId(createdId),
                    UniqueId = entity.UniqueId
                };

                return ExecutionStatus
                    <CategoryIds>
                    .DbOk(ids);
            }
            catch (Exception ex)
            {
                return ExecutionStatus
                    <CategoryIds>
                    .DbError(ex);
            }
        }
    }
}
