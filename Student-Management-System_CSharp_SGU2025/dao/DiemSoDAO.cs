using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class DiemSoDAO
    {
        /// <summary>
        /// Lấy điểm của học sinh theo môn học và học kỳ
        /// </summary>
        public DiemSoDTO GetDiemSo(string maHocSinh, int maMonHoc, int maHocKy)
        {
            DiemSoDTO diem = null;
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                string query = "SELECT MaHocSinh, MaMonHoc, MaHocKy, DiemMieng, " +
                              "DiemGiuaKy, DiemCuoiKy, DiemTrungBinh " +
                              "FROM DiemSo WHERE MaHocSinh = @MaHocSinh AND MaMonHoc = @MaMonHoc AND MaHocKy = @MaHocKy";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            diem = new DiemSoDTO
                            {
                                MaHocSinh = reader["MaHocSinh"].ToString(),
                                MaMonHoc = Convert.ToInt32(reader["MaMonHoc"]),
                                MaHocKy = Convert.ToInt32(reader["MaHocKy"]),
                                DiemThuongXuyen = reader["DiemMieng"] != DBNull.Value ? Convert.ToSingle(reader["DiemMieng"]) : (float?)null,
                                DiemGiuaKy = reader["DiemGiuaKy"] != DBNull.Value ? Convert.ToSingle(reader["DiemGiuaKy"]) : (float?)null,
                                DiemCuoiKy = reader["DiemCuoiKy"] != DBNull.Value ? Convert.ToSingle(reader["DiemCuoiKy"]) : (float?)null,
                                DiemTrungBinh = reader["DiemTrungBinh"] != DBNull.Value ? Convert.ToSingle(reader["DiemTrungBinh"]) : (float?)null
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy điểm số: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return diem;
        }

        /// <summary>
        /// Thêm hoặc cập nhật điểm (Insert nếu chưa có, Update nếu đã có)
        /// </summary>
        public bool UpsertDiemSo(DiemSoDTO diem)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
INSERT INTO DiemSo (MaHocSinh, MaMonHoc, MaHocKy, DiemMieng, 
                    DiemGiuaKy, DiemCuoiKy, DiemTrungBinh) 
                    VALUES (@MaHocSinh, @MaMonHoc, @MaHocKy, @DiemMieng, 
                    @DiemGiuaKy, @DiemCuoiKy, @DiemTrungBinh)
                    ON DUPLICATE KEY UPDATE
                        DiemMieng = @DiemMieng,
                        DiemGiuaKy = @DiemGiuaKy,
                        DiemCuoiKy = @DiemCuoiKy,
                        DiemTrungBinh = @DiemTrungBinh";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocSinh", diem.MaHocSinh);
                    cmd.Parameters.AddWithValue("@MaMonHoc", diem.MaMonHoc);
                    cmd.Parameters.AddWithValue("@MaHocKy", diem.MaHocKy);
                    cmd.Parameters.AddWithValue("@DiemMieng", diem.DiemThuongXuyen.HasValue ? (object)diem.DiemThuongXuyen.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemGiuaKy", diem.DiemGiuaKy.HasValue ? (object)diem.DiemGiuaKy.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemCuoiKy", diem.DiemCuoiKy.HasValue ? (object)diem.DiemCuoiKy.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemTrungBinh", diem.DiemTrungBinh.HasValue ? (object)diem.DiemTrungBinh.Value : DBNull.Value);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lưu điểm số: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }
        /// <summary>
        /// Kiểm tra xem điểm đã tồn tại chưa
        /// </summary>
        public bool KiemTraDiemTonTai(string maHocSinh, int maMonHoc, int maHocKy)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
            SELECT COUNT(*) 
            FROM DiemSo 
            WHERE MaHocSinh = @MaHocSinh 
                AND MaMonHoc = @MaMonHoc 
                AND MaHocKy = @MaHocKy";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi kiểm tra điểm tồn tại: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Thêm mới điểm (chỉ INSERT, không UPDATE)
        /// </summary>
        public bool ThemDiemMoi(DiemSoDTO diem)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
INSERT INTO DiemSo (MaHocSinh, MaMonHoc, MaHocKy, DiemMieng, 
                    DiemGiuaKy, DiemCuoiKy, DiemTrungBinh) 
                    VALUES (@MaHocSinh, @MaMonHoc, @MaHocKy, @DiemMieng, 
                    @DiemGiuaKy, @DiemCuoiKy, @DiemTrungBinh)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocSinh", diem.MaHocSinh);
                    cmd.Parameters.AddWithValue("@MaMonHoc", diem.MaMonHoc);
                    cmd.Parameters.AddWithValue("@MaHocKy", diem.MaHocKy);
                    cmd.Parameters.AddWithValue("@DiemThuongXuyen",
                        diem.DiemThuongXuyen.HasValue ? (object)diem.DiemThuongXuyen.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemGiuaKy",
                        diem.DiemGiuaKy.HasValue ? (object)diem.DiemGiuaKy.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemCuoiKy",
                        diem.DiemCuoiKy.HasValue ? (object)diem.DiemCuoiKy.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemTrungBinh",
                        diem.DiemTrungBinh.HasValue ? (object)diem.DiemTrungBinh.Value : DBNull.Value);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm điểm mới: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Kiểm tra điểm đã đầy đủ chưa (có đủ 3 cột điểm)
        /// </summary>
        public bool KiemTraDiemDayDu(string maHocSinh, int maMonHoc, int maHocKy)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
            SELECT DiemMieng, DiemGiuaKy, DiemCuoiKy
            FROM DiemSo 
            WHERE MaHocSinh = @MaHocSinh 
                AND MaMonHoc = @MaMonHoc 
                AND MaHocKy = @MaHocKy";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool coDiemTX = reader["DiemMieng"] != DBNull.Value;
                            bool coDiemGK = reader["DiemGiuaKy"] != DBNull.Value;
                            bool coDiemCK = reader["DiemCuoiKy"] != DBNull.Value;

                            // Chỉ trả về true nếu đã có đủ cả 3 điểm
                            return coDiemTX && coDiemGK && coDiemCK;
                        }
                        return false; // Chưa có record
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi kiểm tra điểm đầy đủ: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Thêm hoặc cập nhật điểm thông minh: chỉ cập nhật các cột NULL
        /// Trả về: true nếu thêm được điểm mới, false nếu không có gì thay đổi
        /// </summary>
        public bool ThemDiemThongMinh(DiemSoDTO diem)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                // Kiểm tra xem đã có record chưa
                string checkQuery = @"
            SELECT DiemMieng, DiemGiuaKy, DiemCuoiKy
            FROM DiemSo 
            WHERE MaHocSinh = @MaHocSinh 
                AND MaMonHoc = @MaMonHoc 
                AND MaHocKy = @MaHocKy";

                bool recordExists = false;
                float? existingDiemTX = null;
                float? existingDiemGK = null;
                float? existingDiemCK = null;

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@MaHocSinh", diem.MaHocSinh);
                    checkCmd.Parameters.AddWithValue("@MaMonHoc", diem.MaMonHoc);
                    checkCmd.Parameters.AddWithValue("@MaHocKy", diem.MaHocKy);

                    using (MySqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recordExists = true;
                            existingDiemTX = reader["DiemMieng"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemMieng"]) : (float?)null;
                            existingDiemGK = reader["DiemGiuaKy"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemGiuaKy"]) : (float?)null;
                            existingDiemCK = reader["DiemCuoiKy"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemCuoiKy"]) : (float?)null;
                        }
                    }
                }

                if (recordExists)
                {
                    // Kiểm tra xem có điểm nào mới được thêm không
                    bool coThemDiemMoi = false;

                    // Chỉ cho phép thêm vào các cột NULL
                    if (!existingDiemTX.HasValue && diem.DiemThuongXuyen.HasValue)
                        coThemDiemMoi = true;
                    if (!existingDiemGK.HasValue && diem.DiemGiuaKy.HasValue)
                        coThemDiemMoi = true;
                    if (!existingDiemCK.HasValue && diem.DiemCuoiKy.HasValue)
                        coThemDiemMoi = true;

                    // Nếu không có điểm mới nào được thêm, throw exception
                    if (!coThemDiemMoi)
                    {
                        throw new Exception("DIEM_DA_TON_TAI_KHONG_THE_THEM");
                    }

                    // ===== QUAN TRỌNG: Tính lại điểm TB với điểm hiện tại =====
                    // Kết hợp điểm cũ và điểm mới để tính điểm TB
                    float? finalDiemTX = existingDiemTX.HasValue ? existingDiemTX : diem.DiemThuongXuyen;
                    float? finalDiemGK = existingDiemGK.HasValue ? existingDiemGK : diem.DiemGiuaKy;
                    float? finalDiemCK = existingDiemCK.HasValue ? existingDiemCK : diem.DiemCuoiKy;

                    // Tính điểm TB nếu có đủ 3 điểm
                    float? diemTB = null;
                    if (finalDiemTX.HasValue && finalDiemGK.HasValue && finalDiemCK.HasValue)
                    {
                        diemTB = (finalDiemTX.Value + finalDiemGK.Value * 2 + finalDiemCK.Value * 3) / 6;
                        diemTB = (float)Math.Round(diemTB.Value, 1);
                    }

                    // UPDATE chỉ các cột chưa có giá trị
                    string updateQuery = @"
                UPDATE DiemSo 
                SET DiemMieng = COALESCE(DiemMieng, @DiemMieng),
                    DiemGiuaKy = COALESCE(DiemGiuaKy, @DiemGiuaKy),
                    DiemCuoiKy = COALESCE(DiemCuoiKy, @DiemCuoiKy),
                    DiemTrungBinh = @DiemTrungBinh
                WHERE MaHocSinh = @MaHocSinh 
                    AND MaMonHoc = @MaMonHoc 
                    AND MaHocKy = @MaHocKy";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocSinh", diem.MaHocSinh);
                        cmd.Parameters.AddWithValue("@MaMonHoc", diem.MaMonHoc);
                        cmd.Parameters.AddWithValue("@MaHocKy", diem.MaHocKy);
                        cmd.Parameters.AddWithValue("@DiemMieng",
                            diem.DiemThuongXuyen.HasValue ? (object)diem.DiemThuongXuyen.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemGiuaKy",
                            diem.DiemGiuaKy.HasValue ? (object)diem.DiemGiuaKy.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemCuoiKy",
                            diem.DiemCuoiKy.HasValue ? (object)diem.DiemCuoiKy.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemTrungBinh",
                            diemTB.HasValue ? (object)diemTB.Value : DBNull.Value);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                else
                {
                    // INSERT mới
                    string insertQuery = @"
INSERT INTO DiemSo (MaHocSinh, MaMonHoc, MaHocKy, DiemMieng, 
                    DiemGiuaKy, DiemCuoiKy, DiemTrungBinh) 
                    VALUES (@MaHocSinh, @MaMonHoc, @MaHocKy, @DiemMieng, 
                    @DiemGiuaKy, @DiemCuoiKy, @DiemTrungBinh)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocSinh", diem.MaHocSinh);
                        cmd.Parameters.AddWithValue("@MaMonHoc", diem.MaMonHoc);
                        cmd.Parameters.AddWithValue("@MaHocKy", diem.MaHocKy);
                        cmd.Parameters.AddWithValue("@DiemMieng",
                            diem.DiemThuongXuyen.HasValue ? (object)diem.DiemThuongXuyen.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemGiuaKy",
                            diem.DiemGiuaKy.HasValue ? (object)diem.DiemGiuaKy.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemCuoiKy",
                            diem.DiemCuoiKy.HasValue ? (object)diem.DiemCuoiKy.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemTrungBinh",
                            diem.DiemTrungBinh.HasValue ? (object)diem.DiemTrungBinh.Value : DBNull.Value);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

    }
}
