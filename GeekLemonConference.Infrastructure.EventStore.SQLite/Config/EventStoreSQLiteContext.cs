
using System;
using System.Collections.Generic;
using System.Data;

using System.Text;

namespace GeekLemonConference.Infrastructure.EventStore.SQLite.Config
{
    public class EventStoreSQLiteContext : IEventStoreSQLiteContext
    {
        public EventStoreSQLiteContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private string _connectionString;

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }
    }
}
