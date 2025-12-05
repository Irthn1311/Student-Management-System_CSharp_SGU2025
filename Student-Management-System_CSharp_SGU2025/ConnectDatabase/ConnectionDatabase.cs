using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.ConnectDatabase
{
    internal class ConnectionDatabase
    {
        /// <summary>
        /// Lấy connection string - Hardcoded trong code
        /// Để thay đổi cấu hình, sửa trực tiếp trong method này
        /// </summary>
        private static string GetConnectionString()
        {
            // ✅ Cấu hình database - Sửa các giá trị dưới đây
            string server = "127.0.0.1";
            string database = "QuanLyHocSinh";
            string userId = "root";
            string password = "12345678";  // Để trống "" nếu localhost không có password
            int port = 3306;
            int connectionTimeout = 30;

            // Tạo connection string
            string serverWithPort = port != 3306 ? $"{server}:{port}" : server;
            string pwdParam = string.IsNullOrEmpty(password) ? "" : $"Pwd={password};";
            return $"Server={serverWithPort};Database={database};Uid={userId};{pwdParam}Connection Timeout={connectionTimeout};";
        }

        /// <summary>
        /// Lấy kết nối đến cơ sở dữ liệu MySQL
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            try
            {
                string connString = GetConnectionString();
                MySqlConnection connection = new MySqlConnection(connString);
                return connection;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Lỗi kết nối database: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiểm tra kết nối database với thông báo chi tiết
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    // ✅ Ghi log thành công
                    Debug.WriteLine("✅ Kết nối database thành công!");
                    Debug.WriteLine($"   Server: {conn.ServerVersion}");
                    Debug.WriteLine($"   Database: {conn.Database}");

                    Console.WriteLine("✅ Kết nối thành công!");

                    return conn.State == ConnectionState.Open;
                }
            }
            catch (MySqlException ex)
            {
                // ❌ Ghi log lỗi chi tiết
                Debug.WriteLine($"❌ Lỗi kết nối MySQL: {ex.Message}");
                Debug.WriteLine($"   Error Code: {ex.Number}");

                Console.WriteLine($"❌ Lỗi kết nối: {ex.Message}");

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Lỗi không xác định: {ex.Message}");
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra kết nối với thông báo MessageBox
        /// </summary>
        public static bool TestConnectionWithMessage()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        MessageBox.Show(
                            $"✅ Kết nối database thành công!\n\n" +
                            $"Server: {conn.ServerVersion}\n" +
                            $"Database: {conn.Database}\n" +
                            $"Host: localhost",
                            "Kết nối thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        return true;
                    }
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                string errorMessage = "";

                // Phân loại lỗi MySQL
                switch (ex.Number)
                {
                    case 0:
                        errorMessage = "❌ Không thể kết nối đến MySQL Server!\n\n" +
                                     "Vui lòng kiểm tra:\n" +
                                     "1. MySQL Server đang chạy\n" +
                                     "2. Port 3306 không bị chặn";
                        break;
                    case 1042:
                        errorMessage = "❌ Không thể kết nối đến host 'localhost'!\n\n" +
                                     "Vui lòng kiểm tra MySQL Server đang chạy.";
                        break;
                    case 1045:
                        errorMessage = "❌ Sai thông tin đăng nhập!\n\n" +
                                     "Vui lòng kiểm tra:\n" +
                                     "- Username: root\n" +
                                     "- Password: (trống)";
                        break;
                    case 1049:
                        errorMessage = "❌ Database 'QuanLyHocSinh' không tồn tại!\n\n" +
                                     "Vui lòng tạo database trước:\n" +
                                     "CREATE DATABASE QuanLyHocSinh;";
                        break;
                    default:
                        errorMessage = $"❌ Lỗi MySQL (Code {ex.Number}):\n\n{ex.Message}";
                        break;
                }

                MessageBox.Show(
                    errorMessage,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi không xác định:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra và liệt kê các bảng trong database
        /// </summary>
        public static void CheckDatabaseStructure()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    // Lấy danh sách bảng
                    string query = "SHOW TABLES";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            StringBuilder tables = new StringBuilder();
                            tables.AppendLine("📋 Danh sách bảng trong database:\n");

                            int count = 0;
                            while (reader.Read())
                            {
                                count++;
                                tables.AppendLine($"   {count}. {reader[0]}");
                            }

                            if (count == 0)
                            {
                                tables.AppendLine("   ⚠️ Chưa có bảng nào!");
                            }

                            MessageBox.Show(
                                tables.ToString(),
                                "Cấu trúc Database",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi khi kiểm tra cấu trúc:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Đóng kết nối database
        /// </summary>
        public static void CloseConnection(MySqlConnection conn)
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    // Debug.WriteLine("✅ Đã đóng kết nối database"); // Commented to reduce console spam
                }
            }
            catch (Exception ex)
            {
                throw new Exception("L�i khi đóng kết nối: " + ex.Message);
            }
        }
    }
}