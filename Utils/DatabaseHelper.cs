using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace QuanLiQuanAn.Utils
{
    public class DatabaseHelper
    {
        private readonly IConfiguration _configuration;

        public DatabaseHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
