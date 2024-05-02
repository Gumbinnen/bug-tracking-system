using Microsoft.Data.SqlClient;
using System.Data;

namespace BugTrackingSystem.Database
{
    public class DapperDBContext
    {
        private readonly string _connectionString;

        public DapperDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);
    }
}
