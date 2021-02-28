using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories
{
    public interface IUpdateDoer<T>
    {
        Task<ExecutionStatus> Run(T entity);
    }
}
