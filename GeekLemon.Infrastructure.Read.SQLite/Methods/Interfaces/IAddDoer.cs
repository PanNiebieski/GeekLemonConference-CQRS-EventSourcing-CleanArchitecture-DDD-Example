using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories
{
    public interface IAddDoer<T, IdT1, UniqueIdT2> where IdT1 : ValueObject<IdT1>
        where UniqueIdT2 : ValueObject<UniqueIdT2>
    {
        Task<ExecutionStatus<Ids<IdT1, UniqueIdT2>>> Run(T entity);
    }


}
