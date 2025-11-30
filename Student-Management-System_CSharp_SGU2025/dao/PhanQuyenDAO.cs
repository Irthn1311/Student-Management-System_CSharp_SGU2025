using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class PhanQuyenDAO
    {
        #region Chức năng

        /// <summary>
        /// Lấy danh sách tất cả chức năng
        /// </summary>
        public List<ChucNangDTO> GetAllChucNang()
        {
            List<ChucNangDTO> list = new List<ChucNangDTO>();

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = "SELECT MaChucNang, TenChucNang, MoTa FROM ChucNang ORDER BY MaChucNang";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new ChucNangDTO
                    {
                        MaChucNang = reader["MaChucNang"].ToString(),
                        TenChucNang = reader["TenChucNang"].ToString(),
                        MoTa = reader["MoTa"]?.ToString()
                    });
                }
            }

            return list;
        }

        #endregion

        #region Vai trò

        /// <summary>
        /// Thêm vai trò mới
        /// </summary>
        public bool InsertVaiTro(VaiTroDTO vaiTro)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = "INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES (@MaVaiTro, @TenVaiTro, @MoTa)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@MaVaiTro", vaiTro.MaVaiTro);
                cmd.Parameters.AddWithValue("@TenVaiTro", vaiTro.TenVaiTro);
                cmd.Parameters.AddWithValue("@MoTa", vaiTro.MoTa ?? (object)DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Lấy tất cả vai trò
        /// </summary>
        public List<VaiTroDTO> GetAllVaiTro()
        {
            List<VaiTroDTO> list = new List<VaiTroDTO>();

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = "SELECT MaVaiTro, TenVaiTro, MoTa FROM VaiTro ORDER BY TenVaiTro";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new VaiTroDTO
                    {
                        MaVaiTro = reader["MaVaiTro"].ToString(),
                        TenVaiTro = reader["TenVaiTro"].ToString(),
                        MoTa = reader["MoTa"]?.ToString()
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// Xóa vai trò - CHỈ XÓA QUYỀN, KHÔNG XÓA TÀI KHOẢN
        /// </summary>
        public bool DeleteVaiTro(string maVaiTro)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // ✅ 1. Xóa VaiTroChucNangHanhDong (quyền chi tiết)
                    string query1 = "DELETE FROM VaiTroChucNangHanhDong WHERE MaVaiTro = @MaVaiTro";
                    MySqlCommand cmd1 = new MySqlCommand(query1, conn, transaction);
                    cmd1.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                    cmd1.ExecuteNonQuery();

                    // ✅ 2. Xóa VaiTroChucNang (liên kết vai trò - chức năng)
                    string query2 = "DELETE FROM VaiTroChucNang WHERE MaVaiTro = @MaVaiTro";
                    MySqlCommand cmd2 = new MySqlCommand(query2, conn, transaction);
                    cmd2.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                    cmd2.ExecuteNonQuery();

                    // ⚠️ KHÔNG XÓA NguoiDungVaiTro - Để giữ lại lịch sử

                    // ✅ 3. Xóa VaiTro
                    string query4 = "DELETE FROM VaiTro WHERE MaVaiTro = @MaVaiTro";
                    MySqlCommand cmd4 = new MySqlCommand(query4, conn, transaction);
                    cmd4.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                    cmd4.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        #endregion

        #region VaiTroChucNang & VaiTroChucNangHanhDong

        /// <summary>
        /// Thêm quyền cho vai trò (VaiTroChucNang)
        /// </summary>
        public bool InsertVaiTroChucNang(string maVaiTro, string maChucNang)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = "INSERT IGNORE INTO VaiTroChucNang (MaVaiTro, MaChucNang) VALUES (@MaVaiTro, @MaChucNang)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                cmd.Parameters.AddWithValue("@MaChucNang", maChucNang);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Thêm hành động cho vai trò và chức năng (VaiTroChucNangHanhDong)
        /// </summary>
        public bool InsertVaiTroChucNangHanhDong(string maVaiTro, string maChucNang, string hanhDong)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = "INSERT IGNORE INTO VaiTroChucNangHanhDong (MaVaiTro, MaChucNang, HanhDong) " +
                              "VALUES (@MaVaiTro, @MaChucNang, @HanhDong)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                cmd.Parameters.AddWithValue("@MaChucNang", maChucNang);
                cmd.Parameters.AddWithValue("@HanhDong", hanhDong);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Thêm hành động vào bảng ChucNangHanhDong
        /// </summary>
        public bool InsertChucNangHanhDong(string maChucNang, string hanhDong)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = "INSERT IGNORE INTO ChucNangHanhDong (MaChucNang, HanhDong) " +
                              "VALUES (@MaChucNang, @HanhDong)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@MaChucNang", maChucNang);
                cmd.Parameters.AddWithValue("@HanhDong", hanhDong);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Lấy danh sách chức năng của vai trò
        /// </summary>
        public List<string> GetChucNangByVaiTro(string maVaiTro)
        {
            List<string> list = new List<string>();

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = @"SELECT DISTINCT c.TenChucNang 
                                FROM VaiTroChucNang vc
                                INNER JOIN ChucNang c ON vc.MaChucNang = c.MaChucNang
                                WHERE vc.MaVaiTro = @MaVaiTro
                                ORDER BY c.TenChucNang";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader["TenChucNang"].ToString());
                }
            }

            return list;
        }

        /// <summary>
        /// Kiểm tra vai trò có quyền trên chức năng và hành động cụ thể
        /// </summary>
        public bool KiemTraQuyen(string maVaiTro, string maChucNang, string hanhDong)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = @"SELECT COUNT(*) 
                                FROM VaiTroChucNangHanhDong 
                                WHERE MaVaiTro = @MaVaiTro 
                                AND MaChucNang = @MaChucNang 
                                AND HanhDong = @HanhDong";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                cmd.Parameters.AddWithValue("@MaChucNang", maChucNang);
                cmd.Parameters.AddWithValue("@HanhDong", hanhDong);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        /// <summary>
        /// Lấy danh sách vai trò của người dùng
        /// </summary>
        public List<string> GetVaiTroByNguoiDung(string tenDangNhap)
        {
            List<string> list = new List<string>();

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = @"SELECT MaVaiTro FROM NguoiDungVaiTro 
                                WHERE TenDangNhap = @TenDangNhap";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader["MaVaiTro"].ToString());
                }
            }

            return list;
        }

        #endregion

        #region Transaction - Thêm vai trò với đầy đủ quyền

        /// <summary>
        /// Thêm vai trò với các quyền được chọn (Transaction)
        /// </summary>
        public bool ThemVaiTroVoiQuyen(VaiTroDTO vaiTro, List<VaiTroChucNangHanhDongDTO> danhSachQuyen)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Thêm VaiTro
                    string query1 = "INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES (@MaVaiTro, @TenVaiTro, @MoTa)";
                    MySqlCommand cmd1 = new MySqlCommand(query1, conn, transaction);
                    cmd1.Parameters.AddWithValue("@MaVaiTro", vaiTro.MaVaiTro);
                    cmd1.Parameters.AddWithValue("@TenVaiTro", vaiTro.TenVaiTro);
                    cmd1.Parameters.AddWithValue("@MoTa", vaiTro.MoTa ?? (object)DBNull.Value);
                    cmd1.ExecuteNonQuery();

                    // 2. Thêm VaiTroChucNang và VaiTroChucNangHanhDong
                    foreach (var quyen in danhSachQuyen)
                    {
                        // Thêm vào VaiTroChucNang (nếu chưa có)
                        string query2 = "INSERT IGNORE INTO VaiTroChucNang (MaVaiTro, MaChucNang) VALUES (@MaVaiTro, @MaChucNang)";
                        MySqlCommand cmd2 = new MySqlCommand(query2, conn, transaction);
                        cmd2.Parameters.AddWithValue("@MaVaiTro", vaiTro.MaVaiTro);
                        cmd2.Parameters.AddWithValue("@MaChucNang", quyen.MaChucNang);
                        cmd2.ExecuteNonQuery();

                        // Thêm vào ChucNangHanhDong (nếu chưa có)
                        string query3 = "INSERT IGNORE INTO ChucNangHanhDong (MaChucNang, HanhDong) VALUES (@MaChucNang, @HanhDong)";
                        MySqlCommand cmd3 = new MySqlCommand(query3, conn, transaction);
                        cmd3.Parameters.AddWithValue("@MaChucNang", quyen.MaChucNang);
                        cmd3.Parameters.AddWithValue("@HanhDong", quyen.HanhDong);
                        cmd3.ExecuteNonQuery();

                        // Thêm vào VaiTroChucNangHanhDong
                        string query4 = "INSERT INTO VaiTroChucNangHanhDong (MaVaiTro, MaChucNang, HanhDong) " +
                                       "VALUES (@MaVaiTro, @MaChucNang, @HanhDong)";
                        MySqlCommand cmd4 = new MySqlCommand(query4, conn, transaction);
                        cmd4.Parameters.AddWithValue("@MaVaiTro", vaiTro.MaVaiTro);
                        cmd4.Parameters.AddWithValue("@MaChucNang", quyen.MaChucNang);
                        cmd4.Parameters.AddWithValue("@HanhDong", quyen.HanhDong);
                        cmd4.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        #endregion

        /// <summary>
        /// Kiểm tra vai trò đã được gán cho người dùng nào chưa
        /// </summary>
        public bool KiemTraVaiTroDuocGan(string maVaiTro)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = @"SELECT COUNT(*) 
                        FROM NguoiDungVaiTro 
                        WHERE MaVaiTro = @MaVaiTro";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; // true = đã có người dùng, false = chưa có ai
            }
        }

        /// <summary>
        /// Lấy danh sách tên đăng nhập của người dùng có vai trò này
        /// </summary>
        public List<string> GetNguoiDungByVaiTro(string maVaiTro)
        {
            List<string> list = new List<string>();

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = @"SELECT nd.TenDangNhap, hs.HoTen
                        FROM NguoiDungVaiTro ndv
                        INNER JOIN NguoiDung nd ON ndv.TenDangNhap = nd.TenDangNhap
                        LEFT JOIN HoSoNguoiDung hs ON nd.TenDangNhap = hs.TenDangNhap
                        WHERE ndv.MaVaiTro = @MaVaiTro";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string tenDangNhap = reader["TenDangNhap"].ToString();
                    string hoTen = reader["HoTen"]?.ToString();

                    if (!string.IsNullOrEmpty(hoTen))
                        list.Add($"{tenDangNhap} ({hoTen})");
                    else
                        list.Add(tenDangNhap);
                }
            }

            return list;
        }

        /// <summary>
        /// Lấy chi tiết đầy đủ của vai trò (chức năng + hành động)
        /// </summary>
        public Dictionary<string, List<string>> GetChiTietVaiTro(string maVaiTro)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                string query = @"SELECT c.TenChucNang, v.HanhDong
                        FROM VaiTroChucNangHanhDong v
                        INNER JOIN ChucNang c ON v.MaChucNang = c.MaChucNang
                        WHERE v.MaVaiTro = @MaVaiTro
                        ORDER BY c.TenChucNang, v.HanhDong";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaVaiTro", maVaiTro);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string tenChucNang = reader["TenChucNang"].ToString();
                    string hanhDong = reader["HanhDong"].ToString();

                    if (!result.ContainsKey(tenChucNang))
                    {
                        result[tenChucNang] = new List<string>();
                    }

                    result[tenChucNang].Add(hanhDong);
                }
            }

            return result;
        }

    }
}