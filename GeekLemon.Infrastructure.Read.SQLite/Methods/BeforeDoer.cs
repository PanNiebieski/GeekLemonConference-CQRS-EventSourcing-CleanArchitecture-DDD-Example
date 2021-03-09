using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods
{
    public abstract class BeforeDoer : IBeforeDoer
    {
        protected IGeekLemonDBContext _geekLemonContext;

        public void ChangeDBContext(IGeekLemonDBContext context)
        {
            _geekLemonContext = context;
        }

      
    }

    public class DapperDbConnectionFactory : IDbConnectionFactory
    {
        private readonly IDictionary<string, string> _connectionDict;

        public DapperDbConnectionFactory(IDictionary<string, string> connectionDict)
        {
            _connectionDict = connectionDict;
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            string connectionString = null;
            if (_connectionDict.TryGetValue(nameOrConnectionString, out connectionString))
            {
                return new SqlConnection(connectionString);
            }

            throw new ArgumentNullException();
        }

    }
}
