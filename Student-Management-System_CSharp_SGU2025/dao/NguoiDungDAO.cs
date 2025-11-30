using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    /// <summary>
    /// DAO cho bảng NguoiDung (Tài khoản đăng nhập)
    /// </summary>
    public class NguoiDungDAO
    {
        /// <summary>
        /// Thêm người dùng mới vào database (KHÔNG CÓ CỘT VaiTro)
        /// </summary>
        public bool AddNguoiDung(NguoiDungDTO nguoiDung)
        {
            MySqlConnection conn = null;
            MySqlTransaction transaction = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                transaction = conn.BeginTransaction();

                // 1. Thêm vào bảng NguoiDung (KHÔNG CÓ cột VaiTro)
                string query = @"INSERT INTO NguoiDung (TenDangNhap, MatKhau, TrangThai) 
                                VALUES (@TenDangNhap, @MatKhau, @TrangThai)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", nguoiDung.MatKhau);
                    cmd.Parameters.AddWithValue("@TrangThai", nguoiDung.TrangThai ?? "Hoạt động");

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                // 2. Nếu có VaiTro, thêm vào bảng NguoiDungVaiTro
                if (!string.IsNullOrWhiteSpace(nguoiDung.VaiTro))
                {
                    // Lấy MaVaiTro từ tên vai trò (giả sử VaiTro = "HocSinh" -> MaVaiTro = "HS")
                    string maVaiTro = nguoiDung.VaiTro == "HocSinh" ? "HS" : 
                                     nguoiDung.VaiTro == "GiaoVien" ? "GV" : 
                                     nguoiDung.VaiTro == "GiaoVu" ? "GV_QL" : "HS";

                    string queryVaiTro = @"INSERT IGNORE INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) 
                                          VALUES (@TenDangNhap, @MaVaiTro)";

                    using (MySqlCommand cmd = new MySqlCommand(queryVaiTro, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                        cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                        cmd.ExecuteNonQuery(); // Không quan tâm kết quả vì dùng IGNORE
                    }
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine($"[ERROR] NguoiDungDAO.AddNguoiDung: {ex.Message}");
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
        /// Kiểm tra tên đăng nhập đã tồn tại chưa
        /// </summary>
        public bool CheckTenDangNhapExists(string tenDangNhap)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungDAO.CheckTenDangNhapExists: {ex.Message}");
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
        /// Lấy thông tin người dùng theo tên đăng nhập (JOIN với VaiTro)
        /// </summary>
        public NguoiDungDTO GetNguoiDungByTenDangNhap(string tenDangNhap)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                // ✅ LẤY CẢ MaVaiTro, TenVaiTro VÀ LanDangNhapCuoi
                string query = @"SELECT n.TenDangNhap, n.MatKhau, n.TrangThai, n.LanDangNhapCuoi,
                       GROUP_CONCAT(nv.MaVaiTro) as MaVaiTro,
                       GROUP_CONCAT(v.TenVaiTro SEPARATOR ', ') as TenVaiTro
                FROM NguoiDung n
                LEFT JOIN NguoiDungVaiTro nv ON n.TenDangNhap = nv.TenDangNhap
                LEFT JOIN VaiTro v ON nv.MaVaiTro = v.MaVaiTro
                WHERE n.TenDangNhap = @TenDangNhap
                GROUP BY n.TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new NguoiDungDTO
                            {
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString(),
                                MaVaiTro = reader["MaVaiTro"] != DBNull.Value ? reader["MaVaiTro"].ToString() : null,
                                VaiTro = reader["TenVaiTro"] != DBNull.Value ? reader["TenVaiTro"].ToString() : "Người dùng",
                                TrangThai = reader["TrangThai"] != DBNull.Value ? reader["TrangThai"].ToString() : "Hoạt động",
                                LanDangNhapCuoi = reader["LanDangNhapCuoi"] != DBNull.Value
                                    ? (DateTime?)reader["LanDangNhapCuoi"]
                                    : null
                            };
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungDAO.GetNguoiDungByTenDangNhap: {ex.Message}");
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
        /// Lấy tất cả người dùng (JOIN với VaiTro)
        /// </summary>
        public List<NguoiDungDTO> GetAllNguoiDung()
        {
            List<NguoiDungDTO> list = new List<NguoiDungDTO>();
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                // ✅ JOIN với VaiTro và lấy cả MaVaiTro + TenVaiTro
                string query = @"SELECT n.TenDangNhap, n.MatKhau, n.TrangThai, n.LanDangNhapCuoi,
                               GROUP_CONCAT(v.TenVaiTro SEPARATOR ', ') as TenVaiTro,
                               GROUP_CONCAT(nv.MaVaiTro SEPARATOR ',') as MaVaiTro
                        FROM NguoiDung n
                        LEFT JOIN NguoiDungVaiTro nv ON n.TenDangNhap = nv.TenDangNhap
                        LEFT JOIN VaiTro v ON nv.MaVaiTro = v.MaVaiTro
                        GROUP BY n.TenDangNhap
                        ORDER BY n.TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NguoiDungDTO nd = new NguoiDungDTO
                            {
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString(),
                                VaiTro = reader["TenVaiTro"] != DBNull.Value ? reader["TenVaiTro"].ToString() : "Chưa có vai trò",
                                MaVaiTro = reader["MaVaiTro"] != DBNull.Value ? reader["MaVaiTro"].ToString() : null, // ✅ MaVaiTro
                                TrangThai = reader["TrangThai"] != DBNull.Value ? reader["TrangThai"].ToString() : "Hoạt động",
                                LanDangNhapCuoi = reader["LanDangNhapCuoi"] != DBNull.Value
                                    ? (DateTime?)reader["LanDangNhapCuoi"]
                                    : null
                            };
                            list.Add(nd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungDAO.GetAllNguoiDung: {ex.Message}");
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return list;
        }

        /// <summary>
        /// Cập nhật mật khẩu
        /// </summary>
        public bool UpdateMatKhau(string tenDangNhap, string matKhauMoi)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "UPDATE NguoiDung SET MatKhau = @MatKhau WHERE TenDangNhap = @TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MatKhau", matKhauMoi);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungDAO.UpdateMatKhau: {ex.Message}");
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
        /// Xóa người dùng
        /// </summary>
        public bool DeleteNguoiDung(string tenDangNhap)
        {
            MySqlConnection conn = null;
            MySqlTransaction transaction = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                transaction = conn.BeginTransaction();

                // 1. Xóa NguoiDungVaiTro (xóa vai trò được gán)
                string query1 = "DELETE FROM NguoiDungVaiTro WHERE TenDangNhap = @TenDangNhap";
                using (MySqlCommand cmd = new MySqlCommand(query1, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.ExecuteNonQuery();
                }

                // 2. Xóa HoSoNguoiDung (xóa hồ sơ người dùng)
                string query2 = "DELETE FROM HoSoNguoiDung WHERE TenDangNhap = @TenDangNhap";
                using (MySqlCommand cmd = new MySqlCommand(query2, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.ExecuteNonQuery();
                }

                // 3. Xóa NguoiDung (xóa tài khoản chính)
                string query3 = "DELETE FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
                using (MySqlCommand cmd = new MySqlCommand(query3, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"[ERROR] NguoiDungDAO.DeleteNguoiDung: Không tìm thấy tài khoản '{tenDangNhap}'");
                        return false;
                    }
                }

                transaction.Commit();
                Console.WriteLine($"[SUCCESS] Đã xóa tài khoản '{tenDangNhap}' và tất cả dữ liệu liên quan");
                return true;
            }
            catch (MySqlException ex)
            {
                transaction?.Rollback();
                Console.WriteLine($"[ERROR] NguoiDungDAO.DeleteNguoiDung: {ex.Message}");
                Console.WriteLine($"[ERROR] MySQL Error Code: {ex.Number}");
                return false;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine($"[ERROR] NguoiDungDAO.DeleteNguoiDung: {ex.Message}");
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
        /// Thêm người dùng với vai trò và hồ sơ (Transaction)
        /// </summary>
        public bool ThemNguoiDungVoiVaiTroVaHoSo(NguoiDungDTO nguoiDung, string maVaiTro, HoSoNguoiDungDTO hoSo)
        {
            MySqlConnection conn = null;
            MySqlTransaction transaction = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                transaction = conn.BeginTransaction();

                // 1. Thêm vào bảng NguoiDung
                string query1 = @"INSERT INTO NguoiDung (TenDangNhap, MatKhau, TrangThai) 
                                VALUES (@TenDangNhap, @MatKhau, @TrangThai)";

                using (MySqlCommand cmd = new MySqlCommand(query1, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", nguoiDung.MatKhau);
                    cmd.Parameters.AddWithValue("@TrangThai", nguoiDung.TrangThai ?? "Hoạt động");

                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                // 2. Thêm vào bảng NguoiDungVaiTro
                if (!string.IsNullOrWhiteSpace(maVaiTro))
                {
                    string query2 = @"INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) 
                                     VALUES (@TenDangNhap, @MaVaiTro)";

                    using (MySqlCommand cmd = new MySqlCommand(query2, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                        cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                        cmd.ExecuteNonQuery();
                    }
                }

                // 3. Thêm vào bảng HoSoNguoiDung
                if (hoSo != null)
                {
                    string query3 = @"INSERT INTO HoSoNguoiDung 
                                    (TenDangNhap, HoTen, Email, SoDienThoai, NgaySinh, GioiTinh, DiaChi, LoaiDoiTuong) 
                                    VALUES 
                                    (@TenDangNhap, @HoTen, @Email, @SoDienThoai, @NgaySinh, @GioiTinh, @DiaChi, @LoaiDoiTuong)";

                    using (MySqlCommand cmd = new MySqlCommand(query3, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", hoSo.TenDangNhap);
                        cmd.Parameters.AddWithValue("@HoTen", hoSo.HoTen ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", hoSo.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoDienThoai", hoSo.SoDienThoai ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgaySinh", hoSo.NgaySinh ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", hoSo.GioiTinh ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiaChi", hoSo.DiaChi ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@LoaiDoiTuong", hoSo.LoaiDoiTuong ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine($"[ERROR] NguoiDungDAO.ThemNguoiDungVoiVaiTroVaHoSo: {ex.Message}");
                throw;
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
        /// ✅ Cập nhật thời gian đăng nhập cuối
        /// </summary>
        public bool UpdateLanDangNhapCuoi(string tenDangNhap)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "UPDATE NguoiDung SET LanDangNhapCuoi = NOW() WHERE TenDangNhap = @TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungDAO.UpdateLanDangNhapCuoi: {ex.Message}");
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
        /// ✅ Cập nhật vai trò cho người dùng
        /// </summary>
        public bool UpdateVaiTro(string tenDangNhap, string maVaiTroMoi)
        {
            MySqlConnection conn = null;
            MySqlTransaction transaction = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                transaction = conn.BeginTransaction();

                // 1. Xóa tất cả vai trò cũ
                string query1 = "DELETE FROM NguoiDungVaiTro WHERE TenDangNhap = @TenDangNhap";
                using (MySqlCommand cmd = new MySqlCommand(query1, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.ExecuteNonQuery();
                }

                // 2. Thêm vai trò mới
                string query2 = "INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) VALUES (@TenDangNhap, @MaVaiTro)";
                using (MySqlCommand cmd = new MySqlCommand(query2, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTroMoi);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine($"[ERROR] NguoiDungDAO.UpdateVaiTro: {ex.Message}");
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
        /// Cập nhật trạng thái tài khoản (Hoạt động/Bị khóa)
        /// </summary>
        public bool UpdateTrangThai(string tenDangNhap, string trangThaiMoi)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "UPDATE NguoiDung SET TrangThai = @TrangThai WHERE TenDangNhap = @TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungDAO.UpdateTrangThai: {ex.Message}");
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
        /// ✅ Cập nhật vai trò và đồng bộ LoaiDoiTuong trong HoSoNguoiDung
        /// </summary>
        public bool UpdateVaiTroVaLoaiDoiTuong(string tenDangNhap, string maVaiTroMoi)
        {
            MySqlConnection conn = null;
            MySqlTransaction transaction = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                transaction = conn.BeginTransaction();

                // 1. Xóa tất cả vai trò cũ
                string query1 = "DELETE FROM NguoiDungVaiTro WHERE TenDangNhap = @TenDangNhap";
                using (MySqlCommand cmd = new MySqlCommand(query1, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.ExecuteNonQuery();
                }

                // 2. Thêm vai trò mới
                string query2 = "INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) VALUES (@TenDangNhap, @MaVaiTro)";
                using (MySqlCommand cmd = new MySqlCommand(query2, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTroMoi);
                    cmd.ExecuteNonQuery();
                }

                // 3. ✅ Cập nhật LoaiDoiTuong trong HoSoNguoiDung (nếu tồn tại)
                string query3 = @"UPDATE HoSoNguoiDung 
                         SET LoaiDoiTuong = @LoaiDoiTuong 
                         WHERE TenDangNhap = @TenDangNhap";
                using (MySqlCommand cmd = new MySqlCommand(query3, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@LoaiDoiTuong", maVaiTroMoi);
                    cmd.ExecuteNonQuery(); // Không quan trọng nếu không có hồ sơ
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine($"[ERROR] NguoiDungDAO.UpdateVaiTroVaLoaiDoiTuong: {ex.Message}");
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
                Console.WriteLine($"[ERROR] NguoiDungDAO.GetHoSoByTenDangNhap: {ex.Message}");
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
        /// Tìm kiếm người dùng theo tên đăng nhập, vai trò hoặc trạng thái
        /// </summary>
        public List<NguoiDungDTO> SearchNguoiDung(string keyword)
        {
            List<NguoiDungDTO> list = new List<NguoiDungDTO>();
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                // ✅ Tìm kiếm theo TenDangNhap, TenVaiTro, TrangThai
                string query = @"SELECT n.TenDangNhap, n.MatKhau, n.TrangThai, n.LanDangNhapCuoi,
                       GROUP_CONCAT(v.TenVaiTro SEPARATOR ', ') as TenVaiTro,
                       GROUP_CONCAT(nv.MaVaiTro SEPARATOR ',') as MaVaiTro
                FROM NguoiDung n
                LEFT JOIN NguoiDungVaiTro nv ON n.TenDangNhap = nv.TenDangNhap
                LEFT JOIN VaiTro v ON nv.MaVaiTro = v.MaVaiTro
                WHERE n.TenDangNhap LIKE @keyword 
                   OR v.TenVaiTro LIKE @keyword 
                   OR n.TrangThai LIKE @keyword
                GROUP BY n.TenDangNhap
                ORDER BY n.TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NguoiDungDTO nd = new NguoiDungDTO
                            {
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString(),
                                VaiTro = reader["TenVaiTro"] != DBNull.Value ? reader["TenVaiTro"].ToString() : "Chưa có vai trò",
                                MaVaiTro = reader["MaVaiTro"] != DBNull.Value ? reader["MaVaiTro"].ToString() : null,
                                TrangThai = reader["TrangThai"] != DBNull.Value ? reader["TrangThai"].ToString() : "Hoạt động",
                                LanDangNhapCuoi = reader["LanDangNhapCuoi"] != DBNull.Value
                                    ? (DateTime?)reader["LanDangNhapCuoi"]
                                    : null
                            };
                            list.Add(nd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungDAO.SearchNguoiDung: {ex.Message}");
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return list;
        }

        /// <summary>
        /// ✅ Kiểm tra mật khẩu hiện tại có đúng không
        /// </summary>
        public bool KiemTraMatKhauHienTai(string tenDangNhap, string matKhauHash)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhauHash);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungDAO.KiemTraMatKhauHienTai: {ex.Message}");
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
