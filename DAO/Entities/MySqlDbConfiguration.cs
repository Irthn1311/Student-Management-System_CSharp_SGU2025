using System.Data.Entity;
using MySql.Data.EntityFramework;  // For MySqlConnectionFactory
using MySql.Data.MySqlClient;  // For MySqlProviderServices and MySqlClientFactory

namespace Student_Management_System_CSharp_SGU2025.DAO.Entities
{
    /// <summary>
    /// Cấu hình Entity Framework để sử dụng MySQL provider
    /// Đảm bảo Entity Framework không fallback về SQL Server
    /// Sử dụng với MySql.Data 9.5.0 và MySql.Data.EntityFramework 9.5.0
    /// </summary>
    public class MySqlDbConfiguration : DbConfiguration
    {
        public MySqlDbConfiguration()
        {
            // Set MySQL as the default connection factory
            // MySqlConnectionFactory is in MySql.Data.EntityFramework namespace
            SetDefaultConnectionFactory(new MySqlConnectionFactory());
            
            // Set MySQL provider services
            // MySqlProviderServices is in MySql.Data.MySqlClient namespace
            // Use 'new' constructor, not Instance property
            SetProviderServices("MySql.Data.MySqlClient", new MySqlProviderServices());
            
            // Set MySQL provider factory
            SetProviderFactory("MySql.Data.MySqlClient", MySqlClientFactory.Instance);
        }
    }
}
