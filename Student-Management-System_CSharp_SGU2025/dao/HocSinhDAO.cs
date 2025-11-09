using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase; // Namespace chứa ConnectionDatabase
using Student_Management_System_CSharp_SGU2025.DTO;          // Namespace chứa HocSinhDTO
using System;
using System.Collections.Generic;
using System.Data; // Cần thiết cho CommandType (mặc dù không dùng trực tiếp ở đây)

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class HocSinhDAO
    {
        /// <summary>
        /// Kiểm tra xem Mã Học Sinh đã tồn tại trong database chưa.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh cần kiểm tra.</param>
        /// <returns>True nếu tồn tại, False nếu không.</returns>
        public bool KiemTraTonTai(int maHocSinh)
        {
            string sql = "SELECT COUNT(*) FROM HocSinh WHERE MaHocSinh = @maHS";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        // ExecuteScalar trả về giá trị ở ô đầu tiên, dòng đầu tiên (ở đây là COUNT)
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi kiểm tra tồn tại học sinh: " + ex.Message);
                    // Có thể throw lại lỗi hoặc trả về false tùy logic xử lý
                    throw; // Ném lại lỗi để lớp gọi xử lý
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn); // Đảm bảo kết nối được đóng
                }
            }
        }

        /// <summary>
        /// Kiểm tra SĐT đã tồn tại chưa (có thể loại trừ 1 mã HS).
        /// </summary>
        public bool KiemTraTrungSdt(string sdt, int maHocSinhToExclude = 0)
        {
            // Không kiểm tra nếu SĐT rỗng
            if (string.IsNullOrWhiteSpace(sdt)) return false;

            string sql = "SELECT COUNT(*) FROM HocSinh WHERE SDTHS = @sdt";
            if (maHocSinhToExclude > 0)
            {
                sql += " AND MaHocSinh != @maHS"; // Loại trừ chính học sinh này
            }

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@sdt", sdt.Trim());
                        if (maHocSinhToExclude > 0)
                        {
                            cmd.Parameters.AddWithValue("@maHS", maHocSinhToExclude);
                        }
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi kiểm tra trùng SĐT học sinh: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Kiểm tra Email đã tồn tại chưa (có thể loại trừ 1 mã HS).
        /// </summary>
        public bool KiemTraTrungEmail(string email, int maHocSinhToExclude = 0)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            string sql = "SELECT COUNT(*) FROM HocSinh WHERE Email = @email";
            if (maHocSinhToExclude > 0)
            {
                sql += " AND MaHocSinh != @maHS"; // Loại trừ chính học sinh này
            }

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email.Trim());
                        if (maHocSinhToExclude > 0)
                        {
                            cmd.Parameters.AddWithValue("@maHS", maHocSinhToExclude);
                        }
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi kiểm tra trùng Email học sinh: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả học sinh từ database.
        /// </summary>
        /// <returns>Danh sách các đối tượng HocSinhDTO.</returns>
        public List<HocSinhDTO> LayDanhSachHocSinh()
        {
            List<HocSinhDTO> dsHocSinh = new List<HocSinhDTO>();
            string sql = "SELECT * FROM HocSinh";
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
                            // Tạo DTO từ dữ liệu đọc được
                            HocSinhDTO hs = new HocSinhDTO(
                                reader.GetInt32("MaHocSinh"),
                                reader.GetString("HoTen"),
                                reader.GetDateTime("NgaySinh"),
                                reader.GetString("GioiTinh"),
                                reader.IsDBNull(reader.GetOrdinal("SDTHS")) ? null : reader.GetString("SDTHS"), // Xử lý NULL
                                reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),    // Xử lý NULL
                                reader.GetString("TrangThai")
                            );
                            dsHocSinh.Add(hs);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy danh sách học sinh: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return dsHocSinh;
        }

        /// <summary>
        /// Đếm tổng số tất cả học sinh từ database.
        /// </summary>
        /// <returns>Tổng số lượng học sinh tại trường</returns>
        public int DemTongSoLuongHocSinh()
        {
            int count = 0;
            string sql = "SELECT COUNT(*) FROM HocSinh";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đếm số lượng học sinh: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return count;
        }

        /// <summary>
        /// Đếm tổng số tất cả học sinh nam từ database.
        /// </summary>
        /// <returns>Tổng số lượng học sinh nam đang học tại trường</returns>
        public int DemTongSoLuongHocSinhNam()
        {
            int count = 0;
            string sql = "SELECT COUNT(*) FROM HocSinh WHERE GioiTinh = 'Nam'";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đếm số lượng học sinh nam: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return count;
        }

        /// <summary>
        /// Đếm tổng số tất cả học sinh nữ từ database.
        /// </summary>
        /// <returns>Tổng số lượng học sinh nữ đang học tại trường</returns>
        public int DemTongSoLuongHocSinhNu()
        {
            int count = 0;
            string sql = "SELECT COUNT(*) FROM HocSinh WHERE GioiTinh = 'Nữ'";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đếm số lượng học sinh nữ: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return count;
        }

        /// <summary>
        /// Đếm tổng số tất cả học sinh có trạng thái "Đang học" từ database.
        /// </summary>
        /// <returns>Tổng số lượng học sinh có trạng thái "Đang học" tại trường</returns>
        
        public int DemTongSoLuongHocSinhDangHoc()
        {
            int count = 0;
            string sql = "SELECT COUNT(*) FROM HocSinh WHERE TrangThai = 'Đang học'";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đếm số lượng học sinh đang học: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return count;
        }



        /// <summary>
        /// Thêm một học sinh mới vào database và trả về ID của học sinh đó.
        /// </summary>
        /// <param name="hs">Đối tượng HocSinhDTO chứa thông tin cần thêm.</param>
        /// <returns>ID (MaHocSinh) của học sinh vừa thêm, hoặc -1 nếu thất bại.</returns>
        public int ThemHocSinh(HocSinhDTO hs) 
        {
            // Thêm "; SELECT LAST_INSERT_ID()" vào cuối câu lệnh INSERT
            string sql = "INSERT INTO HocSinh (HoTen, NgaySinh, GioiTinh, SDTHS, Email, TrangThai) VALUES (@hoTen, @ngaySinh, @gioiTinh, @sdtHS, @email, @trangThai); SELECT LAST_INSERT_ID();";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                MySqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@hoTen", hs.HoTen);
                        cmd.Parameters.AddWithValue("@ngaySinh", hs.NgaySinh);
                        cmd.Parameters.AddWithValue("@gioiTinh", hs.GioiTinh);
                        cmd.Parameters.AddWithValue("@sdtHS", (object)hs.SdtHS ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@email", (object)hs.Email ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@trangThai", hs.TrangThai);

                        // Dùng ExecuteScalar để lấy giá trị ID trả về từ LAST_INSERT_ID()
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            transaction.Commit();
                            return Convert.ToInt32(result); // Trả về ID mới
                        }
                        else
                        {
                            // Trường hợp ExecuteScalar không trả về gì (lỗi không mong muốn)
                            transaction.Rollback();
                            Console.WriteLine("Lỗi thêm học sinh: Không lấy được LAST_INSERT_ID.");
                            return -1; // Trả về -1 báo lỗi
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi thêm học sinh: " + ex.Message);
                    transaction?.Rollback();
                    return -1; // Trả về -1 nếu có lỗi SQL
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Xóa một học sinh khỏi database dựa vào Mã Học Sinh.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh cần xóa.</param>
        /// <returns>True nếu xóa thành công, False nếu thất bại.</returns>
        public bool XoaHocSinh(int maHocSinh)
        {
            string sql = "DELETE FROM HocSinh WHERE MaHocSinh = @maHS";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi xóa học sinh: " + ex.Message);
                    // throw;
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin một học sinh trong database.
        /// </summary>
        /// <param name="hs">Đối tượng HocSinhDTO chứa thông tin cập nhật.</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại.</returns>
        public bool CapNhatHocSinh(HocSinhDTO hs)
        {
            string sql = @"UPDATE HocSinh SET
                           HoTen = @hoTen,
                           NgaySinh = @ngaySinh,
                           GioiTinh = @gioiTinh,
                           SDTHS = @sdtHS,
                           Email = @email,
                           TrangThai = @trangThai
                         WHERE MaHocSinh = @maHS";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@hoTen", hs.HoTen);
                        cmd.Parameters.AddWithValue("@ngaySinh", hs.NgaySinh);
                        cmd.Parameters.AddWithValue("@gioiTinh", hs.GioiTinh);
                        cmd.Parameters.AddWithValue("@sdtHS", (object)hs.SdtHS ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@email", (object)hs.Email ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@trangThai", hs.TrangThai);
                        cmd.Parameters.AddWithValue("@maHS", hs.MaHS); // Điều kiện WHERE

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi cập nhật học sinh: " + ex.Message);
                    // throw;
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Tìm kiếm một học sinh theo Mã Học Sinh.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh cần tìm.</param>
        /// <returns>Đối tượng HocSinhDTO nếu tìm thấy, null nếu không.</returns>
        public HocSinhDTO TimHocSinhTheoMa(int maHocSinh)
        {
            string sql = "SELECT * FROM HocSinh WHERE MaHocSinh = @maHS";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                HocSinhDTO hs = new HocSinhDTO(
                                    reader.GetInt32("MaHocSinh"),
                                    reader.GetString("HoTen"),
                                    reader.GetDateTime("NgaySinh"),
                                    reader.GetString("GioiTinh"),
                                    reader.IsDBNull(reader.GetOrdinal("SDTHS")) ? null : reader.GetString("SDTHS"),
                                    reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                    reader.GetString("TrangThai")
                                );
                                
                                // ✅ Đọc thêm TenDangNhap từ database
                                if (!reader.IsDBNull(reader.GetOrdinal("TenDangNhap")))
                                {
                                    hs.TenDangNhap = reader.GetString("TenDangNhap");
                                }
                                
                                return hs;
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi tìm học sinh theo mã: " + ex.Message);
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
        /// Cập nhật trạng thái cho một học sinh.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh cần cập nhật.</param>
        /// <param name="trangThaiMoi">Trạng thái mới ('Đang học' hoặc 'Nghỉ học').</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại.</returns>
        public bool CapNhatTrangThaiHocSinh(int maHocSinh, string trangThaiMoi)
        {
            string sql = "UPDATE HocSinh SET TrangThai = @trangThai WHERE MaHocSinh = @maHS";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@trangThai", trangThaiMoi);
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi cập nhật trạng thái học sinh: " + ex.Message);
                    // throw;
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Cập nhật TenDangNhap cho học sinh (liên kết với NguoiDung).
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh cần cập nhật.</param>
        /// <param name="tenDangNhap">Tên đăng nhập mới.</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại.</returns>
        public bool CapNhatTenDangNhap(int maHocSinh, string tenDangNhap)
        {
            string sql = "UPDATE HocSinh SET TenDangNhap = @tenDangNhap WHERE MaHocSinh = @maHS";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi cập nhật TenDangNhap học sinh: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        // --- Bạn có thể thêm các hàm tìm kiếm khác ở đây ---
        // Ví dụ: Tìm theo tên, tìm theo lớp (cần JOIN), ...
        // public List<HocSinhDTO> TimHocSinhTheoTen(string ten) { ... }
        // public List<HocSinhDTO> LayDanhSachHocSinhTheoLop(int maLop, int maHocKy) { ... } // Cần JOIN với PhanLop
    }
}