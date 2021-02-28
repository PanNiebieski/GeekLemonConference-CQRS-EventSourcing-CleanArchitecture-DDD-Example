using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Categories
{
    public interface ICategoryGetAllDoer : IBeforeDoer
    {
        Task<ExecutionStatus<IReadOnlyList<Category>>> Run();
    }
}
