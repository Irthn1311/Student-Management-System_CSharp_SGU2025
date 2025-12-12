using System;
using System.Diagnostics;

namespace Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase
{
    /// <summary>
    /// Class cấu hình chung cho kết nối database
    /// Được sử dụng bởi cả ADO.NET (ConnectionDatabase) và Entity Framework (SchoolDbContext)
    /// 
    /// Logic tự động phân biệt server/client:
    /// - Nếu app chạy trên máy server (Environment.MachineName == ServerMachineName)
    ///   → dùng 127.0.0.1 để kết nối MySQL cho nhanh
    /// - Nếu app chạy trên máy client
    ///   → dùng hostname máy server (ServerMachineName) để kết nối qua LAN
    /// 
    /// Nhờ vậy:
    /// - Client không phải cấu hình IP thủ công
    /// - Khi IP LAN của server thay đổi, hostname vẫn tự resolve đúng trong mạng LAN
    /// - User low-tech không cần sửa config/IP
    /// </summary>
    public static class DatabaseConfig
    {
        #region Cấu hình Database

        /// <summary>
        /// Tên máy tính của máy server (hostname)
        /// Ví dụ: "HUU_TRI_LAPTOP", "SERVER-PC", "Irthn", v.v.
        /// </summary>
        public const string ServerMachineName = "Irthn";

        /// <summary>
        /// Tên database MySQL
        /// </summary>
        public const string DbName = "QuanLyHocSinh";

        /// <summary>
        /// Username MySQL
        /// </summary>
        public const string DbUser = "root";

        /// <summary>
        /// Password MySQL
        /// Để trống "" nếu MySQL không có password
        /// </summary>
        public const string DbPassword = "12345678";

        /// <summary>
        /// Port MySQL (mặc định 3306)
        /// </summary>
        public const int DbPort = 3306;

        /// <summary>
        /// Connection timeout (giây)
        /// </summary>
        public const int DbTimeoutSeconds = 30;

        #endregion

        #region Logic tự động chọn host

        /// <summary>
        /// Lấy hostname/IP của MySQL server dựa trên máy hiện tại
        /// 
        /// Logic:
        /// - Nếu app đang chạy trên máy server (Environment.MachineName == ServerMachineName)
        ///   → trả về "127.0.0.1" (localhost) để kết nối nhanh
        /// - Nếu app đang chạy trên máy client
        ///   → trả về ServerMachineName (hostname máy server) để kết nối qua LAN
        /// 
        /// Nhờ vậy:
        /// - Client không cần cấu hình IP thủ công
        /// - Khi IP LAN của server thay đổi, hostname vẫn tự resolve đúng trong mạng LAN
        /// - User low-tech không cần sửa IP/config
        /// </summary>
        /// <returns>
        /// "127.0.0.1" nếu đang chạy trên máy server,
        /// ServerMachineName nếu đang chạy trên máy client
        /// </returns>
        public static string GetServerHost()
        {
            try
            {
                string currentMachineName = Environment.MachineName;

                bool isServerMachine = string.Equals(
                    currentMachineName,
                    ServerMachineName,
                    StringComparison.OrdinalIgnoreCase
                );

                string host = isServerMachine ? "127.0.0.1" : ServerMachineName;

                // Log đã được comment để tránh spam console
                // Uncomment dòng dưới nếu cần debug:
                // Debug.WriteLine($"[DatabaseConfig] Current machine: {currentMachineName}, " +
                //               $"Is server: {isServerMachine}, " +
                //               $"Using host: {host}");

                return host;
            }
            catch (Exception ex)
            {
                // Nếu có lỗi khi lấy MachineName, fallback về localhost
                Debug.WriteLine($"[DatabaseConfig] Error getting server host: {ex.Message}. Using fallback: 127.0.0.1");
                return "127.0.0.1";
            }
        }

        #endregion
    }
}

