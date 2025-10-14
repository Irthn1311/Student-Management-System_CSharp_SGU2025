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
    internal class NamHocDAO
    {
        /*CREATE TABLE NamHoc (
    Ma_Nam_Hoc INT PRIMARY KEY AUTO_INCREMENT,
    Ten_Nam_Hoc VARCHAR(50) NOT NULL, -- Ví dụ: '2024-2025'
    Ngay_Bat_Dau DATE,
    Ngay_Ket_Thuc DATE
);*/
        public bool themNamHoc(NamHocDTO namHoc)
        {
            string query = "insert into NamHoc(Ten_Nam_Hoc, Ngay_Bat_Dau, Ngay_Ket_Thuc) values(@Ten_Nam_Hoc,@Ngay_Bat_Dau,@Ngay_Ket_Thuc)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ten_Nam_Hoc", namHoc.TenNamHoc);
                    cmd.Parameters.AddWithValue("@Ngay_Bat_Dau", namHoc.NgayBD);
                    cmd.Parameters.AddWithValue("@Ngay_Ket_Thuc", namHoc.NgayKT);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;

                }

            }
        }
        public List<NamHocDTO> DocDSNamHoc()
        {
            List<NamHocDTO> ds = new List<NamHocDTO>();
            string query = "select Ma_Nam_Hoc, Ten_Nam_Hoc, Ngay_Bat_Dau, Ngay_Ket_Thuc from NamHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NamHocDTO nh = new NamHocDTO();
                            nh.MaNamHoc = reader.GetInt32("Ma_Nam_Hoc");
                            nh.TenNamHoc = reader.GetString("Ten_Nam_Hoc");
                            nh.NgayBD = reader.GetDateTime("Ngay_Bat_Dau");
                            nh.NgayKT = reader.GetDateTime("Ngay_Ket_Thuc");
                            ds.Add(nh);
                        }
                    }
                }
            }
            return ds;
        }
        public NamHocDTO LayNamHocTheoId(int maNamHoc)
        {
            NamHocDTO namHoc = null;
            string query = "select Ma_Nam_Hoc, Ten_Nam_Hoc, Ngay_Bat_Dau, Ngay_Ket_Thuc from NamHoc where Ma_Nam_Hoc=@MaNamHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            namHoc = new NamHocDTO();
                            namHoc.MaNamHoc = reader.GetInt32("Ma_Nam_Hoc");
                            namHoc.TenNamHoc = reader.GetString("Ten_Nam_Hoc");
                            namHoc.NgayBD = reader.GetDateTime("Ngay_Bat_Dau");
                            namHoc.NgayKT = reader.GetDateTime("Ngay_Ket_Thuc");
                        }
                    }
                }
            }
            return namHoc;
        }
        public NamHocDTO LayNamHocTheoTen(string tenNamHoc)
        {
            NamHocDTO namHoc = null;
            string query = "select Ma_Nam_Hoc, Ngay_Bat_Dau, Ngay_Ket_Thuc from NamHoc where Ten_Nam_Hoc=@TenNamHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenNamHoc", tenNamHoc);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            namHoc = new NamHocDTO();
                            namHoc.MaNamHoc = reader.GetInt32("Ma_Nam_Hoc");
                            namHoc.TenNamHoc = tenNamHoc;
                            namHoc.NgayBD = reader.GetDateTime("Ngay_Bat_Dau");
                            namHoc.NgayKT = reader.GetDateTime("Ngay_Ket_Thuc");
                        }
                    }
                }
            }return namHoc;
        }
        public bool updateNamHoc(NamHocDTO namHoc)
        {
            string query = "update NamHoc set Ten_Nam_Hoc=@Ten_Nam_Hoc,Ngay_Bat_Dau=@Ngay_Bat_Dau,Ngay_Ket_Thuc=@Ngay_Ket_Thuc where Ma_Nam_Hoc=@Ma_Nam_Hoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ten_Nam_Hoc", namHoc.TenNamHoc);
                    cmd.Parameters.AddWithValue("@Ngay_Bat_Dau", namHoc.NgayBD);
                    cmd.Parameters.AddWithValue("@Ngay_Ket_Thuc", namHoc.NgayKT);
                    cmd.Parameters.AddWithValue("@Ma_Nam_Hoc", namHoc.MaNamHoc);
                    int result=cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
        public bool XoaNamHoc(int maNamHoc)
        {
            string query = "DELETE FROM NamHoc WHERE Ma_Nam_Hoc = @Ma_Nam_Hoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma_Nam_Hoc", maNamHoc);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}
