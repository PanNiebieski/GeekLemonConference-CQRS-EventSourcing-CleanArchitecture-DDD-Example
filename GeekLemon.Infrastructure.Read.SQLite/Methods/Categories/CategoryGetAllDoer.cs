using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Categories;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories
{
    //public class CategoryGetAllDoer : IGetAllDoer<Category>
    public class CategoryGetAllDoer : BeforeDoer, ICategoryGetAllDoer
    {

        private readonly IMapper _mapper;

        public CategoryGetAllDoer
            (IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus<IReadOnlyList<Category>>> Run()
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);

            try
            {
                var r = await connection.QueryAsync<CategoryTemp>
                (@"SELECT Id, DisplayName, Name ,
                WhatWeAreLookingFor,UniqueId ,Version FROM Categories;");

                var rmaped = _mapper.Map<IEnumerable<Category>>(r);

                return ExecutionStatus<IReadOnlyList<Category>>
                    .DbIfDefaultThenError
                    (rmaped.ToList().AsReadOnly());
            }
            catch (Exception ex)
            {
                return ExecutionStatus<IReadOnlyList<Category>>
                    .DbError(ex);
            }
        }
    }
}
