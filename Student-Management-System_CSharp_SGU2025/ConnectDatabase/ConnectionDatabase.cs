using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.ConnectDatabase
{
    internal class ConnectionDatabase
    {
        private static string connectionString = "Server=localhost;Database=QuanLyHocSinh;Uid=root;Pwd=;";

        /// <summary>
        /// Lấy kết nối đến cơ sở dữ liệu MySQL
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                return connection;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Lỗi kết nối database: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiểm tra kết nối database
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    Console.WriteLine("Kết nối thành công!");
                    return conn.State == System.Data.ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Đóng kết nối database
        /// </summary>
        public static void CloseConnection(MySqlConnection conn)
        {
            try
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đóng kết nối: " + ex.Message);
            }
        }   }
}