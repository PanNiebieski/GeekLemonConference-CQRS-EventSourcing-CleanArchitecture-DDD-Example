using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemon.Persistence.Dapper.SQLite
{
    public interface IGeekLemonDBContext
    {
        string ConnectionString { get; }
    }
}
