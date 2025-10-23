using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class LopHocDAO
    {
        // ✅ THÊM METHOD MỚI: Lấy mã lớp tiếp theo từ danh sách hiện có
        public int LayMaLopTiepTheo()
        {
            string query = "SELECT IFNULL(MAX(MaLop), 0) + 1 FROM LopHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        //  Thêm lớp học - ✅ BỔ SUNG maLop thủ công
        public bool ThemLop(LopDTO lop)
        {
            // ✅ THÊM MaLop vào câu INSERT
            string query = "INSERT INTO LopHoc (MaLop, TenLop, MaKhoi, MaGiaoVienChuNhiem) VALUES (@MaLop, @TenLop, @MaKhoi, @GVCN)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", lop.maLop); // ✅ THÊM DÒNG NÀY
                    cmd.Parameters.AddWithValue("@TenLop", lop.tenLop);
                    cmd.Parameters.AddWithValue("@MaKhoi", lop.maKhoi);
                    cmd.Parameters.AddWithValue("@GVCN", lop.maGVCN);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        // Đọc danh sách lớp học
        public List<LopDTO> DocDSLop()
        {
            List<LopDTO> ds = new List<LopDTO>();
            string query = "SELECT MaLop, TenLop,MaKhoi, MaGiaoVienChuNhiem FROM LopHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LopDTO lop = new LopDTO();
                            lop.maLop = reader.GetInt32("MaLop");
                            lop.tenLop = reader.GetString("TenLop");
                            lop.maKhoi = reader.GetInt32("MaKhoi");

                            lop.maGVCN = reader.GetString("MaGiaoVienChuNhiem");
                            ds.Add(lop);
                        }
                    }
                }
            }
            return ds;
        }

        //  Lấy lớp theo ID
        public LopDTO LayLopTheoId(int maLop)
        {
            LopDTO lop = null;
            string query = "SELECT MaLop, TenLop,MaKhoi, MaGiaoVienChuNhiem FROM LopHoc WHERE MaLop = @MaLop";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lop = new LopDTO();
                            lop.maLop = reader.GetInt32("MaLop");
                            lop.tenLop = reader.GetString("TenLop");
                            lop.maKhoi = reader.GetInt32("MaKhoi");

                            lop.maGVCN = reader.GetString("MaGiaoVienChuNhiem");
                        }
                    }
                }
            }
            return lop;
        }

        //  Lấy lớp theo ID
        public LopDTO LayLopTheoTen(string tenLop)
        {
            LopDTO lop = null;
            string query = "SELECT MaLop, TenLop,MaKhoi, MaGiaoVienChuNhiem FROM LopHoc WHERE TenLop = @TenLop";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenLop", tenLop);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lop = new LopDTO();
                            lop.maLop = reader.GetInt32("MaLop");
                            lop.tenLop = reader.GetString("TenLop");
                            lop.maKhoi = reader.GetInt32("MaKhoi");

                            lop.maGVCN = reader.GetString("MaGiaoVienChuNhiem");
                        }
                    }
                }
            }
            return lop;
        }
        //  Cập nhật lớp học
        public bool CapNhatLop(LopDTO lop)
        {
            string query = "UPDATE LopHoc SET TenLop = @Ten_Lop, MaKhoi=@Ma_Khoi, MaGiaoVienChuNhiem = @GVCN WHERE MaLop = @Ma_Lop";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@Ten_Lop", lop.tenLop);
                    cmd.Parameters.AddWithValue("@Ma_khoi", lop.maKhoi);

                    cmd.Parameters.AddWithValue("@GVCN", lop.maGVCN);
                    cmd.Parameters.AddWithValue("@Ma_Lop", lop.maLop);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        //  Xóa lớp học
        public bool XoaLop(int maLop)
        {
            string query = "DELETE FROM LopHoc WHERE MaLop = @Ma_Lop";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma_Lop", maLop);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }


        // ✅ THÊM METHOD: Lấy danh sách mã GVCN đang được phân công
        public List<string> LayDanhSachMaGVCNDangPhanCong()
        {
            List<string> dsMaGVCN = new List<string>();
            string query = "SELECT DISTINCT MaGiaoVienChuNhiem FROM LopHoc WHERE MaGiaoVienChuNhiem IS NOT NULL AND MaGiaoVienChuNhiem != ''";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dsMaGVCN.Add(reader.GetString("MaGiaoVienChuNhiem"));
                        }
                    }
                }
            }
            return dsMaGVCN;
        }
    }
}