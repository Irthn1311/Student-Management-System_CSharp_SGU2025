using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    /// <summary>
    /// Data Access Object cho module Thông Báo
    /// </summary>
    public class ThongBaoDAO
    {
        #region CRUD Thông Báo

        /// <summary>
        /// Thêm thông báo mới
        /// </summary>
        public int ThemThongBao(ThongBaoDTO tb)
        {
            string sql = @"INSERT INTO ThongBao 
                (TieuDe, NoiDung, NgayTao, LoaiThongBao, DoiTuongNhan, NgayHetHan, 
                 MaNguoiTao, PhamVi, MaVaiTroNhan, MaLop, MaKhoi, DoUuTien, TrangThai)
                VALUES 
                (@tieuDe, @noiDung, @ngayTao, @loaiThongBao, @doiTuongNhan, @ngayHetHan,
                 @maNguoiTao, @phamVi, @maVaiTroNhan, @maLop, @maKhoi, @doUuTien, @trangThai);
                SELECT LAST_INSERT_ID();";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tieuDe", tb.TieuDe);
                        cmd.Parameters.AddWithValue("@noiDung", (object)tb.NoiDung ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ngayTao", tb.NgayTao);
                        cmd.Parameters.AddWithValue("@loaiThongBao", tb.LoaiThongBao);
                        cmd.Parameters.AddWithValue("@doiTuongNhan", tb.DoiTuongNhan ?? "");
                        cmd.Parameters.AddWithValue("@ngayHetHan", (object)tb.NgayHetHan ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@maNguoiTao", tb.MaNguoiTao);
                        cmd.Parameters.AddWithValue("@phamVi", tb.PhamVi ?? "ALL");
                        cmd.Parameters.AddWithValue("@maVaiTroNhan", (object)tb.MaVaiTroNhan ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@maLop", (object)tb.MaLop ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@maKhoi", (object)tb.MaKhoi ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@doUuTien", tb.DoUuTien ?? "BINH_THUONG");
                        cmd.Parameters.AddWithValue("@trangThai", tb.TrangThai ?? "HIEN_THI");

                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi thêm thông báo: " + ex.Message);
                    return -1;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Cập nhật thông báo
        /// </summary>
        public bool CapNhatThongBao(ThongBaoDTO tb)
        {
            string sql = @"UPDATE ThongBao SET 
                TieuDe = @tieuDe, NoiDung = @noiDung, LoaiThongBao = @loaiThongBao,
                DoiTuongNhan = @doiTuongNhan, NgayHetHan = @ngayHetHan, PhamVi = @phamVi,
                MaVaiTroNhan = @maVaiTroNhan, MaLop = @maLop, MaKhoi = @maKhoi,
                DoUuTien = @doUuTien, TrangThai = @trangThai
                WHERE MaThongBao = @maThongBao";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", tb.MaThongBao);
                        cmd.Parameters.AddWithValue("@tieuDe", tb.TieuDe);
                        cmd.Parameters.AddWithValue("@noiDung", (object)tb.NoiDung ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@loaiThongBao", tb.LoaiThongBao);
                        cmd.Parameters.AddWithValue("@doiTuongNhan", tb.DoiTuongNhan ?? "");
                        cmd.Parameters.AddWithValue("@ngayHetHan", (object)tb.NgayHetHan ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@phamVi", tb.PhamVi ?? "ALL");
                        cmd.Parameters.AddWithValue("@maVaiTroNhan", (object)tb.MaVaiTroNhan ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@maLop", (object)tb.MaLop ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@maKhoi", (object)tb.MaKhoi ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@doUuTien", tb.DoUuTien ?? "BINH_THUONG");
                        cmd.Parameters.AddWithValue("@trangThai", tb.TrangThai ?? "HIEN_THI");

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi cập nhật thông báo: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Xóa thông báo (soft delete)
        /// </summary>
        public bool XoaThongBao(int maThongBao)
        {
            string sql = "UPDATE ThongBao SET TrangThai = 'DA_XOA' WHERE MaThongBao = @maThongBao";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi xóa thông báo: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Xóa thông báo vĩnh viễn (hard delete)
        /// </summary>
        public bool XoaVinhVienThongBao(int maThongBao)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    
                    // Xóa người nhận trước
                    string sqlNguoiNhan = "DELETE FROM NguoiNhanThongBao WHERE MaThongBao = @maThongBao";
                    using (MySqlCommand cmd = new MySqlCommand(sqlNguoiNhan, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        cmd.ExecuteNonQuery();
                    }

                    // Xóa thông báo
                    string sqlThongBao = "DELETE FROM ThongBao WHERE MaThongBao = @maThongBao";
                    using (MySqlCommand cmd = new MySqlCommand(sqlThongBao, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi xóa vĩnh viễn thông báo: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy thông báo theo mã
        /// </summary>
        public ThongBaoDTO LayThongBaoTheoMa(int maThongBao)
        {
            string sql = @"SELECT tb.*, 
                COALESCE(gv.HoTen, hs.HoTen, tb.MaNguoiTao) as TenNguoiTao,
                lh.TenLop, kl.TenKhoi,
                ltb.TenLoai as TenLoaiThongBao, ltb.MauSac as MauSacLoai
                FROM ThongBao tb
                LEFT JOIN GiaoVien gv ON tb.MaNguoiTao = gv.MaGiaoVien
                LEFT JOIN HocSinh hs ON tb.MaNguoiTao = hs.TenDangNhap
                LEFT JOIN LopHoc lh ON tb.MaLop = lh.MaLop
                LEFT JOIN KhoiLop kl ON tb.MaKhoi = kl.MaKhoi
                LEFT JOIN LoaiThongBao ltb ON tb.LoaiThongBao = ltb.MaLoai
                WHERE tb.MaThongBao = @maThongBao";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapThongBaoFromReader(reader);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy thông báo: " + ex.Message);
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return null;
        }

        #endregion

        #region Lấy danh sách thông báo

        /// <summary>
        /// Lấy tất cả thông báo (Admin view)
        /// </summary>
        public List<ThongBaoDTO> LayTatCaThongBao(string trangThai = "HIEN_THI")
        {
            List<ThongBaoDTO> ds = new List<ThongBaoDTO>();
            string sql = @"SELECT tb.*, 
                COALESCE(gv.HoTen, hs.HoTen, tb.MaNguoiTao) as TenNguoiTao,
                lh.TenLop, kl.TenKhoi,
                ltb.TenLoai as TenLoaiThongBao, ltb.MauSac as MauSacLoai,
                (SELECT COUNT(*) FROM NguoiNhanThongBao WHERE MaThongBao = tb.MaThongBao AND DaDoc = TRUE) as SoNguoiDaDoc,
                (SELECT COUNT(*) FROM NguoiNhanThongBao WHERE MaThongBao = tb.MaThongBao) as TongNguoiNhan
                FROM ThongBao tb
                LEFT JOIN GiaoVien gv ON tb.MaNguoiTao = gv.MaGiaoVien
                LEFT JOIN HocSinh hs ON tb.MaNguoiTao = hs.TenDangNhap
                LEFT JOIN LopHoc lh ON tb.MaLop = lh.MaLop
                LEFT JOIN KhoiLop kl ON tb.MaKhoi = kl.MaKhoi
                LEFT JOIN LoaiThongBao ltb ON tb.LoaiThongBao = ltb.MaLoai
                WHERE tb.TrangThai = @trangThai
                ORDER BY tb.NgayTao DESC";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@trangThai", trangThai);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ds.Add(MapThongBaoFromReader(reader));
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy danh sách thông báo: " + ex.Message);
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return ds;
        }

        /// <summary>
        /// Lấy thông báo của người dùng (User view)
        /// </summary>
        public List<ThongBaoDTO> LayThongBaoNguoiDung(string tenDangNhap, bool chiChuaDoc = false)
        {
            List<ThongBaoDTO> ds = new List<ThongBaoDTO>();
            string sql = @"SELECT tb.*, 
                COALESCE(gv.HoTen, hs.HoTen, tb.MaNguoiTao) as TenNguoiTao,
                lh.TenLop, kl.TenKhoi,
                ltb.TenLoai as TenLoaiThongBao, ltb.MauSac as MauSacLoai,
                nn.DaDoc, nn.NgayDoc
                FROM ThongBao tb
                INNER JOIN NguoiNhanThongBao nn ON tb.MaThongBao = nn.MaThongBao
                LEFT JOIN GiaoVien gv ON tb.MaNguoiTao = gv.MaGiaoVien
                LEFT JOIN HocSinh hs ON tb.MaNguoiTao = hs.TenDangNhap
                LEFT JOIN LopHoc lh ON tb.MaLop = lh.MaLop
                LEFT JOIN KhoiLop kl ON tb.MaKhoi = kl.MaKhoi
                LEFT JOIN LoaiThongBao ltb ON tb.LoaiThongBao = ltb.MaLoai
                WHERE nn.TenDangNhap = @tenDangNhap 
                AND nn.DaXoa = FALSE 
                AND tb.TrangThai = 'HIEN_THI'";

            if (chiChuaDoc)
                sql += " AND nn.DaDoc = FALSE";

            sql += " ORDER BY tb.NgayTao DESC";

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
                            while (reader.Read())
                            {
                                var tb = MapThongBaoFromReader(reader);
                                tb.DaDoc = reader.GetBoolean("DaDoc");
                                tb.NgayDoc = reader.IsDBNull(reader.GetOrdinal("NgayDoc")) 
                                    ? (DateTime?)null 
                                    : reader.GetDateTime("NgayDoc");
                                ds.Add(tb);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy thông báo người dùng: " + ex.Message);
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return ds;
        }

        /// <summary>
        /// Lấy thông báo do người dùng tạo
        /// </summary>
        public List<ThongBaoDTO> LayThongBaoDaTao(string maNguoiTao)
        {
            List<ThongBaoDTO> ds = new List<ThongBaoDTO>();
            string sql = @"SELECT tb.*, 
                COALESCE(gv.HoTen, hs.HoTen, tb.MaNguoiTao) as TenNguoiTao,
                lh.TenLop, kl.TenKhoi,
                ltb.TenLoai as TenLoaiThongBao, ltb.MauSac as MauSacLoai,
                (SELECT COUNT(*) FROM NguoiNhanThongBao WHERE MaThongBao = tb.MaThongBao AND DaDoc = TRUE) as SoNguoiDaDoc,
                (SELECT COUNT(*) FROM NguoiNhanThongBao WHERE MaThongBao = tb.MaThongBao) as TongNguoiNhan
                FROM ThongBao tb
                LEFT JOIN GiaoVien gv ON tb.MaNguoiTao = gv.MaGiaoVien
                LEFT JOIN HocSinh hs ON tb.MaNguoiTao = hs.TenDangNhap
                LEFT JOIN LopHoc lh ON tb.MaLop = lh.MaLop
                LEFT JOIN KhoiLop kl ON tb.MaKhoi = kl.MaKhoi
                LEFT JOIN LoaiThongBao ltb ON tb.LoaiThongBao = ltb.MaLoai
                WHERE tb.MaNguoiTao = @maNguoiTao AND tb.TrangThai != 'DA_XOA'
                ORDER BY tb.NgayTao DESC";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maNguoiTao", maNguoiTao);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ds.Add(MapThongBaoFromReader(reader));
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy thông báo đã tạo: " + ex.Message);
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return ds;
        }

        #endregion

        #region Quản lý người nhận

        /// <summary>
        /// Thêm người nhận thông báo
        /// </summary>
        public bool ThemNguoiNhan(int maThongBao, string tenDangNhap)
        {
            string sql = @"INSERT IGNORE INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayNhan)
                VALUES (@maThongBao, @tenDangNhap, FALSE, NOW())";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi thêm người nhận: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Thêm nhiều người nhận
        /// </summary>
        public int ThemNhieuNguoiNhan(int maThongBao, List<string> dsTenDangNhap)
        {
            int count = 0;
            string sql = @"INSERT IGNORE INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayNhan)
                VALUES (@maThongBao, @tenDangNhap, FALSE, NOW())";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    foreach (var tenDangNhap in dsTenDangNhap)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                            cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi thêm nhiều người nhận: " + ex.Message);
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return count;
        }

        /// <summary>
        /// Gửi thông báo theo vai trò
        /// </summary>
        public int GuiTheoVaiTro(int maThongBao, string maVaiTro)
        {
            string sql = @"INSERT IGNORE INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayNhan)
                SELECT @maThongBao, ndvt.TenDangNhap, FALSE, NOW()
                FROM NguoiDungVaiTro ndvt
                JOIN NguoiDung nd ON ndvt.TenDangNhap = nd.TenDangNhap
                WHERE ndvt.MaVaiTro = @maVaiTro AND nd.TrangThai = 'Hoạt động'";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        cmd.Parameters.AddWithValue("@maVaiTro", maVaiTro);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi gửi theo vai trò: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Gửi thông báo theo lớp
        /// </summary>
        public int GuiTheoLop(int maThongBao, int maLop, int maHocKy)
        {
            string sql = @"INSERT IGNORE INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayNhan)
                SELECT @maThongBao, hs.TenDangNhap, FALSE, NOW()
                FROM PhanLop pl
                JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
                WHERE pl.MaLop = @maLop AND pl.MaHocKy = @maHocKy 
                AND hs.TenDangNhap IS NOT NULL";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        cmd.Parameters.AddWithValue("@maLop", maLop);
                        cmd.Parameters.AddWithValue("@maHocKy", maHocKy);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi gửi theo lớp: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Gửi thông báo theo khối
        /// </summary>
        public int GuiTheoKhoi(int maThongBao, int maKhoi, int maHocKy)
        {
            string sql = @"INSERT IGNORE INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayNhan)
                SELECT DISTINCT @maThongBao, hs.TenDangNhap, FALSE, NOW()
                FROM PhanLop pl
                JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
                JOIN LopHoc lh ON pl.MaLop = lh.MaLop
                WHERE lh.MaKhoi = @maKhoi AND pl.MaHocKy = @maHocKy 
                AND hs.TenDangNhap IS NOT NULL";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        cmd.Parameters.AddWithValue("@maKhoi", maKhoi);
                        cmd.Parameters.AddWithValue("@maHocKy", maHocKy);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi gửi theo khối: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Gửi thông báo toàn trường
        /// </summary>
        public int GuiToanTruong(int maThongBao)
        {
            string sql = @"INSERT IGNORE INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayNhan)
                SELECT @maThongBao, nd.TenDangNhap, FALSE, NOW()
                FROM NguoiDung nd
                WHERE nd.TrangThai = 'Hoạt động'";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi gửi toàn trường: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Đánh dấu đã đọc
        /// </summary>
        public bool DanhDauDaDoc(int maThongBao, string tenDangNhap)
        {
            string sql = @"UPDATE NguoiNhanThongBao 
                SET DaDoc = TRUE, NgayDoc = NOW() 
                WHERE MaThongBao = @maThongBao AND TenDangNhap = @tenDangNhap";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đánh dấu đã đọc: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Đánh dấu tất cả đã đọc
        /// </summary>
        public int DanhDauTatCaDaDoc(string tenDangNhap)
        {
            string sql = @"UPDATE NguoiNhanThongBao 
                SET DaDoc = TRUE, NgayDoc = NOW() 
                WHERE TenDangNhap = @tenDangNhap AND DaDoc = FALSE";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đánh dấu tất cả đã đọc: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        #endregion

        #region Thống kê

        /// <summary>
        /// Đếm số thông báo chưa đọc
        /// </summary>
        public int DemChuaDoc(string tenDangNhap)
        {
            string sql = @"SELECT COUNT(*) FROM NguoiNhanThongBao nn
                JOIN ThongBao tb ON nn.MaThongBao = tb.MaThongBao
                WHERE nn.TenDangNhap = @tenDangNhap 
                AND nn.DaDoc = FALSE AND nn.DaXoa = FALSE
                AND tb.TrangThai = 'HIEN_THI'";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đếm chưa đọc: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy thống kê thông báo
        /// </summary>
        public ThongKeThongBaoDTO LayThongKe(string tenDangNhap = null)
        {
            var tk = new ThongKeThongBaoDTO();
            
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Tổng thông báo
                    string sqlTong = "SELECT COUNT(*) FROM ThongBao WHERE TrangThai = 'HIEN_THI'";
                    using (MySqlCommand cmd = new MySqlCommand(sqlTong, conn))
                    {
                        tk.TongThongBao = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Theo phạm vi
                    string sqlPhamVi = @"SELECT 
                        SUM(CASE WHEN MaVaiTroNhan = 'teacher' THEN 1 ELSE 0 END) as GuiGV,
                        SUM(CASE WHEN MaVaiTroNhan = 'student' THEN 1 ELSE 0 END) as GuiHS,
                        SUM(CASE WHEN PhamVi = 'ALL' THEN 1 ELSE 0 END) as GuiAll
                        FROM ThongBao WHERE TrangThai = 'HIEN_THI'";
                    using (MySqlCommand cmd = new MySqlCommand(sqlPhamVi, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tk.GuiGiaoVien = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                tk.GuiHocSinh = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                                tk.GuiToanTruong = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            }
                        }
                    }

                    // Theo độ ưu tiên
                    string sqlUuTien = @"SELECT 
                        SUM(CASE WHEN DoUuTien = 'KHAN_CAP' THEN 1 ELSE 0 END) as KhanCap,
                        SUM(CASE WHEN DoUuTien = 'QUAN_TRONG' THEN 1 ELSE 0 END) as QuanTrong
                        FROM ThongBao WHERE TrangThai = 'HIEN_THI'";
                    using (MySqlCommand cmd = new MySqlCommand(sqlUuTien, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tk.KhanCap = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                tk.QuanTrong = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            }
                        }
                    }

                    // Nếu có user, lấy thống kê đọc
                    if (!string.IsNullOrEmpty(tenDangNhap))
                    {
                        string sqlDoc = @"SELECT 
                            SUM(CASE WHEN DaDoc = TRUE THEN 1 ELSE 0 END) as DaDoc,
                            SUM(CASE WHEN DaDoc = FALSE THEN 1 ELSE 0 END) as ChuaDoc
                            FROM NguoiNhanThongBao nn
                            JOIN ThongBao tb ON nn.MaThongBao = tb.MaThongBao
                            WHERE nn.TenDangNhap = @tenDangNhap 
                            AND nn.DaXoa = FALSE AND tb.TrangThai = 'HIEN_THI'";
                        using (MySqlCommand cmd = new MySqlCommand(sqlDoc, conn))
                        {
                            cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    tk.DaDoc = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                    tk.ChuaDoc = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                                }
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy thống kê: " + ex.Message);
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return tk;
        }

        #endregion

        #region Loại thông báo

        /// <summary>
        /// Lấy danh sách loại thông báo
        /// </summary>
        public List<LoaiThongBaoDTO> LayDanhSachLoaiThongBao()
        {
            List<LoaiThongBaoDTO> ds = new List<LoaiThongBaoDTO>();
            string sql = "SELECT * FROM LoaiThongBao ORDER BY ThuTu";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ds.Add(new LoaiThongBaoDTO
                                {
                                    MaLoai = reader.GetString("MaLoai"),
                                    TenLoai = reader.GetString("TenLoai"),
                                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString("MoTa"),
                                    MauSac = reader.IsDBNull(reader.GetOrdinal("MauSac")) ? "#3B82F6" : reader.GetString("MauSac"),
                                    Icon = reader.IsDBNull(reader.GetOrdinal("Icon")) ? null : reader.GetString("Icon"),
                                    ThuTu = reader.GetInt32("ThuTu")
                                });
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy loại thông báo: " + ex.Message);
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return ds;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Map dữ liệu từ Reader sang DTO
        /// </summary>
        private ThongBaoDTO MapThongBaoFromReader(MySqlDataReader reader)
        {
            var tb = new ThongBaoDTO
            {
                MaThongBao = reader.GetInt32("MaThongBao"),
                TieuDe = reader.GetString("TieuDe"),
                NoiDung = reader.IsDBNull(reader.GetOrdinal("NoiDung")) ? null : reader.GetString("NoiDung"),
                NgayTao = reader.GetDateTime("NgayTao"),
                LoaiThongBao = reader.GetString("LoaiThongBao"),
                DoiTuongNhan = reader.IsDBNull(reader.GetOrdinal("DoiTuongNhan")) ? "" : reader.GetString("DoiTuongNhan"),
                NgayHetHan = reader.IsDBNull(reader.GetOrdinal("NgayHetHan")) ? (DateTime?)null : reader.GetDateTime("NgayHetHan"),
                MaNguoiTao = reader.IsDBNull(reader.GetOrdinal("MaNguoiTao")) ? null : reader.GetString("MaNguoiTao")
            };

            // Extended columns (có thể không tồn tại)
            try
            {
                tb.PhamVi = reader.IsDBNull(reader.GetOrdinal("PhamVi")) ? "ALL" : reader.GetString("PhamVi");
                tb.MaVaiTroNhan = reader.IsDBNull(reader.GetOrdinal("MaVaiTroNhan")) ? null : reader.GetString("MaVaiTroNhan");
                tb.MaLop = reader.IsDBNull(reader.GetOrdinal("MaLop")) ? (int?)null : reader.GetInt32("MaLop");
                tb.MaKhoi = reader.IsDBNull(reader.GetOrdinal("MaKhoi")) ? (int?)null : reader.GetInt32("MaKhoi");
                tb.DoUuTien = reader.IsDBNull(reader.GetOrdinal("DoUuTien")) ? "BINH_THUONG" : reader.GetString("DoUuTien");
                tb.TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? "HIEN_THI" : reader.GetString("TrangThai");
            }
            catch { }

            // JOIN columns
            try
            {
                tb.TenNguoiTao = reader.IsDBNull(reader.GetOrdinal("TenNguoiTao")) ? tb.MaNguoiTao : reader.GetString("TenNguoiTao");
                tb.TenLop = reader.IsDBNull(reader.GetOrdinal("TenLop")) ? null : reader.GetString("TenLop");
                tb.TenKhoi = reader.IsDBNull(reader.GetOrdinal("TenKhoi")) ? null : reader.GetString("TenKhoi");
                tb.TenLoaiThongBao = reader.IsDBNull(reader.GetOrdinal("TenLoaiThongBao")) ? tb.LoaiThongBao : reader.GetString("TenLoaiThongBao");
                tb.MauSacLoai = reader.IsDBNull(reader.GetOrdinal("MauSacLoai")) ? "#3B82F6" : reader.GetString("MauSacLoai");
            }
            catch { }

            // Thống kê
            try
            {
                tb.SoNguoiDaDoc = reader.IsDBNull(reader.GetOrdinal("SoNguoiDaDoc")) ? 0 : reader.GetInt32("SoNguoiDaDoc");
                tb.TongNguoiNhan = reader.IsDBNull(reader.GetOrdinal("TongNguoiNhan")) ? 0 : reader.GetInt32("TongNguoiNhan");
            }
            catch { }

            return tb;
        }

        #endregion

        #region Wrapper Methods (cho BUS layer)

        /// <summary>
        /// Lấy danh sách thông báo theo người dùng với filter và phân trang
        /// </summary>
        public List<ThongBaoDTO> LayDanhSachThongBaoTheoNguoiDung(
            string tenDangNhap,
            string loaiThongBao = null,
            bool? daDoc = null,
            string timKiem = null,
            int pageNumber = 1,
            int pageSize = 50)
        {
            // Lấy tất cả thông báo trước, sau đó filter ở client
            var danhSach = LayThongBaoNguoiDung(tenDangNhap, false);
            
            // Filter theo trạng thái đọc
            if (daDoc.HasValue)
            {
                danhSach = danhSach.Where(tb => tb.DaDoc == daDoc.Value).ToList();
            }

            // Lọc theo loại
            if (!string.IsNullOrEmpty(loaiThongBao))
            {
                danhSach = danhSach.Where(tb => tb.LoaiThongBao == loaiThongBao).ToList();
            }

            // Tìm kiếm
            if (!string.IsNullOrWhiteSpace(timKiem))
            {
                string keyword = timKiem.ToLower();
                danhSach = danhSach.Where(tb =>
                    tb.TieuDe.ToLower().Contains(keyword) ||
                    (tb.NoiDung != null && tb.NoiDung.ToLower().Contains(keyword)) ||
                    (tb.DoiTuongNhan != null && tb.DoiTuongNhan.ToLower().Contains(keyword))
                ).ToList();
            }

            // Phân trang
            return danhSach.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Lấy danh sách thông báo đã gửi với filter và phân trang
        /// </summary>
        public List<ThongBaoDTO> LayDanhSachThongBaoDaGui(
            string tenDangNhap,
            string loaiThongBao = null,
            string timKiem = null,
            int pageNumber = 1,
            int pageSize = 50)
        {
            var danhSach = LayThongBaoDaTao(tenDangNhap);

            // Lọc theo loại
            if (!string.IsNullOrEmpty(loaiThongBao))
            {
                danhSach = danhSach.Where(tb => tb.LoaiThongBao == loaiThongBao).ToList();
            }

            // Tìm kiếm
            if (!string.IsNullOrWhiteSpace(timKiem))
            {
                string keyword = timKiem.ToLower();
                danhSach = danhSach.Where(tb =>
                    tb.TieuDe.ToLower().Contains(keyword) ||
                    (tb.NoiDung != null && tb.NoiDung.ToLower().Contains(keyword)) ||
                    (tb.DoiTuongNhan != null && tb.DoiTuongNhan.ToLower().Contains(keyword))
                ).ToList();
            }

            // Phân trang
            return danhSach.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Lấy chi tiết thông báo kèm thông tin người nhận
        /// </summary>
        public ThongBaoDTO LayThongBaoChiTiet(int maThongBao, string tenDangNhap)
        {
            var tb = LayThongBaoTheoMa(maThongBao);
            if (tb == null) return null;

            // Lấy thông tin đã đọc
            string sql = @"SELECT DaDoc, NgayDoc FROM NguoiNhanThongBao 
                WHERE MaThongBao = @maThongBao AND TenDangNhap = @tenDangNhap";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maThongBao", maThongBao);
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tb.DaDoc = reader.GetBoolean("DaDoc");
                                tb.NgayDoc = reader.IsDBNull(reader.GetOrdinal("NgayDoc"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime("NgayDoc");
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy chi tiết thông báo: " + ex.Message);
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }

            return tb;
        }

        /// <summary>
        /// Đếm số thông báo chưa đọc (alias)
        /// </summary>
        public int DemThongBaoChuaDoc(string tenDangNhap)
        {
            return DemChuaDoc(tenDangNhap);
        }

        /// <summary>
        /// Lấy thống kê thông báo (alias)
        /// </summary>
        public ThongKeThongBaoDTO LayThongKeThongBao(string tenDangNhap)
        {
            return LayThongKe(tenDangNhap);
        }

        /// <summary>
        /// Đếm tổng số thông báo với filter
        /// </summary>
        public int DemTongThongBao(
            string tenDangNhap,
            string loaiThongBao = null,
            bool? daDoc = null,
            string timKiem = null)
        {
            // Lấy tất cả thông báo trước, sau đó filter ở client
            var danhSach = LayThongBaoNguoiDung(tenDangNhap, false);
            
            // Filter theo trạng thái đọc
            if (daDoc.HasValue)
            {
                danhSach = danhSach.Where(tb => tb.DaDoc == daDoc.Value).ToList();
            }

            // Lọc theo loại
            if (!string.IsNullOrEmpty(loaiThongBao))
            {
                danhSach = danhSach.Where(tb => tb.LoaiThongBao == loaiThongBao).ToList();
            }

            // Tìm kiếm
            if (!string.IsNullOrWhiteSpace(timKiem))
            {
                string keyword = timKiem.ToLower();
                danhSach = danhSach.Where(tb =>
                    tb.TieuDe.ToLower().Contains(keyword) ||
                    (tb.NoiDung != null && tb.NoiDung.ToLower().Contains(keyword)) ||
                    (tb.DoiTuongNhan != null && tb.DoiTuongNhan.ToLower().Contains(keyword))
                ).ToList();
            }

            return danhSach.Count;
        }

        #endregion
    }
}
