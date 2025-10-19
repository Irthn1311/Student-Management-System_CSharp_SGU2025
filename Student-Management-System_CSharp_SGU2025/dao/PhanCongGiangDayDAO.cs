using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class PhanCongGiangDayDAO
    {
        /* 
    MaPhanCong INT PRIMARY KEY AUTO_INCREMENT,
    MaLop INT,
    MaGiaoVien VARCHAR(15),
    MaMonHoc INT,
    MaHocKy INT,
    NgayBatDau DATE,
    NgayKetThuc DATE,
    UNIQUE (MaLop, MaGiaoVien, MaMonHoc, MaHocKy),
    FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop),
    FOREIGN KEY (MaGiaoVien) REFERENCES GiaoVien(MaGiaoVien),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
*/
        public bool ThemPhanCongGiangDay(PhanCongGiangDayDTO pcgd)
        {
            string query = @"INSERT INTO PhanCongGiangDay 
                            (MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc) 
                             VALUES (@MaLop, @MaGiaoVien, @MaMonHoc, @MaHocKy, @NgayBatDau, @NgayKetThuc)";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", pcgd.MaLop);
                    cmd.Parameters.AddWithValue("@MaGiaoVien", pcgd.MaGiaoVien);
                    cmd.Parameters.AddWithValue("@MaMonHoc", pcgd.MaMonHoc);
                    cmd.Parameters.AddWithValue("@MaHocKy", pcgd.MaHocKy);
                    cmd.Parameters.AddWithValue("@NgayBatDau", pcgd.TuNgay);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", pcgd.DenNgay);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        // Đọc danh sách phân công giảng dạy
        public List<PhanCongGiangDayDTO> DocDSPhanCong()
        {
            List<PhanCongGiangDayDTO> ds = new List<PhanCongGiangDayDTO>();
            string query = "SELECT * FROM PhanCongGiangDay";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PhanCongGiangDayDTO pc = new PhanCongGiangDayDTO
                            {
                                MaPhanCong = reader.GetInt32("MaPhanCong"),
                                MaLop = reader.GetInt32("MaLop"),
                                MaGiaoVien = reader.GetString("MaGiaoVien"),
                                MaMonHoc = reader.GetInt32("MaMonHoc"),
                                MaHocKy = reader.GetInt32("MaHocKy"),
                                TuNgay = reader.GetDateTime("NgayBatDau"),
                                DenNgay = reader.GetDateTime("NgayKetThuc")
                            };
                            ds.Add(pc);
                        }
                    }
                }
            }
            return ds;

        }
        public PhanCongGiangDayDTO LayPhanCongTheoId(int maPhanCong)
        {
            PhanCongGiangDayDTO pc = null;
            string query = "SELECT * FROM PhanCongGiangDay WHERE MaPhanCong = @MaPhanCong";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhanCong", maPhanCong);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pc = new PhanCongGiangDayDTO
                            {
                                MaPhanCong = reader.GetInt32("MaPhanCong"),
                                MaLop = reader.GetInt32("MaLop"),
                                MaGiaoVien = reader.GetString("MaGiaoVien"),
                                MaMonHoc = reader.GetInt32("MaMonHoc"),
                                MaHocKy = reader.GetInt32("MaHocKy"),
                                TuNgay = reader.GetDateTime("NgayBatDau"),
                                DenNgay = reader.GetDateTime("NgayKetThuc")
                            };
                        }
                    }
                }
            }
            return pc;
        }
        public bool XoaPhanCong(int maPhanCong)
        {
            string query = "DELETE FROM PhanCongGiangDay WHERE MaPhanCong = @MaPhanCong";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhanCong", maPhanCong);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

    }
}