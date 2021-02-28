using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Application.Contracts.Persistence;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Categories;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemon.Infrastructure.Read.SQLite.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ICategoryAddDoer _categoryAddDoer;
        private ICategoryGetAllDoer _categoryGetAllDoer;
        private ICategoryDeleteDoer _categoryDeleteDoer;
        private ICategoryGetByIdDoer _categoryGetByIdDoer;
        private ICategoryUpdateDoer _categoryUpdateDoer;

        public CategoryRepository(ICategoryAddDoer categoryAddDoer,
            ICategoryGetAllDoer categoryGetAllDoer, ICategoryDeleteDoer categoryDeleteDoer,
            ICategoryGetByIdDoer categoryGetByIdDoer, ICategoryUpdateDoer categoryUpdateDoer)
        {
            _categoryAddDoer = categoryAddDoer;
            _categoryGetAllDoer = categoryGetAllDoer;
            _categoryDeleteDoer = categoryDeleteDoer;
            _categoryGetByIdDoer = categoryGetByIdDoer;
            _categoryUpdateDoer = categoryUpdateDoer;
        }

        public void ChangeContext(IGeekLemonDBContext geekLemonDB)
        {
            _categoryAddDoer.ChangeDBContext(geekLemonDB);
            _categoryGetAllDoer.ChangeDBContext(geekLemonDB);
            _categoryDeleteDoer.ChangeDBContext(geekLemonDB);
            _categoryGetByIdDoer.ChangeDBContext(geekLemonDB);
            _categoryUpdateDoer.ChangeDBContext(geekLemonDB);
        }

        public Task<ExecutionStatus<CategoryIds>> AddAsync(Category entity)
        {
            return _categoryAddDoer.Run(entity);
        }

        public Task<ExecutionStatus> DeleteAsync(CategoryId categoryId)
        {
            return _categoryDeleteDoer.Run(categoryId);
        }

        public Task<ExecutionStatus> DeleteAsync(CategoryUniqueId categoryId)
        {
            return _categoryDeleteDoer.Run(categoryId);
        }

        public Task<ExecutionStatus<IReadOnlyList<Category>>> GetAllAsync()
        {
            return _categoryGetAllDoer.Run();
        }

        public Task<ExecutionStatus<Category>> GetByIdAsync(CategoryId id)
        {
            return _categoryGetByIdDoer.Run(id);
        }

        public Task<ExecutionStatus<Category>> GetByIdAsync(CategoryUniqueId id)
        {
            return _categoryGetByIdDoer.Run(id);
        }

        public Task<ExecutionStatus> UpdateByUniqueIdAsync(Category entity)
        {
            return _categoryUpdateDoer.Run(entity, ByWhatId.UniqueId);
        }

        public Task<ExecutionStatus> UpdateByIdAsync(Category entity)
        {
            return _categoryUpdateDoer.Run(entity, ByWhatId.CreatedId);
        }
    }
}
