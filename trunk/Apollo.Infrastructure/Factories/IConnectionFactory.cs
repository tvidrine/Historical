using System.Data.SqlClient;

namespace Apollo.Infrastructure.Factories
{
    public interface IConnectionFactory
    {
        SqlConnection GetConnection();
    }
}