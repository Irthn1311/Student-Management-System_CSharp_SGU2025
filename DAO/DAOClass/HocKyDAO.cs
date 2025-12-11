using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class HocKyDAO
    {
        public bool ThemHocKy(HocKyDTO hocKy)
        {
            // (Code hàm ThemHocKy của bạn... giữ nguyên)
            string query = "INSERT INTO HocKy(MaHocKy, TenHocKy, MaNamHoc, NgayBD, NgayKT, TrangThai) VALUES(@MaHocKy, @TenHocKy, @MaNamHoc, @NgayBD, @NgayKT, @TrangThai)";
            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocKy", hocKy.MaHocKy); // Thêm MaHocKy nếu bạn dùng (dựa trên SQL)
                        cmd.Parameters.AddWithValue("@TenHocKy", hocKy.TenHocKy);
                        cmd.Parameters.AddWithValue("@MaNamHoc", hocKy.MaNamHoc);
                        cmd.Parameters.AddWithValue("@NgayBD", hocKy.NgayBD);
                        cmd.Parameters.AddWithValue("@NgayKT", hocKy.NgayKT);
                        cmd.Parameters.AddWithValue("@TrangThai", hocKy.TrangThai);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi ThemHocKy: {ex.Message}");
                throw;
            }
        }

        public List<HocKyDTO> DocDSHocKy()
        {
            // (Code hàm DocDSHocKy của bạn... giữ nguyên)
            List<HocKyDTO> ds = new List<HocKyDTO>();
            string query = @"SELECT hk.MaHocKy, hk.TenHocKy, hk.MaNamHoc, hk.NgayBD, hk.NgayKT, hk.TrangThai 
                            FROM HocKy hk 
                            INNER JOIN NamHoc nh ON hk.MaNamHoc = nh.MaNamHoc 
                            ORDER BY nh.NgayBatDau DESC, hk.NgayBD DESC";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HocKyDTO hk = new HocKyDTO
                                {
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    TenHocKy = reader.GetString("TenHocKy"),
                                    MaNamHoc = reader.GetString("MaNamHoc"),
                                    NgayBD = reader.GetDateTime("NgayBD"),
                                    NgayKT = reader.GetDateTime("NgayKT"),
                                    TrangThai = reader.GetString("TrangThai")
                                };
                                ds.Add(hk);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi DocDSHocKy: {ex.Message}");
                throw;
            }

            return ds;
        }

        public HocKyDTO LayHocKyTheoMa(int maHocKy)
        {
            // (Code hàm LayHocKyTheoMa của bạn... giữ nguyên)
            HocKyDTO hocKy = null;
            string query = "SELECT MaHocKy, TenHocKy, MaNamHoc, NgayBD, NgayKT, TrangThai FROM HocKy WHERE MaHocKy=@MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hocKy = new HocKyDTO
                                {
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    TenHocKy = reader.GetString("TenHocKy"),
                                    MaNamHoc = reader.GetString("MaNamHoc"),
                                    NgayBD = reader.GetDateTime("NgayBD"),
                                    NgayKT = reader.GetDateTime("NgayKT"),
                                    TrangThai = reader.GetString("TrangThai")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayHocKyTheoMa: {ex.Message}");
                throw;
            }

            return hocKy;
        }

        public bool CapNhatHocKy(HocKyDTO hocKy)
        {
            // (Code hàm CapNhatHocKy của bạn... giữ nguyên)
            string query = "UPDATE HocKy SET TenHocKy=@TenHocKy, MaNamHoc=@MaNamHoc, NgayBD=@NgayBD, NgayKT=@NgayKT, TrangThai=@TrangThai WHERE MaHocKy=@MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenHocKy", hocKy.TenHocKy);
                        cmd.Parameters.AddWithValue("@MaNamHoc", hocKy.MaNamHoc);
                        cmd.Parameters.AddWithValue("@NgayBD", hocKy.NgayBD);
                        cmd.Parameters.AddWithValue("@NgayKT", hocKy.NgayKT);
                        cmd.Parameters.AddWithValue("@TrangThai", hocKy.TrangThai);
                        cmd.Parameters.AddWithValue("@MaHocKy", hocKy.MaHocKy);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CapNhatHocKy: {ex.Message}");
                throw;
            }
        }

        public bool XoaHocKy(int maHocKy)
        {
            // (Code hàm XoaHocKy của bạn... giữ nguyên)
            string query = "DELETE FROM HocKy WHERE MaHocKy = @MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                        int result = cmd.ExecuteNonQuery();

                        return result > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error [{ex.Number}]: {ex.Message}");
                throw new Exception($"Lỗi database: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi XoaHocKy: {ex.Message}");
                throw;
            }
        }

        public List<HocKyDTO> LayDanhSachHocKyTheoNamHoc(string maNamHoc)
        {
            // (Code hàm LayDanhSachHocKyTheoNamHoc của bạn... giữ nguyên)
            List<HocKyDTO> ds = new List<HocKyDTO>();
            string query = "SELECT MaHocKy, TenHocKy, MaNamHoc, NgayBD, NgayKT, TrangThai FROM HocKy WHERE MaNamHoc=@MaNamHoc ORDER BY NgayBD";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HocKyDTO hk = new HocKyDTO
                                {
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    TenHocKy = reader.GetString("TenHocKy"),
                                    MaNamHoc = reader.GetString("MaNamHoc"),
                                    NgayBD = reader.GetDateTime("NgayBD"),
                                    NgayKT = reader.GetDateTime("NgayKT"),
                                    TrangThai = reader.GetString("TrangThai")
                                };
                                ds.Add(hk);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayDanhSachHocKyTheoNamHoc: {ex.Message}");
                throw;
            }

            return ds;
        }

        public List<HocKyDTO> GetAllHocKy()
        {
            // (Code hàm GetAllHocKy của bạn... giữ nguyên)
            List<HocKyDTO> list = new List<HocKyDTO>();
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
                        SELECT MaHocKy, TenHocKy, MaNamHoc, TrangThai, NgayBD, NgayKT
                        FROM HocKy
                        ORDER BY MaHocKy DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HocKyDTO hk = new HocKyDTO
                            {
                                MaHocKy = Convert.ToInt32(reader["MaHocKy"]),
                                TenHocKy = reader["TenHocKy"].ToString(),
                                MaNamHoc = reader["MaNamHoc"].ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                NgayBD = reader["NgayBD"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["NgayBD"]) : (DateTime?)null,
                                NgayKT = reader["NgayKT"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["NgayKT"]) : (DateTime?)null
                            };
                            list.Add(hk);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách học kỳ: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }

            return list;
        }

        // === BẮT ĐẦU HÀM MỚI (ĐÃ SỬA LẠI) ===

        /// <summary>
        /// Lấy thông tin năm học theo mã.
        /// </summary>
        /// <param name="maNamHoc">Mã của năm học (ví dụ: '2025-2026')</param>
        /// <returns>Đối tượng NamHocDTO hoặc null nếu không tìm thấy.</returns>
        public NamHocDTO LayNamHocTheoMa(string maNamHoc)
        {
            NamHocDTO namHoc = null;
            string query = "SELECT MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc FROM NamHoc WHERE MaNamHoc = @maNamHoc";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@maNamHoc", maNamHoc);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                namHoc = new NamHocDTO();

                                namHoc.MaNamHoc = reader.GetString("MaNamHoc");
                                namHoc.TenNamHoc = reader.GetString("TenNamHoc");

                                // SỬA LẠI: Đọc GetDateTime() để khớp với NamHocDTO (không phải nullable)
                                // Giả định rằng NgayBatDau/NgayKetThuc trong CSDL không bao giờ NULL
                                if (!reader.IsDBNull(reader.GetOrdinal("NgayBatDau")))
                                    namHoc.NgayBD = reader.GetDateTime("NgayBatDau");

                                if (!reader.IsDBNull(reader.GetOrdinal("NgayKetThuc")))
                                    namHoc.NgayKT = reader.GetDateTime("NgayKetThuc");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayNamHocTheoMa: {ex.Message}");
                throw; // Ném lỗi để BLL xử lý
            }
            return namHoc;
        }

        /// <summary>
        /// Thêm một năm học mới vào CSDL.
        /// </summary>
        /// <returns>True nếu thêm thành công, False nếu thất bại.</returns>
        public bool ThemNamHoc(NamHocDTO namHoc) // SỬA LẠI: Nhận 1 tham số NamHocDTO
        {
            string query = "INSERT INTO NamHoc (MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc) VALUES (@maNamHoc, @tenNamHoc, @ngayBD, @ngayKT)";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // "Giải nén" DTO
                        cmd.Parameters.AddWithValue("@maNamHoc", namHoc.MaNamHoc);
                        cmd.Parameters.AddWithValue("@tenNamHoc", namHoc.TenNamHoc);
                        cmd.Parameters.AddWithValue("@ngayBD", namHoc.NgayBD);
                        cmd.Parameters.AddWithValue("@ngayKT", namHoc.NgayKT);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi ThemNamHoc: {ex.Message}");
                throw; // Ném lỗi để BLL xử lý
            }
        }

        /// <summary>
        /// Tìm học kỳ theo tên học kỳ và mã năm học.
        /// </summary>
        /// <param name="tenHocKy">Tên học kỳ (ví dụ: "Học kỳ I", "Học kỳ II")</param>
        /// <param name="maNamHoc">Mã năm học (ví dụ: "2024-2025")</param>
        /// <returns>HocKyDTO nếu tìm thấy, null nếu không tìm thấy</returns>
        public HocKyDTO LayHocKyTheoTenVaNamHoc(string tenHocKy, string maNamHoc)
        {
            HocKyDTO hocKy = null;
            string query = "SELECT MaHocKy, TenHocKy, MaNamHoc, NgayBD, NgayKT, TrangThai FROM HocKy WHERE TenHocKy = @TenHocKy AND MaNamHoc = @MaNamHoc";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenHocKy", tenHocKy);
                        cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hocKy = new HocKyDTO
                                {
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    TenHocKy = reader.GetString("TenHocKy"),
                                    MaNamHoc = reader.GetString("MaNamHoc"),
                                    NgayBD = reader.IsDBNull(reader.GetOrdinal("NgayBD")) ? (DateTime?)null : reader.GetDateTime("NgayBD"),
                                    NgayKT = reader.IsDBNull(reader.GetOrdinal("NgayKT")) ? (DateTime?)null : reader.GetDateTime("NgayKT"),
                                    TrangThai = reader.GetString("TrangThai")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayHocKyTheoTenVaNamHoc: {ex.Message}");
                throw;
            }

            return hocKy;
        }

        // === KẾT THÚC HÀM MỚI ===

        /// <summary>
        /// Kiểm tra học kỳ có dữ liệu xếp loại hay không
        /// </summary>
        public bool KiemTraHocKyCoXepLoai(int maHocKy)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
            SELECT COUNT(DISTINCT hs.MaHocSinh) as SoLuong
            FROM HocSinh hs
            INNER JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh 
                AND pl.MaHocKy = @MaHocKy
            LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh 
                AND ds.MaHocKy = @MaHocKy
            WHERE hs.TrangThai = 'Đang học'
                AND ds.DiemTrungBinh IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                    object result = cmd.ExecuteScalar();
                    int soLuong = result != null ? Convert.ToInt32(result) : 0;
                    return soLuong > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi KiemTraHocKyCoXepLoai: {ex.Message}");
                return false;
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Lấy học kỳ mới nhất có dữ liệu điểm số
        /// Thứ tự ưu tiên: Năm học mới nhất → Học kỳ II → Học kỳ I
        /// </summary>
        public HocKyDTO LayHocKyMoiNhatCoDuLieu()
        {
            HocKyDTO hocKy = null;
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                // ✅ SỬA LẠI: Theo yêu cầu, học kỳ mới nhất có dữ liệu là MaHocKy=1
                // Logic: Sắp xếp theo ngày kết thúc học kỳ (NgayKT) DESC để lấy học kỳ kết thúc muộn nhất có dữ liệu
                // Nếu không có NgayKT thì dùng NgayBD
                // Điều này sẽ đảm bảo chọn học kỳ có dữ liệu điểm số mới nhất theo thời gian thực tế
                string query = @"
            SELECT DISTINCT hk.MaHocKy, hk.TenHocKy, hk.MaNamHoc, hk.TrangThai, hk.NgayBD, hk.NgayKT,
                   nh.NgayBatDau
            FROM HocKy hk
            INNER JOIN NamHoc nh ON hk.MaNamHoc = nh.MaNamHoc
            WHERE EXISTS (
                SELECT 1 
                FROM DiemSo ds 
                WHERE ds.MaHocKy = hk.MaHocKy 
                    AND ds.DiemTrungBinh IS NOT NULL
            )
            ORDER BY 
                -- Ưu tiên học kỳ đang diễn ra (TrangThai = 'Đang diễn ra')
                CASE WHEN hk.TrangThai = 'Đang diễn ra' THEN 0 ELSE 1 END ASC,
                -- Sau đó sắp xếp theo ngày kết thúc (hoặc ngày bắt đầu nếu không có ngày kết thúc)
                COALESCE(hk.NgayKT, hk.NgayBD) DESC,
                -- Cuối cùng sắp xếp theo MaHocKy DESC để đảm bảo tính nhất quán
                hk.MaHocKy DESC
            LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int maHocKy = Convert.ToInt32(reader["MaHocKy"]);
                            string tenHocKy = reader["TenHocKy"].ToString();
                            string maNamHoc = reader["MaNamHoc"].ToString();
                            
                            // ✅ DEBUG: In ra để kiểm tra
                            Console.WriteLine($"[LayHocKyMoiNhatCoDuLieu] Tìm thấy: MaHocKy={maHocKy}, TenHocKy={tenHocKy}, MaNamHoc={maNamHoc}");
                            
                            hocKy = new HocKyDTO
                            {
                                MaHocKy = maHocKy,
                                TenHocKy = tenHocKy,
                                MaNamHoc = maNamHoc,
                                TrangThai = reader["TrangThai"].ToString(),
                                NgayBD = reader["NgayBD"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["NgayBD"]) : (DateTime?)null,
                                NgayKT = reader["NgayKT"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["NgayKT"]) : (DateTime?)null
                            };
                        }
                        else
                        {
                            Console.WriteLine("[LayHocKyMoiNhatCoDuLieu] Không tìm thấy học kỳ nào có dữ liệu điểm số");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayHocKyMoiNhatCoDuLieu: {ex.Message}");
                throw;
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }

            return hocKy;
        }

        /// <summary>
        /// Kiểm tra học kỳ có dữ liệu điểm số thực sự hay không
        /// </summary>
        public bool KiemTraHocKyCoDiemSo(int maHocKy)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
            SELECT COUNT(*) as SoLuong
            FROM DiemSo
            WHERE MaHocKy = @MaHocKy
                AND DiemTrungBinh IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                    object result = cmd.ExecuteScalar();
                    int soLuong = result != null ? Convert.ToInt32(result) : 0;
                    
                    // ✅ DEBUG: In ra số lượng điểm số
                    Console.WriteLine($"[KiemTraHocKyCoDiemSo] MaHocKy={maHocKy}, SoLuong={soLuong}");
                    
                    return soLuong > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi KiemTraHocKyCoDiemSo: {ex.Message}");
                return false;
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Lấy học kỳ I đầu tiên (theo thứ tự thời gian) - dùng khi không có học kỳ nào có dữ liệu điểm số
        /// Theo yêu cầu: chọn "Học kì I 2025-2026" (học kỳ đầu tiên)
        /// </summary>
        public HocKyDTO LayHocKyIDauTienCuaNamHocMoiNhat()
        {
            HocKyDTO hocKy = null;
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                // ✅ SỬA LẠI: Lấy học kỳ I đầu tiên theo thứ tự thời gian (NgayBatDau ASC, MaHocKy ASC)
                string query = @"
            SELECT hk.MaHocKy, hk.TenHocKy, hk.MaNamHoc, hk.TrangThai, hk.NgayBD, hk.NgayKT,
                   nh.NgayBatDau
            FROM HocKy hk
            INNER JOIN NamHoc nh ON hk.MaNamHoc = nh.MaNamHoc
            WHERE hk.TenHocKy LIKE '%I%'
                AND hk.TenHocKy NOT LIKE '%II%'
                AND hk.TenHocKy NOT LIKE '%2%'
            ORDER BY nh.NgayBatDau ASC, hk.MaHocKy ASC
            LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int maHocKy = Convert.ToInt32(reader["MaHocKy"]);
                            string tenHocKy = reader["TenHocKy"].ToString();
                            string maNamHoc = reader["MaNamHoc"].ToString();
                            
                            // ✅ DEBUG: In ra để kiểm tra
                            Console.WriteLine($"[LayHocKyIDauTienCuaNamHocMoiNhat] Tìm thấy: MaHocKy={maHocKy}, TenHocKy={tenHocKy}, MaNamHoc={maNamHoc}");
                            
                            hocKy = new HocKyDTO
                            {
                                MaHocKy = maHocKy,
                                TenHocKy = tenHocKy,
                                MaNamHoc = maNamHoc,
                                TrangThai = reader["TrangThai"].ToString(),
                                NgayBD = reader["NgayBD"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["NgayBD"]) : (DateTime?)null,
                                NgayKT = reader["NgayKT"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["NgayKT"]) : (DateTime?)null
                            };
                        }
                        else
                        {
                            Console.WriteLine("[LayHocKyIDauTienCuaNamHocMoiNhat] Không tìm thấy học kỳ I nào");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayHocKyIDauTienCuaNamHocMoiNhat: {ex.Message}");
                throw;
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }

            return hocKy;
        }

    }
}
