using Microsoft.Data.Sqlite;
using System.Data;

namespace SwiftProject.Data
{
    public class SqliteConnectionFactory(IConfiguration config) : ISqliteConnectionFactory
    {
        private readonly string _connectionString = config.GetConnectionString("Default") ?? "Data Source=swift-database.db";

        IDbConnection ISqliteConnectionFactory.CreateConnection()
            => new SqliteConnection(_connectionString);
    }
}
