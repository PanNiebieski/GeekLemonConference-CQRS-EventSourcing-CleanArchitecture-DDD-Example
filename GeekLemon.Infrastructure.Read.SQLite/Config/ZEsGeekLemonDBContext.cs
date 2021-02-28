using GeekLemon.Persistence.Dapper.SQLite;

namespace GeekLemon.Infrastructure.Read.SQLite
{
    public class ZEsGeekLemonDBContext : IZEsGeekLemonDBContext
    {
        public ZEsGeekLemonDBContext(string connectionString)
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
