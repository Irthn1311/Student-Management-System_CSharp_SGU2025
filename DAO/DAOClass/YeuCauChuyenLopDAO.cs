using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class YeuCauChuyenLopDAO
    {
        /// <summary>
        /// Thêm yêu cầu chuyển lớp mới
        /// </summary>
        public bool ThemYeuCau(YeuCauChuyenLopDTO yeuCau)
        {
            string sql = @"INSERT INTO YeuCauChuyenLop 
                (MaHocSinh, MaLopHienTai, MaLopMongMuon, MaHocKy, LyDoYeuCau, NguoiTao, NgayTao, TrangThai) 
                VALUES (@maHocSinh, @maLopHienTai, @maLopMongMuon, @maHocKy, @lyDo, @nguoiTao, @ngayTao, @trangThai)";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHocSinh", yeuCau.MaHocSinh);
                        cmd.Parameters.AddWithValue("@maLopHienTai", yeuCau.MaLopHienTai);
                        cmd.Parameters.AddWithValue("@maLopMongMuon", yeuCau.MaLopMongMuon.HasValue ? (object)yeuCau.MaLopMongMuon.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@maHocKy", yeuCau.MaHocKy);
                        cmd.Parameters.AddWithValue("@lyDo", yeuCau.LyDoYeuCau);
                        cmd.Parameters.AddWithValue("@nguoiTao", yeuCau.NguoiTao);
                        cmd.Parameters.AddWithValue("@ngayTao", yeuCau.NgayTao);
                        cmd.Parameters.AddWithValue("@trangThai", yeuCau.TrangThai);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi thêm yêu cầu chuyển lớp: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy tất cả yêu cầu chuyển lớp (với thông tin join đầy đủ)
        /// </summary>
        public List<YeuCauChuyenLopDTO> LayTatCaYeuCau()
        {
            List<YeuCauChuyenLopDTO> dsYeuCau = new List<YeuCauChuyenLopDTO>();
            string sql = @"
                SELECT 
                    yc.*,
                    hs.HoTen AS TenHocSinh,
                    lh1.TenLop AS TenLopHienTai,
                    lh2.TenLop AS TenLopMongMuon,
                    lh3.TenLop AS TenLopDuocDuyet,
                    hk.TenHocKy,
                    nh.TenNamHoc
                FROM YeuCauChuyenLop yc
                INNER JOIN HocSinh hs ON yc.MaHocSinh = hs.MaHocSinh
                INNER JOIN LopHoc lh1 ON yc.MaLopHienTai = lh1.MaLop
                LEFT JOIN LopHoc lh2 ON yc.MaLopMongMuon = lh2.MaLop
                LEFT JOIN LopHoc lh3 ON yc.MaLopDuocDuyet = lh3.MaLop
                INNER JOIN HocKy hk ON yc.MaHocKy = hk.MaHocKy
                INNER JOIN NamHoc nh ON hk.MaNamHoc = nh.MaNamHoc
                ORDER BY yc.NgayTao DESC";

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
                                dsYeuCau.Add(MapFromReader(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi lấy danh sách yêu cầu: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }

            return dsYeuCau;
        }

        /// <summary>
        /// Lấy yêu cầu theo trạng thái
        /// </summary>
        public List<YeuCauChuyenLopDTO> LayYeuCauTheoTrangThai(string trangThai)
        {
            List<YeuCauChuyenLopDTO> dsYeuCau = new List<YeuCauChuyenLopDTO>();
            string sql = @"
                SELECT 
                    yc.*,
                    hs.HoTen AS TenHocSinh,
                    lh1.TenLop AS TenLopHienTai,
                    lh2.TenLop AS TenLopMongMuon,
                    lh3.TenLop AS TenLopDuocDuyet,
                    hk.TenHocKy,
                    nh.TenNamHoc
                FROM YeuCauChuyenLop yc
                INNER JOIN HocSinh hs ON yc.MaHocSinh = hs.MaHocSinh
                INNER JOIN LopHoc lh1 ON yc.MaLopHienTai = lh1.MaLop
                LEFT JOIN LopHoc lh2 ON yc.MaLopMongMuon = lh2.MaLop
                LEFT JOIN LopHoc lh3 ON yc.MaLopDuocDuyet = lh3.MaLop
                INNER JOIN HocKy hk ON yc.MaHocKy = hk.MaHocKy
                INNER JOIN NamHoc nh ON hk.MaNamHoc = nh.MaNamHoc
                WHERE yc.TrangThai = @trangThai
                ORDER BY yc.NgayTao DESC";

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
                                dsYeuCau.Add(MapFromReader(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi lấy yêu cầu theo trạng thái: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }

            return dsYeuCau;
        }

        /// <summary>
        /// Lấy yêu cầu theo học sinh
        /// </summary>
        public List<YeuCauChuyenLopDTO> LayYeuCauTheoHocSinh(int maHocSinh)
        {
            List<YeuCauChuyenLopDTO> dsYeuCau = new List<YeuCauChuyenLopDTO>();
            string sql = @"
                SELECT 
                    yc.*,
                    hs.HoTen AS TenHocSinh,
                    lh1.TenLop AS TenLopHienTai,
                    lh2.TenLop AS TenLopMongMuon,
                    lh3.TenLop AS TenLopDuocDuyet,
                    hk.TenHocKy,
                    nh.TenNamHoc
                FROM YeuCauChuyenLop yc
                INNER JOIN HocSinh hs ON yc.MaHocSinh = hs.MaHocSinh
                INNER JOIN LopHoc lh1 ON yc.MaLopHienTai = lh1.MaLop
                LEFT JOIN LopHoc lh2 ON yc.MaLopMongMuon = lh2.MaLop
                LEFT JOIN LopHoc lh3 ON yc.MaLopDuocDuyet = lh3.MaLop
                INNER JOIN HocKy hk ON yc.MaHocKy = hk.MaHocKy
                INNER JOIN NamHoc nh ON hk.MaNamHoc = nh.MaNamHoc
                WHERE yc.MaHocSinh = @maHocSinh
                ORDER BY yc.NgayTao DESC";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHocSinh", maHocSinh);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dsYeuCau.Add(MapFromReader(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi lấy yêu cầu theo học sinh: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }

            return dsYeuCau;
        }

        /// <summary>
        /// Lấy yêu cầu theo mã
        /// </summary>
        public YeuCauChuyenLopDTO LayYeuCauTheoMa(int maYeuCau)
        {
            string sql = @"
                SELECT 
                    yc.*,
                    hs.HoTen AS TenHocSinh,
                    lh1.TenLop AS TenLopHienTai,
                    lh2.TenLop AS TenLopMongMuon,
                    lh3.TenLop AS TenLopDuocDuyet,
                    hk.TenHocKy,
                    nh.TenNamHoc
                FROM YeuCauChuyenLop yc
                INNER JOIN HocSinh hs ON yc.MaHocSinh = hs.MaHocSinh
                INNER JOIN LopHoc lh1 ON yc.MaLopHienTai = lh1.MaLop
                LEFT JOIN LopHoc lh2 ON yc.MaLopMongMuon = lh2.MaLop
                LEFT JOIN LopHoc lh3 ON yc.MaLopDuocDuyet = lh3.MaLop
                INNER JOIN HocKy hk ON yc.MaHocKy = hk.MaHocKy
                INNER JOIN NamHoc nh ON hk.MaNamHoc = nh.MaNamHoc
                WHERE yc.MaYeuCau = @maYeuCau";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maYeuCau", maYeuCau);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapFromReader(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi lấy yêu cầu theo mã: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }

            return null;
        }

        /// <summary>
        /// Duyệt yêu cầu
        /// </summary>
        public bool DuyetYeuCau(int maYeuCau, int maLopDuocDuyet, string nguoiXuLy, string ghiChuAdmin)
        {
            string sql = @"UPDATE YeuCauChuyenLop 
                SET TrangThai = N'Đã duyệt', 
                    MaLopDuocDuyet = @maLopDuocDuyet,
                    NgayXuLy = @ngayXuLy,
                    NguoiXuLy = @nguoiXuLy,
                    GhiChuAdmin = @ghiChuAdmin
                WHERE MaYeuCau = @maYeuCau";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maYeuCau", maYeuCau);
                        cmd.Parameters.AddWithValue("@maLopDuocDuyet", maLopDuocDuyet);
                        cmd.Parameters.AddWithValue("@ngayXuLy", DateTime.Now);
                        cmd.Parameters.AddWithValue("@nguoiXuLy", nguoiXuLy);
                        cmd.Parameters.AddWithValue("@ghiChuAdmin", ghiChuAdmin ?? "");

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi duyệt yêu cầu: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Từ chối yêu cầu
        /// </summary>
        public bool TuChoiYeuCau(int maYeuCau, string nguoiXuLy, string ghiChuAdmin)
        {
            string sql = @"UPDATE YeuCauChuyenLop 
                SET TrangThai = N'Từ chối',
                    NgayXuLy = @ngayXuLy,
                    NguoiXuLy = @nguoiXuLy,
                    GhiChuAdmin = @ghiChuAdmin
                WHERE MaYeuCau = @maYeuCau";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maYeuCau", maYeuCau);
                        cmd.Parameters.AddWithValue("@ngayXuLy", DateTime.Now);
                        cmd.Parameters.AddWithValue("@nguoiXuLy", nguoiXuLy);
                        cmd.Parameters.AddWithValue("@ghiChuAdmin", ghiChuAdmin ?? "");

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi từ chối yêu cầu: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Xóa yêu cầu
        /// </summary>
        public bool XoaYeuCau(int maYeuCau)
        {
            string sql = "DELETE FROM YeuCauChuyenLop WHERE MaYeuCau = @maYeuCau";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maYeuCau", maYeuCau);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi xóa yêu cầu: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Map từ DataReader sang DTO
        /// </summary>
        private YeuCauChuyenLopDTO MapFromReader(MySqlDataReader reader)
        {
            return new YeuCauChuyenLopDTO
            {
                MaYeuCau = Convert.ToInt32(reader["MaYeuCau"]),
                MaHocSinh = Convert.ToInt32(reader["MaHocSinh"]),
                MaLopHienTai = Convert.ToInt32(reader["MaLopHienTai"]),
                MaLopMongMuon = reader["MaLopMongMuon"] != DBNull.Value ? Convert.ToInt32(reader["MaLopMongMuon"]) : (int?)null,
                MaHocKy = Convert.ToInt32(reader["MaHocKy"]),
                LyDoYeuCau = reader["LyDoYeuCau"].ToString(),
                NgayTao = Convert.ToDateTime(reader["NgayTao"]),
                NguoiTao = reader["NguoiTao"].ToString(),
                TrangThai = reader["TrangThai"].ToString(),
                NgayXuLy = reader["NgayXuLy"] != DBNull.Value ? Convert.ToDateTime(reader["NgayXuLy"]) : (DateTime?)null,
                NguoiXuLy = reader["NguoiXuLy"] != DBNull.Value ? reader["NguoiXuLy"].ToString() : null,
                GhiChuAdmin = reader["GhiChuAdmin"] != DBNull.Value ? reader["GhiChuAdmin"].ToString() : null,
                MaLopDuocDuyet = reader["MaLopDuocDuyet"] != DBNull.Value ? Convert.ToInt32(reader["MaLopDuocDuyet"]) : (int?)null,
                
                // Thông tin mở rộng
                TenHocSinh = reader["TenHocSinh"].ToString(),
                TenLopHienTai = reader["TenLopHienTai"].ToString(),
                TenLopMongMuon = reader["TenLopMongMuon"] != DBNull.Value ? reader["TenLopMongMuon"].ToString() : null,
                TenLopDuocDuyet = reader["TenLopDuocDuyet"] != DBNull.Value ? reader["TenLopDuocDuyet"].ToString() : null,
                TenHocKy = reader["TenHocKy"].ToString(),
                TenNamHoc = reader["TenNamHoc"].ToString()
            };
        }
    }
}

