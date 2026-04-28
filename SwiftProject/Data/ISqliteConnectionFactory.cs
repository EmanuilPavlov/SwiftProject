using System.Data;

namespace SwiftProject.Data
{
    public interface ISqliteConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
