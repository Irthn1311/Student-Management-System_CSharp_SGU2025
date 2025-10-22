using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO; // Giả định có GiaoVien DTO
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class GiaoVienDAO
    {
        // === 1. Thêm Giáo Viên (CRUD) ===
        public bool ThemGiaoVien(GiaoVien gv)
        {
            string query = @"INSERT INTO GiaoVien (MaGiaoVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, TrangThai) 
                             VALUES (@MaGV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Email, @TrangThai)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", gv.MaGiaoVien);
                    cmd.Parameters.AddWithValue("@HoTen", gv.HoTen);
                    cmd.Parameters.AddWithValue("@NgaySinh", gv.NgaySinh);
                    cmd.Parameters.AddWithValue("@GioiTinh", gv.GioiTinh);
                    cmd.Parameters.AddWithValue("@DiaChi", gv.DiaChi);
                    cmd.Parameters.AddWithValue("@SDT", gv.SoDienThoai);
                    cmd.Parameters.AddWithValue("@Email", gv.Email);
                    cmd.Parameters.AddWithValue("@TrangThai", gv.TrangThai);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // === 2. Đọc Danh Sách Giáo Viên ĐẦY ĐỦ (Hiển thị + JOIN Chuyên môn) ===
        public List<GiaoVien> DocDSGiaoVienDayDu()
        {
            List<GiaoVien> ds = new List<GiaoVien>();
            // Sử dụng GROUP_CONCAT để JOIN và gom nhóm tên các môn chuyên môn
            string query = @"
                SELECT 
                    GV.*, 
                    GROUP_CONCAT(MH.TenMonHoc ORDER BY GCM.LaChuyenMonChinh DESC SEPARATOR ', ') AS DanhSachChuyenMon
                FROM GiaoVien GV
                LEFT JOIN GiaoVienChuyenMon GCM ON GV.MaGiaoVien = GCM.MaGiaoVien
                LEFT JOIN MonHoc MH ON GCM.MaMonHoc = MH.MaMonHoc
                GROUP BY GV.MaGiaoVien
                ORDER BY GV.MaGiaoVien";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GiaoVien gv = new GiaoVien();
                            gv.MaGiaoVien = reader.GetString("MaGiaoVien");
                            gv.HoTen = reader.GetString("HoTen");
                            gv.NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? DateTime.MinValue : reader.GetDateTime("NgaySinh");
                            gv.GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? "" : reader.GetString("GioiTinh");
                            gv.DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? "" : reader.GetString("DiaChi");
                            gv.SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? "" : reader.GetString("SoDienThoai");
                            gv.Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString("Email");
                            gv.TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? "" : reader.GetString("TrangThai");

                            //// Lấy thông tin JOIN (Danh sách chuyên môn)
                            //gv.MaGiaoVien = reader.IsDBNull(reader.GetOrdinal("DanhSachChuyenMon")) ? "" : reader.GetString("DanhSachChuyenMon");
                            ds.Add(gv);
                        }
                    }
                }
            }
            return ds;
        }

        // === 3. Cập nhật Giáo Viên (CRUD) ===
        public bool CapNhatGiaoVien(GiaoVien gv)
        {
            string query = @"UPDATE GiaoVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, 
                             DiaChi=@DiaChi, SoDienThoai=@SDT, Email=@Email, TrangThai=@TrangThai 
                             WHERE MaGiaoVien=@MaGV";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", gv.MaGiaoVien);
                    cmd.Parameters.AddWithValue("@HoTen", gv.HoTen);
                    cmd.Parameters.AddWithValue("@NgaySinh", gv.NgaySinh);
                    cmd.Parameters.AddWithValue("@GioiTinh", gv.GioiTinh);
                    cmd.Parameters.AddWithValue("@DiaChi", gv.DiaChi);
                    cmd.Parameters.AddWithValue("@SDT", gv.SoDienThoai);
                    cmd.Parameters.AddWithValue("@Email", gv.Email);
                    cmd.Parameters.AddWithValue("@TrangThai", gv.TrangThai);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // === 4. Xóa Giáo Viên (CRUD) ===
        public bool XoaGiaoVien(string maGiaoVien)
        {
            string query = "DELETE FROM GiaoVien WHERE MaGiaoVien = @MaGV";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", maGiaoVien);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // === 5. Lấy Giáo Viên theo Mã (Phục vụ cho kiểm tra/cập nhật) ===
        public GiaoVien LayGiaoVienTheoMa(string maGiaoVien)
        {
            string query = "SELECT * FROM GiaoVien WHERE MaGiaoVien = @MaGV";
            // Logic tương tự LayLopTheoId/LayLopTheoTen, chỉ cần đọc 1 bản ghi
            return null; // Giả lập trả về null nếu không tìm thấy
        }

        // === 6. Xử lý Chuyên Môn: Thêm ===
        public bool ThemChuyenMon(string maGiaoVien, int maMonHoc, bool laChuyenMonChinh)
        {
            string query = "INSERT INTO GiaoVienChuyenMon (MaGiaoVien, MaMonHoc, LaChuyenMonChinh) VALUES (@MaGV, @MaMH, @LaChinh)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", maGiaoVien);
                    cmd.Parameters.AddWithValue("@MaMH", maMonHoc);
                    cmd.Parameters.AddWithValue("@LaChinh", laChuyenMonChinh);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // === 7. Xử lý Chuyên Môn: Xóa tất cả chuyên môn theo GV (Phục vụ cho Cập nhật) ===
        public bool XoaTatCaChuyenMonTheoGV(string maGiaoVien)
        {
            string query = "DELETE FROM GiaoVienChuyenMon WHERE MaGiaoVien = @MaGV";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", maGiaoVien);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}