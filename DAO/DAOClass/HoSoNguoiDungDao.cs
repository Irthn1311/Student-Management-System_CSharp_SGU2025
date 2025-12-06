using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Data;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    /// <summary>
    /// DAO cho bảng HoSoNguoiDung
    /// </summary>
    public class HoSoNguoiDungDAO
    {
        /// <summary>
        /// Lấy hồ sơ người dùng theo tên đăng nhập
        /// </summary>
        public HoSoNguoiDungDTO GetHoSoByTenDangNhap(string tenDangNhap)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"SELECT MaHoSo, TenDangNhap, HoTen, Email, SoDienThoai, 
                               NgaySinh, GioiTinh, DiaChi, LoaiDoiTuong
                        FROM HoSoNguoiDung 
                        WHERE TenDangNhap = @TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new HoSoNguoiDungDTO
                            {
                                MaHoSo = reader.GetInt32("MaHoSo"),
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                HoTen = reader["HoTen"] != DBNull.Value ? reader["HoTen"].ToString() : null,
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                SoDienThoai = reader["SoDienThoai"] != DBNull.Value ? reader["SoDienThoai"].ToString() : null,
                                NgaySinh = reader["NgaySinh"] != DBNull.Value ? (DateTime?)reader["NgaySinh"] : null,
                                GioiTinh = reader["GioiTinh"] != DBNull.Value ? reader["GioiTinh"].ToString() : null,
                                DiaChi = reader["DiaChi"] != DBNull.Value ? reader["DiaChi"].ToString() : null,
                                LoaiDoiTuong = reader["LoaiDoiTuong"] != DBNull.Value ? reader["LoaiDoiTuong"].ToString() : null
                            };
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] HoSoNguoiDungDAO.GetHoSoByTenDangNhap: {ex.Message}");
                return null;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Cập nhật hồ sơ người dùng
        /// </summary>
        public bool UpdateHoSo(HoSoNguoiDungDTO hoSo)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"UPDATE HoSoNguoiDung 
                        SET Email = @Email, 
                            SoDienThoai = @SoDienThoai, 
                            DiaChi = @DiaChi
                        WHERE TenDangNhap = @TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", hoSo.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SoDienThoai", hoSo.SoDienThoai ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiaChi", hoSo.DiaChi ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TenDangNhap", hoSo.TenDangNhap);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] HoSoNguoiDungDAO.UpdateHoSo: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Kiểm tra email đã tồn tại chưa (loại trừ chính người dùng đang cập nhật)
        /// </summary>
        public bool KiemTraEmailTonTai(string email, string tenDangNhapLoaiTru)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"SELECT COUNT(*) FROM HoSoNguoiDung 
                        WHERE Email = @Email AND TenDangNhap != @TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email.Trim());
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhapLoaiTru);

                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] HoSoNguoiDungDAO.KiemTraEmailTonTai: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}