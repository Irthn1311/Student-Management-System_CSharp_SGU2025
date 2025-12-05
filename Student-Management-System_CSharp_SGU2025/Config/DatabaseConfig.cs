using System;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace Student_Management_System_CSharp_SGU2025.Config
{
    /// <summary>
    /// Cấu hình database từ file JSON
    /// </summary>
    public class DatabaseConfig
    {
        private static readonly string ConfigFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Config",
            "database_config.json"
        );

        private static DatabaseConfigRoot _config;
        private static readonly object _lock = new object();
        private static readonly JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();

        /// <summary>
        /// Lấy cấu hình database (singleton pattern với lazy loading)
        /// </summary>
        public static DatabaseConfigRoot GetConfig()
        {
            if (_config == null)
            {
                lock (_lock)
                {
                    if (_config == null)
                    {
                        _config = LoadConfig();
                    }
                }
            }
            return _config;
        }

        /// <summary>
        /// Tải cấu hình từ file JSON
        /// </summary>
        private static DatabaseConfigRoot LoadConfig()
        {
            try
            {
                if (!File.Exists(ConfigFilePath))
                {
                    // Tạo file mặc định nếu chưa tồn tại
                    CreateDefaultConfig();
                }

                string jsonContent = File.ReadAllText(ConfigFilePath, Encoding.UTF8);
                var config = JsonSerializer.Deserialize<DatabaseConfigRoot>(jsonContent);

                if (config == null || config.Database == null)
                {
                    throw new Exception("Cấu hình database không hợp lệ!");
                }

                return config;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi đọc cấu hình database từ file: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tạo file cấu hình mặc định
        /// </summary>
        private static void CreateDefaultConfig()
        {
            try
            {
                // Đảm bảo thư mục Config tồn tại
                string configDir = Path.GetDirectoryName(ConfigFilePath);
                if (!Directory.Exists(configDir))
                {
                    Directory.CreateDirectory(configDir);
                }

                // Tạo cấu hình mặc định
                var defaultConfig = new DatabaseConfigRoot
                {
                    Database = new DatabaseSettings
                    {
                        Server = "127.0.0.1",
                        Database = "QuanLyHocSinh",
                        UserId = "root",
                        Password = "12345678",
                        Port = 3306,
                        ConnectionTimeout = 30
                    }
                };

                // Lưu file
                string json = JsonSerializer.Serialize(defaultConfig);
                File.WriteAllText(ConfigFilePath, json, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tạo file cấu hình mặc định: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lưu cấu hình vào file JSON
        /// </summary>
        public static void SaveConfig(DatabaseConfigRoot config)
        {
            try
            {
                string configDir = Path.GetDirectoryName(ConfigFilePath);
                if (!Directory.Exists(configDir))
                {
                    Directory.CreateDirectory(configDir);
                }

                string json = JsonSerializer.Serialize(config);
                File.WriteAllText(ConfigFilePath, json, Encoding.UTF8);

                // Reset singleton để tải lại cấu hình mới
                lock (_lock)
                {
                    _config = config;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu cấu hình database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy connection string cho ADO.NET (format: Server=...;Database=...;Uid=...;Pwd=...;)
        /// </summary>
        public static string GetAdoNetConnectionString()
        {
            var db = GetConfig().Database;
            return $"Server={db.Server};Database={db.Database};Uid={db.UserId};Pwd={db.Password};Port={db.Port};Connection Timeout={db.ConnectionTimeout};";
        }

        /// <summary>
        /// Lấy connection string cho Entity Framework (format: server=...;database=...;user id=...;password=...;)
        /// </summary>
        public static string GetEntityFrameworkConnectionString()
        {
            var db = GetConfig().Database;
            return $"server={db.Server};database={db.Database};user id={db.UserId};password={db.Password};port={db.Port};Connection Timeout={db.ConnectionTimeout};";
        }

        /// <summary>
        /// Reload cấu hình từ file (dùng khi file được thay đổi bên ngoài)
        /// </summary>
        public static void ReloadConfig()
        {
            lock (_lock)
            {
                _config = null;
                _config = LoadConfig();
            }
        }
    }

    /// <summary>
    /// Root class cho cấu hình database
    /// </summary>
    public class DatabaseConfigRoot
    {
        public DatabaseSettings Database { get; set; }
    }

    /// <summary>
    /// Cài đặt database
    /// </summary>
    public class DatabaseSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public int Port { get; set; } = 3306;
        public int ConnectionTimeout { get; set; } = 30;
    }
}

