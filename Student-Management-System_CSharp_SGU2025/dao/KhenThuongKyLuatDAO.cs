using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class KhenThuongKyLuatDAO
    {
        /// <summary>
        /// Thêm một bản ghi khen thưởng/kỷ luật mới
        /// </summary>
        public bool ThemKhenThuongKyLuat(KhenThuongKyLuatDTO kt)
        {
            string sql = @"INSERT INTO KhenThuongKyLuat 
                          (MaHocSinh, Loai, NoiDung, CapKhenThuong, MucXuLy, NgayApDung, NguoiLapID, TrangThaiDuyet) 
                          VALUES (@maHS, @loai, @noiDung, @capKhen, @mucXuLy, @ngayApDung, @nguoiLap, @trangThai)";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", kt.MaHocSinh);
                        cmd.Parameters.AddWithValue("@loai", kt.Loai);
                        cmd.Parameters.AddWithValue("@noiDung", kt.NoiDung);
                        cmd.Parameters.AddWithValue("@capKhen", (object)kt.CapKhenThuong ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@mucXuLy", (object)kt.MucXuLy ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ngayApDung", kt.NgayApDung);
                        cmd.Parameters.AddWithValue("@nguoiLap", (object)kt.NguoiLapID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@trangThai", kt.TrangThaiDuyet);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi thêm khen thưởng/kỷ luật: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy danh sách khen thưởng theo loại
        /// </summary>
        public List<KhenThuongKyLuatDTO> LayDanhSachTheoLoai(string loai)
        {
            List<KhenThuongKyLuatDTO> ds = new List<KhenThuongKyLuatDTO>();
            string sql = @"SELECT kt.*, hs.HoTen 
                          FROM KhenThuongKyLuat kt 
                          JOIN HocSinh hs ON kt.MaHocSinh = hs.MaHocSinh 
                          WHERE kt.Loai = @loai 
                          ORDER BY kt.NgayApDung DESC";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@loai", loai);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                KhenThuongKyLuatDTO kt = new KhenThuongKyLuatDTO
                                {
                                    MaKTKL = reader.GetInt32("MaKTKL"),
                                    MaHocSinh = reader.GetInt32("MaHocSinh"),
                                    Loai = reader.GetString("Loai"),
                                    NoiDung = reader.GetString("NoiDung"),
                                    CapKhenThuong = reader.IsDBNull(reader.GetOrdinal("CapKhenThuong"))
                                        ? null : reader.GetString("CapKhenThuong"),
                                    MucXuLy = reader.IsDBNull(reader.GetOrdinal("MucXuLy"))
                                        ? null : reader.GetString("MucXuLy"),
                                    NgayApDung = reader.GetDateTime("NgayApDung"),
                                    NguoiLapID = reader.IsDBNull(reader.GetOrdinal("NguoiLapID"))
                                        ? null : reader.GetString("NguoiLapID"),
                                    TrangThaiDuyet = reader.GetString("TrangThaiDuyet")
                                };
                                ds.Add(kt);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy danh sách khen thưởng/kỷ luật: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return ds;
        }

        /// <summary>
        /// Xóa một bản ghi khen thưởng/kỷ luật
        /// </summary>
        public bool XoaKhenThuongKyLuat(int maKTKL)
        {
            string sql = "DELETE FROM KhenThuongKyLuat WHERE MaKTKL = @maKTKL";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maKTKL", maKTKL);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi xóa khen thưởng/kỷ luật: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Cập nhật trạng thái duyệt
        /// </summary>
        public bool CapNhatTrangThaiDuyet(int maKTKL, string trangThaiMoi)
        {
            string sql = "UPDATE KhenThuongKyLuat SET TrangThaiDuyet = @trangThai WHERE MaKTKL = @maKTKL";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@trangThai", trangThaiMoi);
                        cmd.Parameters.AddWithValue("@maKTKL", maKTKL);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi cập nhật trạng thái duyệt: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết một bản ghi
        /// </summary>
        public KhenThuongKyLuatDTO LayTheoMa(int maKTKL)
        {
            string sql = "SELECT * FROM KhenThuongKyLuat WHERE MaKTKL = @maKTKL";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maKTKL", maKTKL);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new KhenThuongKyLuatDTO
                                {
                                    MaKTKL = reader.GetInt32("MaKTKL"),
                                    MaHocSinh = reader.GetInt32("MaHocSinh"),
                                    Loai = reader.GetString("Loai"),
                                    NoiDung = reader.GetString("NoiDung"),
                                    CapKhenThuong = reader.IsDBNull(reader.GetOrdinal("CapKhenThuong"))
                                        ? null : reader.GetString("CapKhenThuong"),
                                    MucXuLy = reader.IsDBNull(reader.GetOrdinal("MucXuLy"))
                                        ? null : reader.GetString("MucXuLy"),
                                    NgayApDung = reader.GetDateTime("NgayApDung"),
                                    NguoiLapID = reader.IsDBNull(reader.GetOrdinal("NguoiLapID"))
                                        ? null : reader.GetString("NguoiLapID"),
                                    TrangThaiDuyet = reader.GetString("TrangThaiDuyet")
                                };
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy thông tin khen thưởng/kỷ luật: " + ex.Message);
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
        /// Cập nhật thông tin khen thưởng/kỷ luật
        /// </summary>
        public bool CapNhatKhenThuongKyLuat(int maKTKL, string noiDung, string capKhenThuong,
            string mucXuLy, DateTime ngayApDung)
        {
            string sql = @"UPDATE KhenThuongKyLuat 
                   SET NoiDung = @noiDung, 
                       CapKhenThuong = @capKhen, 
                       MucXuLy = @mucXuLy, 
                       NgayApDung = @ngayApDung 
                   WHERE MaKTKL = @maKTKL";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@noiDung", noiDung);
                        cmd.Parameters.AddWithValue("@capKhen", (object)capKhenThuong ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@mucXuLy", (object)mucXuLy ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ngayApDung", ngayApDung);
                        cmd.Parameters.AddWithValue("@maKTKL", maKTKL);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi cập nhật khen thưởng/kỷ luật: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Đếm số lượng khen thưởng theo học kỳ
        /// </summary>
        public int DemKhenThuongTheoHocKy(int maHocKy)
        {
            string sql = @"SELECT COUNT(DISTINCT kt.MaKTKL) 
                  FROM KhenThuongKyLuat kt
                  JOIN HocSinh hs ON kt.MaHocSinh = hs.MaHocSinh
                  JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh
                  WHERE kt.Loai = 'Khen thưởng' 
                  AND pl.MaHocKy = @maHocKy";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHocKy", maHocKy);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đếm khen thưởng theo học kỳ: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Đếm số lượng khen thưởng theo cấp khen và học kỳ
        /// </summary>
        public int DemKhenThuongTheoCapVaHocKy(string capKhen, int maHocKy)
        {
            string sql = @"SELECT COUNT(DISTINCT kt.MaKTKL) 
                  FROM KhenThuongKyLuat kt
                  JOIN HocSinh hs ON kt.MaHocSinh = hs.MaHocSinh
                  JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh
                  WHERE kt.Loai = 'Khen thưởng' 
                  AND kt.CapKhenThuong = @capKhen
                  AND pl.MaHocKy = @maHocKy";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@capKhen", capKhen);
                        cmd.Parameters.AddWithValue("@maHocKy", maHocKy);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đếm khen thưởng theo cấp: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Đếm số lượng học sinh bị kỷ luật theo học kỳ (đếm học sinh duy nhất)
        /// </summary>
        public int DemHocSinhKyLuatTheoHocKy(int maHocKy)
        {
            string sql = @"SELECT COUNT(DISTINCT kt.MaHocSinh) 
                  FROM KhenThuongKyLuat kt
                  JOIN HocSinh hs ON kt.MaHocSinh = hs.MaHocSinh
                  JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh
                  WHERE kt.Loai = 'Kỷ luật' 
                  AND pl.MaHocKy = @maHocKy";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHocKy", maHocKy);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đếm học sinh kỷ luật theo học kỳ: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Đếm tổng số học sinh trong học kỳ
        /// </summary>
        public int DemTongHocSinhTheoHocKy(int maHocKy)
        {
            string sql = @"SELECT COUNT(DISTINCT pl.MaHocSinh) 
                  FROM PhanLop pl
                  WHERE pl.MaHocKy = @maHocKy";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHocKy", maHocKy);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đếm tổng học sinh theo học kỳ: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

    }
}