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
    public interface IJudgeRepository :
        IAsyncRepository<Judge>
    {
        Task<ExecutionStatus> DeleteAsync(JudgeId entity);

        Task<ExecutionStatus> DeleteAsync(JudgeUniqueId id);

        Task<ExecutionStatus<JudgeIds>> AddAsync(Judge entity);

        Task<ExecutionStatus<Judge>> GetByIdAsync(JudgeId id);

        Task<ExecutionStatus<Judge>> GetByIdAsync(JudgeUniqueId id);
    }
}
