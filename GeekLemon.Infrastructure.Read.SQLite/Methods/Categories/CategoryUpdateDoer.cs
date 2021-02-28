using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemonConference.Application.Contracts.Persistence;
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
    //public class CategoryUpdateDoer : IUpdateDoer<Category>
    public class CategoryUpdateDoer : BeforeDoer, ICategoryUpdateDoer
    {
        private readonly IMapper _mapper;

        public CategoryUpdateDoer
            (IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus> Run(Category entity, ByWhatId byWhatId)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            //Nie ma SQL INjection
            var q1 = @"UPDATE Categories 
                SET Name = @Name, DisplayName = @DisplayName, WhatWeAreLookingFor=@WhatWeAreLookingFor
                ,Version = @Version
                WHERE UniqueId = @UniqueId; ";

            var q = @"UPDATE Categories 
                SET Name = @Name, DisplayName = @DisplayName, WhatWeAreLookingFor=@WhatWeAreLookingFor
                ,Version = @Version
                WHERE Id = @Id; ";
            try
            {
                if (byWhatId == ByWhatId.UniqueId)
                    q = q1;

                var result = await connection.QueryAsync<int>(q,
                 new
                 {
                     @UniqueId = entity.UniqueId.Value.ToString(),
                     @Id = entity.Id.Value,
                     @Name = entity.Name,
                     @Version = entity.Version,
                     @DisplayName = entity.DisplayName,
                     @WhatWeAreLookingFor = entity.WhatWeAreLookingFor
                 });

                return ExecutionStatus.DbOk();
            }
            catch (Exception ex)
            {
                if (ExecutionFlow.Options.ThrowExceptions)
                    throw;

                return ExecutionStatus.DbError(ex);
            }
        }
    }
}
