using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO; // Cần DTO nếu dùng, ở đây dùng Tuple
using System;
using System.Collections.Generic;
using System.Data; // Cần cho ConnectionState

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class LoginDAO
    {
        /// <summary>
        /// Kiểm tra xem Tài Khoản đăng nhập có hợp lệ (đúng mật khẩu và trạng thái) không.
        /// </summary>
        public bool KiemTraTaiKhoanCoDangNhapDuocKhong(String tenDangNhap, String matKhau, String trangThai = "Hoạt động")
        {
            string sql = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @tenDangNhap AND MatKhau = @matKhau AND TrangThai = @trangThai";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@matKhau", matKhau);
                        cmd.Parameters.AddWithValue("@trangThai", trangThai);

                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi khi kiểm tra đăng nhập: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng từ database.
        /// </summary>
        public List<(String tenDangNhap, String matKhau, String trangThai)> LayDanhSachNguoiDung()
        {
            List<(String tenDangNhap, String matKhau, String trangThai)> dsNguoiDung = new List<(String tenDangNhap, String matKhau, String trangThai)>();
            string sql = "SELECT TenDangNhap, MatKhau, TrangThai FROM NguoiDung";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            (string tenDangNhap, string matKhau, string trangThai) nguoiDung =
                            (
                                reader.GetString("TenDangNhap"),
                                reader.GetString("MatKhau"), // Cảnh báo bảo mật: Không nên lấy mật khẩu
                                reader.GetString("TrangThai")
                            );
                            dsNguoiDung.Add(nguoiDung);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy danh sách người dùng: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return dsNguoiDung;
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một người dùng theo Tên đăng nhập.
        /// </summary>
        /// <returns>Một tuple chứa thông tin, hoặc null nếu không tìm thấy.</returns>
        public (string tenDangNhap, string matKhau, string trangThai)? LayNguoiDungTheoTen(string tenDangNhap)
        {
            string sql = "SELECT TenDangNhap, MatKhau, TrangThai FROM NguoiDung WHERE TenDangNhap = @tenDangNhap";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return (
                                    reader.GetString("TenDangNhap"),
                                    reader.GetString("MatKhau"),
                                    reader.GetString("TrangThai")
                                );
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy người dùng theo tên: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return null; // Không tìm thấy
        }

        /// <summary>
        /// Kiểm tra xem Tên đăng nhập đã tồn tại trong CSDL chưa.
        /// </summary>
        public bool KiemTraNguoiDungTonTai(string tenDangNhap)
        {
            string sql = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @tenDangNhap";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi kiểm tra người dùng tồn tại: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Thêm một người dùng mới vào CSDL.
        /// </summary>
        public bool ThemNguoiDung(string tenDangNhap, string matKhau, string trangThai)
        {
            string sql = "INSERT INTO NguoiDung (TenDangNhap, MatKhau, TrangThai) VALUES (@tenDangNhap, @matKhau, @trangThai)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@matKhau", matKhau);
                        cmd.Parameters.AddWithValue("@trangThai", trangThai);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi thêm người dùng: " + ex.Message);
                    throw; // Ném lỗi (ví dụ: trùng khóa chính) để BLL xử lý
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Cập nhật Mật khẩu và Trạng thái của người dùng.
        /// </summary>
        public bool CapNhatNguoiDung(string tenDangNhap, string matKhau, string trangThai)
        {
            string sql = "UPDATE NguoiDung SET MatKhau = @matKhau, TrangThai = @trangThai WHERE TenDangNhap = @tenDangNhap";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@matKhau", matKhau);
                        cmd.Parameters.AddWithValue("@trangThai", trangThai);
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi cập nhật người dùng: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Xóa người dùng và các tham chiếu liên quan (dùng Transaction).
        /// </summary>
        public bool XoaNguoiDung(string tenDangNhap)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                MySqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    MySqlCommand cmd;

                    // 1. Xóa khỏi NguoiDungVaiTro
                    cmd = new MySqlCommand("DELETE FROM NguoiDungVaiTro WHERE TenDangNhap = @tenDangNhap", conn, transaction);
                    cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                    cmd.ExecuteNonQuery();

                    // 2. Xóa khỏi KhenThuongKyLuat (nếu có)
                    cmd = new MySqlCommand("DELETE FROM KhenThuongKyLuat WHERE NguoiLapID = @tenDangNhap", conn, transaction);
                    cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                    cmd.ExecuteNonQuery();

                    // 3. Xóa khỏi ThongBao (nếu có)
                    cmd = new MySqlCommand("DELETE FROM ThongBao WHERE MaNguoiTao = @tenDangNhap", conn, transaction);
                    cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                    cmd.ExecuteNonQuery();

                    // 4. Xóa khỏi NguoiDung
                    cmd = new MySqlCommand("DELETE FROM NguoiDung WHERE TenDangNhap = @tenDangNhap", conn, transaction);
                    cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    transaction.Commit();
                    return rowsAffected > 0;
                }
                catch (MySqlException ex)
                {
                    transaction?.Rollback(); // Hoàn tác nếu có lỗi
                    Console.WriteLine("Lỗi xóa người dùng (Transaction): " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Tìm kiếm người dùng theo Tên đăng nhập.
        /// </summary>
        public List<(String tenDangNhap, String matKhau, String trangThai)> TimKiemNguoiDung(string tuKhoa)
        {
            List<(String tenDangNhap, String matKhau, String trangThai)> dsNguoiDung = new List<(String tenDangNhap, String matKhau, String trangThai)>();
            string sql = "SELECT TenDangNhap, MatKhau, TrangThai FROM NguoiDung WHERE TenDangNhap LIKE @tuKhoa";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tuKhoa", $"%{tuKhoa}%"); // Thêm % để tìm kiếm gần đúng
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                (string tenDangNhap, string matKhau, string trangThai) nguoiDung =
                                (
                                    reader.GetString("TenDangNhap"),
                                    reader.GetString("MatKhau"),
                                    reader.GetString("TrangThai")
                                );
                                dsNguoiDung.Add(nguoiDung);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi tìm kiếm người dùng: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return dsNguoiDung;
        }
    }
}