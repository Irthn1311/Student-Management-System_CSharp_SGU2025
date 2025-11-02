using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class NhapDiemDAO
    {
        /// <summary>
        /// Lấy danh sách học sinh kèm điểm theo môn học và học kỳ
        /// </summary>
        public List<NhapDiemDTO> GetDanhSachNhapDiem(int maMonHoc, int maHocKy)
        {
            List<NhapDiemDTO> list = new List<NhapDiemDTO>();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                string query = @"
            SELECT 
                hs.MaHocSinh,
                hs.HoTen,
                ds.DiemThuongXuyen,
                ds.DiemGiuaKy,
                ds.DiemCuoiKy,
                ds.DiemTrungBinh
            FROM HocSinh hs
            LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh 
                AND ds.MaMonHoc = @MaMonHoc 
                AND ds.MaHocKy = @MaHocKy
            WHERE hs.TrangThai = 'Đang học'
            ORDER BY hs.MaHocSinh";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhapDiemDTO dto = new NhapDiemDTO
                            {
                                MaHocSinh = reader["MaHocSinh"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                DiemTX = reader["DiemThuongXuyen"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemThuongXuyen"]) : (float?)null,
                                DiemGK = reader["DiemGiuaKy"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemGiuaKy"]) : (float?)null,
                                DiemCK = reader["DiemCuoiKy"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemCuoiKy"]) : (float?)null,
                                DiemTB = reader["DiemTrungBinh"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemTrungBinh"]) : (float?)null
                            };
                            list.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách nhập điểm: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return list;
        }
        /// <summary>
        /// Lấy danh sách học sinh kèm điểm theo lớp, môn học và học kỳ
        /// </summary>
        public List<NhapDiemDTO> GetDanhSachNhapDiemTheoLop(int maLop, int maMonHoc, int maHocKy)
        {
            List<NhapDiemDTO> list = new List<NhapDiemDTO>();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                string query = @"
            SELECT 
                hs.MaHocSinh,
                hs.HoTen,
                ds.DiemThuongXuyen,
                ds.DiemGiuaKy,
                ds.DiemCuoiKy,
                ds.DiemTrungBinh
            FROM HocSinh hs
            INNER JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh
            LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh 
                AND ds.MaMonHoc = @MaMonHoc 
                AND ds.MaHocKy = @MaHocKy
            WHERE pl.MaLop = @MaLop AND hs.TrangThai = 'Đang học'
            ORDER BY hs.MaHocSinh";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhapDiemDTO dto = new NhapDiemDTO
                            {
                                MaHocSinh = reader["MaHocSinh"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                DiemTX = reader["DiemThuongXuyen"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemThuongXuyen"]) : (float?)null,
                                DiemGK = reader["DiemGiuaKy"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemGiuaKy"]) : (float?)null,
                                DiemCK = reader["DiemCuoiKy"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemCuoiKy"]) : (float?)null,
                                DiemTB = reader["DiemTrungBinh"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemTrungBinh"]) : (float?)null
                            };
                            list.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách nhập điểm theo lớp: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return list;
        }

        /// <summary>
        /// Lấy tất cả học sinh để hiển thị ban đầu (không cần điểm)
        /// </summary>
        public List<NhapDiemDTO> GetAllHocSinhForNhapDiem()
        {
            List<NhapDiemDTO> list = new List<NhapDiemDTO>();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                string query = @"
            SELECT MaHocSinh, HoTen
            FROM HocSinh
            WHERE TrangThai = 'Đang học'
            ORDER BY MaHocSinh";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhapDiemDTO dto = new NhapDiemDTO
                            {
                                MaHocSinh = reader["MaHocSinh"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                DiemTX = null,
                                DiemGK = null,
                                DiemCK = null,
                                DiemTB = null
                            };
                            list.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách học sinh: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return list;
        }

        /// <summary>
        /// Lấy mã lớp của học sinh
        /// </summary>
        public int? GetMaLopByMaHocSinh(string maHocSinh)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
            SELECT MaLop 
            FROM PhanLop 
            WHERE MaHocSinh = @MaHocSinh 
            LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy mã lớp: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Lấy danh sách điểm của tất cả học sinh theo học kỳ (5 môn chính + điểm TB)
        /// Chỉ tính TB khi có đủ điểm 13 môn
        /// </summary>
        public List<XemBangDiemDTO> GetBangDiemTheoHocKy(int maHocKy)
        {
            List<XemBangDiemDTO> list = new List<XemBangDiemDTO>();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
            SELECT 
                hs.MaHocSinh,
                hs.HoTen,
                MAX(CASE WHEN mh.TenMonHoc = 'Toán' THEN ds.DiemTrungBinh END) as DiemToan,
                MAX(CASE WHEN mh.TenMonHoc = 'Ngữ Văn' THEN ds.DiemTrungBinh END) as DiemVan,
                MAX(CASE WHEN mh.TenMonHoc = 'Tiếng Anh' THEN ds.DiemTrungBinh END) as DiemAnh,
                MAX(CASE WHEN mh.TenMonHoc = 'Vật Lý' THEN ds.DiemTrungBinh END) as DiemLy,
                MAX(CASE WHEN mh.TenMonHoc = 'Hóa Học' THEN ds.DiemTrungBinh END) as DiemHoa,
                CASE 
                    WHEN (SELECT COUNT(DISTINCT ds2.MaMonHoc)
                          FROM DiemSo ds2
                          WHERE ds2.MaHocSinh = hs.MaHocSinh 
                            AND ds2.MaHocKy = @MaHocKy
                            AND ds2.DiemTrungBinh IS NOT NULL) = 13
                    THEN (SELECT AVG(ds2.DiemTrungBinh)
                          FROM DiemSo ds2
                          WHERE ds2.MaHocSinh = hs.MaHocSinh 
                            AND ds2.MaHocKy = @MaHocKy
                            AND ds2.DiemTrungBinh IS NOT NULL)
                    ELSE NULL
                END as DiemTB
            FROM HocSinh hs
            LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh AND ds.MaHocKy = @MaHocKy
            LEFT JOIN MonHoc mh ON ds.MaMonHoc = mh.MaMonHoc
            WHERE hs.TrangThai = 'Đang học'
            GROUP BY hs.MaHocSinh, hs.HoTen
            ORDER BY hs.MaHocSinh";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            XemBangDiemDTO dto = new XemBangDiemDTO
                            {
                                MaHocSinh = reader["MaHocSinh"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                DiemToan = reader["DiemToan"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemToan"]) : (float?)null,
                                DiemVan = reader["DiemVan"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemVan"]) : (float?)null,
                                DiemAnh = reader["DiemAnh"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemAnh"]) : (float?)null,
                                DiemLy = reader["DiemLy"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemLy"]) : (float?)null,
                                DiemHoa = reader["DiemHoa"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemHoa"]) : (float?)null,
                                DiemTB = reader["DiemTB"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemTB"]) : (float?)null
                            };
                            list.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy bảng điểm: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return list;
        }

        /// <summary>
        /// Lấy chi tiết điểm đầy đủ của một học sinh theo học kỳ
        /// </summary>
        public ChiTietDiemDTO GetChiTietDiem(string maHocSinh, int maHocKy)
        {
            ChiTietDiemDTO dto = null;
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                // Query lấy thông tin học sinh và điểm TB chung
                string queryHocSinh = @"
            SELECT 
                hs.MaHocSinh,
                hs.HoTen,
                CASE 
                    WHEN (SELECT COUNT(DISTINCT ds2.MaMonHoc)
                          FROM DiemSo ds2
                          WHERE ds2.MaHocSinh = @MaHocSinh 
                            AND ds2.MaHocKy = @MaHocKy
                            AND ds2.DiemTrungBinh IS NOT NULL) = 
                         (SELECT COUNT(*) FROM MonHoc)
                    THEN (SELECT AVG(ds2.DiemTrungBinh)
                          FROM DiemSo ds2
                          WHERE ds2.MaHocSinh = @MaHocSinh 
                            AND ds2.MaHocKy = @MaHocKy
                            AND ds2.DiemTrungBinh IS NOT NULL)
                    ELSE NULL
                END as DiemTB
            FROM HocSinh hs
            WHERE hs.MaHocSinh = @MaHocSinh";

                using (MySqlCommand cmd = new MySqlCommand(queryHocSinh, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dto = new ChiTietDiemDTO
                            {
                                MaHocSinh = reader["MaHocSinh"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                DiemTB = reader["DiemTB"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemTB"]) : (float?)null
                            };
                        }
                    }
                }

                if (dto == null)
                {
                    return null;
                }

                // Query lấy điểm các môn học
                string queryDiem = @"
            SELECT 
                mh.MaMonHoc,
                mh.TenMonHoc,
                ds.DiemTrungBinh
            FROM MonHoc mh
            LEFT JOIN DiemSo ds ON mh.MaMonHoc = ds.MaMonHoc 
                AND ds.MaHocSinh = @MaHocSinh 
                AND ds.MaHocKy = @MaHocKy
            ORDER BY mh.MaMonHoc";

                using (MySqlCommand cmd = new MySqlCommand(queryDiem, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int maMonHoc = Convert.ToInt32(reader["MaMonHoc"]);
                            DiemMonHocDTO diemMon = new DiemMonHocDTO
                            {
                                MaMonHoc = maMonHoc,
                                TenMonHoc = reader["TenMonHoc"].ToString(),
                                DiemTrungBinh = reader["DiemTrungBinh"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemTrungBinh"]) : (float?)null
                            };
                            dto.DiemCacMon.Add(maMonHoc, diemMon);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy chi tiết điểm: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return dto;
        }

        /// <summary>
        /// Lấy thống kê điểm theo học kỳ
        /// </summary>
        public ThongKeDTO GetThongKeDiemTheoHocKy(int maHocKy)
        {
            ThongKeDTO thongKe = new ThongKeDTO();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                // Query lấy thống kê
                string query = @"
            SELECT 
                -- Điểm TB chung (tính trên các học sinh có đủ 13 môn)
                AVG(CASE 
                    WHEN DiemTBChung.DiemTB IS NOT NULL THEN DiemTBChung.DiemTB 
                    ELSE NULL 
                END) as DiemTBChung,
                
                -- Điểm cao nhất
                MAX(DiemTBChung.DiemTB) as DiemCaoNhat,
                
                -- Điểm thấp nhất (chỉ tính học sinh có điểm)
                MIN(DiemTBChung.DiemTB) as DiemThapNhat,
                
                -- Tổng số học sinh
                COUNT(DISTINCT hs.MaHocSinh) as TongHocSinh,
                
                -- Số học sinh đã có điểm TB chung
                COUNT(DISTINCT CASE WHEN DiemTBChung.DiemTB IS NOT NULL THEN hs.MaHocSinh END) as HocSinhDaNhap
                
            FROM HocSinh hs
            LEFT JOIN (
                SELECT 
                    ds.MaHocSinh,
                    CASE 
                        WHEN COUNT(DISTINCT ds.MaMonHoc) = 13 AND COUNT(DISTINCT CASE WHEN ds.DiemTrungBinh IS NOT NULL THEN ds.MaMonHoc END) = 13
                        THEN AVG(ds.DiemTrungBinh)
                        ELSE NULL
                    END as DiemTB
                FROM DiemSo ds
                WHERE ds.MaHocKy = @MaHocKy
                GROUP BY ds.MaHocSinh
            ) as DiemTBChung ON hs.MaHocSinh = DiemTBChung.MaHocSinh
            WHERE hs.TrangThai = 'Đang học'";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            thongKe.DiemTBChung = reader["DiemTBChung"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemTBChung"]) : 0f;
                            thongKe.DiemCaoNhat = reader["DiemCaoNhat"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemCaoNhat"]) : 0f;
                            thongKe.DiemThapNhat = reader["DiemThapNhat"] != DBNull.Value ?
                                Convert.ToSingle(reader["DiemThapNhat"]) : 0f;
                            thongKe.TongHocSinh = Convert.ToInt32(reader["TongHocSinh"]);
                            thongKe.HocSinhDaNhapDiem = Convert.ToInt32(reader["HocSinhDaNhap"]);
                        }
                    }
                }

                // Tính số học sinh chưa nhập điểm
                thongKe.HocSinhChuaNhapDiem = thongKe.TongHocSinh - thongKe.HocSinhDaNhapDiem;

                // Lấy tên học sinh điểm cao nhất
                string queryDiemCaoNhat = @"
            SELECT hs.HoTen
            FROM HocSinh hs
            INNER JOIN (
                SELECT 
                    ds.MaHocSinh,
                    AVG(ds.DiemTrungBinh) as DiemTB
                FROM DiemSo ds
                WHERE ds.MaHocKy = @MaHocKy
                GROUP BY ds.MaHocSinh
                HAVING COUNT(DISTINCT ds.MaMonHoc) = 13 
                    AND COUNT(DISTINCT CASE WHEN ds.DiemTrungBinh IS NOT NULL THEN ds.MaMonHoc END) = 13
                ORDER BY DiemTB DESC
                LIMIT 1
            ) as DiemCaoNhat ON hs.MaHocSinh = DiemCaoNhat.MaHocSinh";

                using (MySqlCommand cmd = new MySqlCommand(queryDiemCaoNhat, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                    object result = cmd.ExecuteScalar();
                    thongKe.HocSinhDiemCaoNhat = result?.ToString() ?? "Chưa có";
                }

                // Lấy tên học sinh điểm thấp nhất
                string queryDiemThapNhat = @"
            SELECT hs.HoTen
            FROM HocSinh hs
            INNER JOIN (
                SELECT 
                    ds.MaHocSinh,
                    AVG(ds.DiemTrungBinh) as DiemTB
                FROM DiemSo ds
                WHERE ds.MaHocKy = @MaHocKy
                GROUP BY ds.MaHocSinh
                HAVING COUNT(DISTINCT ds.MaMonHoc) = 13 
                    AND COUNT(DISTINCT CASE WHEN ds.DiemTrungBinh IS NOT NULL THEN ds.MaMonHoc END) = 13
                ORDER BY DiemTB ASC
                LIMIT 1
            ) as DiemThapNhat ON hs.MaHocSinh = DiemThapNhat.MaHocSinh";

                using (MySqlCommand cmd = new MySqlCommand(queryDiemThapNhat, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                    object result = cmd.ExecuteScalar();
                    thongKe.HocSinhDiemThapNhat = result?.ToString() ?? "Chưa có";
                }

                // Lấy điểm TB học kỳ trước
                string queryDiemKyTruoc = @"
            SELECT AVG(DiemTB) as DiemTBKyTruoc
            FROM (
                SELECT 
                    ds.MaHocSinh,
                    AVG(ds.DiemTrungBinh) as DiemTB
                FROM DiemSo ds
                INNER JOIN HocKy hk ON ds.MaHocKy = hk.MaHocKy
                WHERE hk.MaHocKy < @MaHocKy
                GROUP BY ds.MaHocSinh, ds.MaHocKy
                HAVING COUNT(DISTINCT ds.MaMonHoc) = 13 
                    AND COUNT(DISTINCT CASE WHEN ds.DiemTrungBinh IS NOT NULL THEN ds.MaMonHoc END) = 13
                ORDER BY hk.MaHocKy DESC
            ) as DiemKyTruoc
            LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(queryDiemKyTruoc, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                    object result = cmd.ExecuteScalar();
                    thongKe.DiemTBChungKyTruoc = result != DBNull.Value && result != null ?
                        Convert.ToSingle(result) : 0f;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thống kê điểm: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return thongKe;
        }

        /// <summary>
        /// Lấy danh sách lớp có điểm số trong học kỳ
        /// </summary>
        public List<LopDTO> GetDanhSachLopCoDiem(int maHocKy)
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
            INNER JOIN DiemSo ds ON pl.MaHocSinh = ds.MaHocSinh
            WHERE ds.MaHocKy = @MaHocKy
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
                throw new Exception("Lỗi khi lấy danh sách lớp có điểm: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return list;
        }

        /// <summary>
        /// Lấy bảng điểm theo học kỳ và lọc theo lớp (nếu có)
        /// </summary>
        public List<XemBangDiemDTO> GetBangDiemTheoHocKyVaLop(int maHocKy, int? maLop = null)
        {
            List<XemBangDiemDTO> list = new List<XemBangDiemDTO>();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
            SELECT 
                hs.MaHocSinh,
                hs.HoTen,
                MAX(CASE WHEN mh.TenMonHoc = 'Toán' THEN ds.DiemTrungBinh END) as DiemToan,
                MAX(CASE WHEN mh.TenMonHoc = 'Ngữ Văn' THEN ds.DiemTrungBinh END) as DiemVan,
                MAX(CASE WHEN mh.TenMonHoc = 'Tiếng Anh' THEN ds.DiemTrungBinh END) as DiemAnh,
                MAX(CASE WHEN mh.TenMonHoc = 'Vật Lý' THEN ds.DiemTrungBinh END) as DiemLy,
                MAX(CASE WHEN mh.TenMonHoc = 'Hóa Học' THEN ds.DiemTrungBinh END) as DiemHoa,
                CASE 
                    WHEN (SELECT COUNT(DISTINCT ds2.MaMonHoc)
                          FROM DiemSo ds2
                          WHERE ds2.MaHocSinh = hs.MaHocSinh 
                            AND ds2.MaHocKy = @MaHocKy
                            AND ds2.DiemTrungBinh IS NOT NULL) = 13
                    THEN (SELECT AVG(ds2.DiemTrungBinh)
                          FROM DiemSo ds2
                          WHERE ds2.MaHocSinh = hs.MaHocSinh 
                            AND ds2.MaHocKy = @MaHocKy
                            AND ds2.DiemTrungBinh IS NOT NULL)
                    ELSE NULL
                END as DiemTB
            FROM HocSinh hs
            LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh AND ds.MaHocKy = @MaHocKy
            LEFT JOIN MonHoc mh ON ds.MaMonHoc = mh.MaMonHoc";

                // Thêm điều kiện lọc theo lớp nếu có
                if (maLop.HasValue && maLop.Value > 0)
                {
                    query += @"
            INNER JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh
            WHERE hs.TrangThai = 'Đang học' AND pl.MaLop = @MaLop";
                }
                else
                {
                    query += @"
            WHERE hs.TrangThai = 'Đang học'";
                }

                query += @"
            GROUP BY hs.MaHocSinh, hs.HoTen
            ORDER BY hs.MaHocSinh";

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
                            XemBangDiemDTO dto = new XemBangDiemDTO
                            {
                                MaHocSinh = reader["MaHocSinh"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                DiemToan = reader["DiemToan"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemToan"]) : (float?)null,
                                DiemVan = reader["DiemVan"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemVan"]) : (float?)null,
                                DiemAnh = reader["DiemAnh"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemAnh"]) : (float?)null,
                                DiemLy = reader["DiemLy"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemLy"]) : (float?)null,
                                DiemHoa = reader["DiemHoa"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemHoa"]) : (float?)null,
                                DiemTB = reader["DiemTB"] != DBNull.Value ?
                                    Convert.ToSingle(reader["DiemTB"]) : (float?)null
                            };
                            list.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy bảng điểm: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return list;
        }

    }
}