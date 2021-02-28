using GeekLemon.Persistence.Dapper.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace GeekLemon.Infrastructure.Read.SQLite
{
    public class GeekLemonDBContext : IGeekLemonDBContext
    {
        public GeekLemonDBContext(string connectionString)
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
