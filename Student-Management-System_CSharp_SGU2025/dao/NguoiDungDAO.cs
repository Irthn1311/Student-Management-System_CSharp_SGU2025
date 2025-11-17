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

                string query = @"SELECT n.TenDangNhap, n.MatKhau, n.TrangThai, 
                                       GROUP_CONCAT(nv.MaVaiTro) as VaiTro
                                FROM NguoiDung n
                                LEFT JOIN NguoiDungVaiTro nv ON n.TenDangNhap = nv.TenDangNhap
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
                                VaiTro = reader["VaiTro"] != DBNull.Value ? reader["VaiTro"].ToString() : null,
                                TrangThai = reader["TrangThai"] != DBNull.Value ? reader["TrangThai"].ToString() : "Hoạt động"
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

                string query = @"SELECT n.TenDangNhap, n.MatKhau, n.TrangThai,
                                       GROUP_CONCAT(nv.MaVaiTro) as VaiTro
                                FROM NguoiDung n
                                LEFT JOIN NguoiDungVaiTro nv ON n.TenDangNhap = nv.TenDangNhap
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
                                VaiTro = reader["VaiTro"] != DBNull.Value ? reader["VaiTro"].ToString() : null,
                                TrangThai = reader["TrangThai"] != DBNull.Value ? reader["TrangThai"].ToString() : "Hoạt động"
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
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "DELETE FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
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
    }
}
