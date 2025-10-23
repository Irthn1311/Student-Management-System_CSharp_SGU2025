using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO; // Giả định có HanhKiem DTO
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class HanhKiemDAO
    {
        // === 1. Thêm Hạnh Kiểm (CRUD) ===
        public bool ThemHanhKiem(HanhKiem hk)
        {
            string query = @"INSERT INTO HanhKiem (MaHocSinh, MaHocKy, XepLoai, NhanXet) 
                             VALUES (@MaHS, @MaHK, @XL, @NX)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHS", hk.MaHocSinh);
                    cmd.Parameters.AddWithValue("@MaHK", hk.MaHocKy);
                    cmd.Parameters.AddWithValue("@XL", hk.XepLoai);
                    cmd.Parameters.AddWithValue("@NX", hk.NhanXet);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // === 2. Đọc Danh Sách Hạnh Kiểm ĐẦY ĐỦ (Hiển thị + JOIN Học sinh, Học kỳ) ===
        public List<HanhKiem> DocDSHanhKiemDayDu()
        {
            List<HanhKiem> ds = new List<HanhKiem>();
            string query = @"
                SELECT 
                    HK.*, HS.HoTen AS HoTenHocSinh, KY.TenHocKy 
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
                            HanhKiem hk = new HanhKiem();
                            hk.MaHocSinh = reader.GetString("MaHocSinh");
                            hk.MaHocKy = reader.GetInt32("MaHocKy");
                            hk.XepLoai = reader.GetString("XepLoai");
                            hk.NhanXet = reader.IsDBNull(reader.GetOrdinal("NhanXet")) ? "" : reader.GetString("NhanXet");

                            // Giả định: Các trường sau được thêm vào HanhKiem DTO cho mục đích hiển thị
                            // hk.HoTenHocSinh = reader.GetString("HoTenHocSinh"); 
                            // hk.TenHocKy = reader.GetString("TenHocKy");
                            ds.Add(hk);
                        }
                    }
                }
            }
            return ds;
        }

        // === 3. Cập nhật Hạnh Kiểm (CRUD) ===
        public bool CapNhatHanhKiem(HanhKiem hk)
        {
            // Key là MaHocSinh và MaHocKy
            string query = @"UPDATE HanhKiem SET XepLoai=@XL, NhanXet=@NX 
                             WHERE MaHocSinh=@MaHS AND MaHocKy=@MaHK";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHS", hk.MaHocSinh);
                    cmd.Parameters.AddWithValue("@MaHK", hk.MaHocKy);
                    cmd.Parameters.AddWithValue("@XL", hk.XepLoai);
                    cmd.Parameters.AddWithValue("@NX", hk.NhanXet);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // === 4. Xóa Hạnh Kiểm (CRUD) ===
        public bool XoaHanhKiem(string maHocSinh, int maHocKy)
        {
            string query = "DELETE FROM HanhKiem WHERE MaHocSinh = @MaHS AND MaHocKy=@MaHK";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHS", maHocSinh);
                    cmd.Parameters.AddWithValue("@MaHK", maHocKy);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // === 5. Lấy Hạnh Kiểm theo Key (Phục vụ cho kiểm tra) ===
        public HanhKiem LayHanhKiemTheoKey(string maHocSinh, int maHocKy)
        {
            string query = "SELECT * FROM HanhKiem WHERE MaHocSinh = @MaHS AND MaHocKy=@MaHK";
            // Logic tương tự LayLopTheoId/LayLopTheoTen, chỉ cần đọc 1 bản ghi
            return null; // Giả lập trả về null nếu không tìm thấy
        }
    }
}