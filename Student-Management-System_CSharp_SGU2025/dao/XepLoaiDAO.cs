using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class XepLoaiDAO
    {
        /// <summary>
        /// Lấy danh sách xếp loại học sinh theo học kỳ và lớp
        /// </summary>
        public List<XepLoaiDTO> GetDanhSachXepLoai(int maHocKy, int? maLop = null)
        {
            List<XepLoaiDTO> list = new List<XepLoaiDTO>();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
                    SELECT 
                        hs.MaHocSinh,
                        hs.HoTen,
                        l.TenLop,
                        -- Điểm trung bình chung (chỉ tính nếu có đủ 13 môn)
                        CASE 
                            WHEN COUNT(DISTINCT ds.MaMonHoc) = 13 
                                 AND COUNT(DISTINCT CASE WHEN ds.DiemTrungBinh IS NOT NULL THEN ds.MaMonHoc END) = 13
                            THEN AVG(ds.DiemTrungBinh)
                            ELSE NULL
                        END as DiemTB,
                        -- Điểm Toán
                        MAX(CASE WHEN mh.TenMonHoc = 'Toán' THEN ds.DiemTrungBinh END) as DiemToan,
                        -- Điểm Ngữ Văn
                        MAX(CASE WHEN mh.TenMonHoc = 'Ngữ Văn' THEN ds.DiemTrungBinh END) as DiemVan,
                        -- Điểm Tiếng Anh
                        MAX(CASE WHEN mh.TenMonHoc = 'Tiếng Anh' THEN ds.DiemTrungBinh END) as DiemAnh,
                        -- Điểm thấp nhất
                        MIN(ds.DiemTrungBinh) as DiemThapNhat
                    FROM HocSinh hs
                    INNER JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh 
                        AND pl.MaHocKy = @MaHocKy";

                if (maLop.HasValue && maLop.Value > 0)
                {
                    query += " AND pl.MaLop = @MaLop";
                }

                query += @"
                    INNER JOIN LopHoc l ON pl.MaLop = l.MaLop
                    LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh 
                        AND ds.MaHocKy = @MaHocKy
                    LEFT JOIN MonHoc mh ON ds.MaMonHoc = mh.MaMonHoc
                    WHERE hs.TrangThai = 'Đang học'
                    GROUP BY hs.MaHocSinh, hs.HoTen, l.TenLop
                    HAVING DiemTB IS NOT NULL
                    ORDER BY l.TenLop, hs.HoTen";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                    if (maLop.HasValue && maLop.Value > 0)
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop.Value);
                    }

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            float? diemTB = reader["DiemTB"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemTB"]) : (float?)null;
                            float? diemToan = reader["DiemToan"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemToan"]) : (float?)null;
                            float? diemVan = reader["DiemVan"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemVan"]) : (float?)null;
                            float? diemAnh = reader["DiemAnh"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemAnh"]) : (float?)null;
                            float? diemThapNhat = reader["DiemThapNhat"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemThapNhat"]) : (float?)null;

                            // Tính học lực
                            string hocLuc = TinhHocLuc(diemTB, diemToan, diemVan, diemAnh, diemThapNhat);

                            XepLoaiDTO dto = new XepLoaiDTO
                            {
                                MaHocSinh = Convert.ToInt32(reader["MaHocSinh"]),
                                HoTen = reader["HoTen"].ToString(),
                                TenLop = reader["TenLop"].ToString(),
                                DiemTB = diemTB,
                                HocLuc = hocLuc,
                                DiemToan = diemToan,
                                DiemVan = diemVan,
                                DiemAnh = diemAnh,
                                DiemThapNhat = diemThapNhat
                            };
                            list.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách xếp loại: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return list;
        }

        /// <summary>
        /// Tính học lực dựa trên điểm số
        /// </summary>
        private string TinhHocLuc(float? diemTB, float? diemToan, float? diemVan,
            float? diemAnh, float? diemThapNhat)
        {
            if (!diemTB.HasValue || !diemToan.HasValue || !diemVan.HasValue ||
                !diemAnh.HasValue || !diemThapNhat.HasValue)
            {
                return "";
            }

            // Tính điểm cao nhất trong 3 môn chính
            float diemCaoNhat3Mon = Math.Max(diemToan.Value, Math.Max(diemVan.Value, diemAnh.Value));

            // GIỎI
            if (diemTB.Value >= 8.0f && diemCaoNhat3Mon >= 8.0f && diemThapNhat.Value >= 6.5f)
            {
                return "Giỏi";
            }
            // KHÁ
            else if (diemTB.Value >= 6.5f && diemCaoNhat3Mon >= 6.5f && diemThapNhat.Value >= 5.0f)
            {
                return "Khá";
            }
            // TRUNG BÌNH
            else if (diemTB.Value >= 5.0f && diemCaoNhat3Mon >= 5.0f && diemThapNhat.Value >= 3.5f)
            {
                return "Trung bình";
            }
            // YẾU
            else if (diemTB.Value >= 3.5f && diemThapNhat.Value >= 2.0f)
            {
                return "Yếu";
            }
            else
            {
                return "Kém";
            }
        }

        /// <summary>
        /// Lấy danh sách lớp có học sinh đã phân lớp trong học kỳ
        /// </summary>
        public List<LopDTO> GetDanhSachLopTheoHocKy(int maHocKy)
        {
            List<LopDTO> list = new List<LopDTO>();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
                    SELECT DISTINCT l.MaLop, l.TenLop, l.MaKhoi, l.MaGiaoVienChuNhiem
                    FROM LopHoc l
                    INNER JOIN PhanLop pl ON l.MaLop = pl.MaLop
                    WHERE pl.MaHocKy = @MaHocKy
                    ORDER BY l.TenLop";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LopDTO lop = new LopDTO
                            {
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                TenLop = reader["TenLop"].ToString(),
                                MaKhoi = Convert.ToInt32(reader["MaKhoi"]),
                                MaGVCN = reader["MaGiaoVienChuNhiem"]?.ToString()
                            };
                            list.Add(lop);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách lớp theo học kỳ: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return list;
        }

        // Thêm vào XepLoaiDAO.cs

        /// <summary>
        /// Lưu xếp loại tổng kết vào database
        /// </summary>
        public bool LuuXepLoai(int maHocSinh, int maHocKy, string xepLoai, string ghiChu = "")
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"INSERT INTO XepLoai (MaHocSinh, MaHocKy, XepLoaiDG, GhiChu) 
                        VALUES (@maHS, @maHK, @xepLoai, @ghiChu)
                        ON DUPLICATE KEY UPDATE 
                        XepLoaiDG = @xepLoai, 
                        GhiChu = @ghiChu";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                    cmd.Parameters.AddWithValue("@maHK", maHocKy);
                    cmd.Parameters.AddWithValue("@xepLoai", xepLoai);
                    cmd.Parameters.AddWithValue("@ghiChu", (object)ghiChu ?? DBNull.Value);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lưu xếp loại: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Lấy xếp loại đã lưu của học sinh
        /// </summary>
        public string LayXepLoaiDaLuu(int maHocSinh, int maHocKy)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"SELECT XepLoaiDG FROM XepLoai 
                        WHERE MaHocSinh = @maHS AND MaHocKy = @maHK";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                    cmd.Parameters.AddWithValue("@maHK", maHocKy);

                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

    }
}
