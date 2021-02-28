using GeekLemonConference.Application.Contracts.Persistence;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.Contracts.Repository
{
    public interface ICategoryRepository
        : IAsyncRepository<Category>
    {
        Task<ExecutionStatus> DeleteAsync(CategoryId categoryId);

        Task<ExecutionStatus> DeleteAsync(CategoryUniqueId categoryId);

        Task<ExecutionStatus<CategoryIds>> AddAsync(Category entity);

        Task<ExecutionStatus<Category>> GetByIdAsync(CategoryId id);

        Task<ExecutionStatus<Category>> GetByIdAsync(CategoryUniqueId id);
    }
}
