using GeekLemonConference.Application.Contracts.Persistence;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.Contracts.Repository
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<ExecutionStatus<IReadOnlyList<T>>> GetAllAsync();

        Task<ExecutionStatus> UpdateByUniqueIdAsync(T entity);

        Task<ExecutionStatus> UpdateByIdAsync(T entity);
    }


}
