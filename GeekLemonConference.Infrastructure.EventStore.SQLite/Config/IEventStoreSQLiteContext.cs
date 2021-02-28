using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.EventStore.SQLite.Config
{
    public interface IEventStoreSQLiteContext
    {
        string ConnectionString { get; }
    }
}
