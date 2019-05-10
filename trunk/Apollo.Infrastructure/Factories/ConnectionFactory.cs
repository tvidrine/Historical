using System.Data.SqlClient;
using Apollo.Core.Contracts.Configuration;

namespace Apollo.Infrastructure.Factories
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IAuditConfiguration _configuration;

        public ConnectionFactory(IAuditConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_configuration.AuditDataConnection);
            connection.Open();

            return connection;
        }
    }
}