using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class HanhKiemDAO
    {
        // === 1. Thêm Hạnh Kiểm (CRUD) ===
        public bool ThemHanhKiem(HanhKiemDTO hk) // Dùng DTO
        {
            string query = @"INSERT INTO HanhKiem (MaHocSinh, MaHocKy, XepLoai, NhanXet) 
                             VALUES (@MaHS, @MaHK, @XL, @NX)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHS", hk.MaHocSinh); // Đã là int
                    cmd.Parameters.AddWithValue("@MaHK", hk.MaHocKy);
                    cmd.Parameters.AddWithValue("@XL", hk.XepLoai);
                    cmd.Parameters.AddWithValue("@NX", hk.NhanXet);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // === 2. Đọc Danh Sách Hạnh Kiểm ĐẦY ĐỦ ===
        public List<HanhKiemDTO> DocDSHanhKiemDayDu() // Dùng DTO
        {
            List<HanhKiemDTO> ds = new List<HanhKiemDTO>();
            string query = @"
                SELECT 
                    HK.*, HS.HoTen AS HoTenHocSinh, KY.TenHocKy, KY.MaNamHoc 
                FROM HanhKiem HK
                JOIN HocSinh HS ON HK.MaHocSinh = HS.MaHocSinh
                JOIN HocKy KY ON HK.MaHocKy = KY.MaHocKy
                ORDER BY HS.MaHocSinh, KY.MaHocKy";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HanhKiemDTO hk = new HanhKiemDTO();
                            hk.MaHocSinh = reader.GetInt32("MaHocSinh"); // Sửa: Đọc là int
                            hk.MaHocKy = reader.GetInt32("MaHocKy");
                            hk.XepLoai = reader.GetString("XepLoai");
                            hk.NhanXet = reader.IsDBNull(reader.GetOrdinal("NhanXet")) ? "" : reader.GetString("NhanXet");
                            ds.Add(hk);
                        }
                    }
                }
            }
            return ds;
        }

        // === 3. Cập nhật Hạnh Kiểm (CRUD) ===
        public bool CapNhatHanhKiem(HanhKiemDTO hk) // Dùng DTO
        {
            string query = @"UPDATE HanhKiem SET XepLoai=@XL, NhanXet=@NX 
                             WHERE MaHocSinh=@MaHS AND MaHocKy=@MaHK";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHS", hk.MaHocSinh); // Đã là int
                    cmd.Parameters.AddWithValue("@MaHK", hk.MaHocKy);
                    cmd.Parameters.AddWithValue("@XL", hk.XepLoai);
                    cmd.Parameters.AddWithValue("@NX", hk.NhanXet);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // === 4. Xóa Hạnh Kiểm (CRUD) ===
        public bool XoaHanhKiem(int maHocSinh, int maHocKy) // Sửa: string -> int
        {
            string query = "DELETE FROM HanhKiem WHERE MaHocSinh = @MaHS AND MaHocKy=@MaHK";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHS", maHocSinh); // Đã là int
                    cmd.Parameters.AddWithValue("@MaHK", maHocKy);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // === 5. Lấy Hạnh Kiểm theo Key (Quan trọng) ===
        public HanhKiemDTO LayHanhKiemTheoKey(int maHocSinh, int maHocKy) // Sửa: string -> int
        {
            HanhKiemDTO hanhKiem = null;
            string query = "SELECT MaHocSinh, MaHocKy, XepLoai, NhanXet FROM HanhKiem WHERE MaHocSinh = @MaHS AND MaHocKy = @MaHK";

            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                if (conn == null)
                {
                    Console.WriteLine("Lỗi LayHanhKiemTheoKey: Không thể tạo connection");
                    return null;
                }

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHS", maHocSinh); // Đã là int
                cmd.Parameters.AddWithValue("@MaHK", maHocKy);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hanhKiem = new HanhKiemDTO
                    {
                        MaHocSinh = reader.GetInt32("MaHocSinh"), // Sửa: Đọc là int
                        MaHocKy = Convert.ToInt32(reader["MaHocKy"]),
                        XepLoai = reader["XepLoai"].ToString(),
                        NhanXet = reader.IsDBNull(reader.GetOrdinal("NhanXet")) ? "" : reader.GetString("NhanXet")
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayHanhKiemTheoKey: {ex.Message}");
            }
            finally
            {
                if (conn != null)
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return hanhKiem;
        }
    }
}