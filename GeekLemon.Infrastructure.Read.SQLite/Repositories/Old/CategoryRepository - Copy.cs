//using AutoMapper;
//using Dapper;
//using GeekLemon.Persistence.Dapper.SQLite;
//using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
//using GeekLemonConference.Application.Contracts.Persistence;
//using GeekLemonConference.Application.Contracts.Repository;
//using GeekLemonConference.Domain;
//using GeekLemonConference.Domain.Entities;
//using GeekLemonConference.Domain.ValueObjects.Ids;
//using Microsoft.Data.Sqlite;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GeekLemon.Infrastructure.Read.SQLite.Repositories
//{
//    public class CategoryRepository : ICategoryRepository
//    {
//        private IGeekLemonDBContext _geekLemonContext;
//        private readonly IMapper _mapper;

//        public CategoryRepository(IGeekLemonDBContext geekLemonContext,
//            IMapper mapper)
//        {
//            _geekLemonContext = geekLemonContext;
//            _mapper = mapper;
//        }

//        public async Task<ExecutionStatus<int>> AddAsync(Category entity)
//        {
//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            //Nie ma SQL INjection
//            var q = @"INSERT INTO Categories(Name, DisplayName, WhatWeAreLookingFor
//                ,@UniqueId, Version)
//                VALUES (@Name, @DisplayName, @WhatWeAreLookingFor,@UniqueId, @Version);
//                SELECT last_insert_rowid();";

//            try
//            {
//                var result = await connection.QueryAsync<int>(q,
//                new
//                {
//                    @Name = entity.Name,
//                    @DisplayName = entity.DisplayName,
//                    @WhatWeAreLookingFor = entity.WhatWeAreLookingFor
//                });

//                int id = result.FirstOrDefault();

//                return ExecutionStatus<int>.DbOk(id);
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus<int>.DbError(ex);
//            }

//        }

//        public async Task<ExecutionStatus> DeleteAsync(CategoryId entity)
//        {
//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            var q = @"DELETE FROM Categories WHERE Id=@Id;";


//            try
//            {
//                var result = await connection.QueryAsync<int>(q,
//                 new
//                 {
//                     @Id = entity.Value
//                 });
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus.DbError(ex);
//            }

//            return ExecutionStatus.DbOk();
//        }

//        public async Task<ExecutionStatus<IReadOnlyList<Category>>> GetAllAsync()
//        {
//            using var connection = new SqliteConnection
//                (_geekLemonContext.ConnectionString);

//            try
//            {
//                var r = await connection.QueryAsync<CategoryTemp>
//                (@"SELECT Id, DisplayName, Name ,
//                WhatWeAreLookingFor,UniqueId ,Version FROM Categories;");

//                var rmaped = _mapper.Map<IEnumerable<Category>>(r);

//                return ExecutionStatus<IReadOnlyList<Category>>.DbOk(rmaped.ToList().AsReadOnly());
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus<IReadOnlyList<Category>>.DbError(ex);
//            }

//        }

//        public async Task<ExecutionStatus<Category>> GetByIdAsync(int id)
//        {
//            using var connection = new SqliteConnection
//                (_geekLemonContext.ConnectionString);

//            var q = @"SELECT Id, DisplayName, Name,
//                WhatWeAreLookingFor,UniqueId ,Version , FROM Categories
//                Where Id = @Id";

//            try
//            {
//                var r = await connection.
//                QueryFirstOrDefaultAsync<CategoryTemp>
//                (q, new
//                {
//                    @Id = id,
//                });

//                var rmaped = _mapper.Map<Category>(r);

//                return ExecutionStatus<Category>.DbOk(rmaped);
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus<Category>.DbError(ex);
//            }

//        }

//        public async Task<ExecutionStatus> UpdateAsync(Category entity)
//        {
//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            //Nie ma SQL INjection
//            var q = @"UPDATE Categories 
//                SET Name = @Name, DisplayName = @DisplayName, WhatWeAreLookingFor=@WhatWeAreLookingFor
//                ,UniqueId = @UniqueId, Version = @Version
//                WHERE Id = @Id; ";
//            try
//            {
//                var result = await connection.QueryAsync<int>(q,
//                 new
//                 {
//                     @Id = entity.Id.Value,
//                     @Name = entity.Name,
//                     @DisplayName = entity.DisplayName,
//                     @WhatWeAreLookingFor = entity.WhatWeAreLookingFor
//                 });

//                return ExecutionStatus.DbOk();
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus.DbError(ex);
//            }


//        }

//        public Task<ExecutionStatus> DeleteByUniqueIdAsync(CategoryUniqueId categoryId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ExecutionStatus<Category>> GetByUniqueIdAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        Task<ExecutionStatus<Ids>> IAsyncRepository<Category>.AddAsync(Category entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
