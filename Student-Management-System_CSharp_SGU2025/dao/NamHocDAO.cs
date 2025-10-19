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
        MaNamHoc VARCHAR(10) PRIMARY KEY,
        TenNamHoc VARCHAR(50) NOT NULL,
        NgayBatDau DATE,
        NgayKetThuc DATE
);
);*/
        public bool themNamHoc(NamHocDTO namHoc)
        {
            string query = "insert into NamHoc(TenNamHoc, NgayBatDau, NgayKetThuc) values(@TenNamHoc,@NgayBatDau,@NgayKetThuc)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenNamHoc", namHoc.TenNamHoc);
                    cmd.Parameters.AddWithValue("@NgayBatDau", namHoc.NgayBD);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", namHoc.NgayKT);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;

                }

            }
        }
        public List<NamHocDTO> DocDSNamHoc()
        {
            List<NamHocDTO> ds = new List<NamHocDTO>();
            string query = "select MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc from NamHoc";
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
                            nh.MaNamHoc = reader.GetString("MaNamHoc");
                            nh.TenNamHoc = reader.GetString("TenNamHoc");
                            nh.NgayBD = reader.GetDateTime("NgayBatDau");
                            nh.NgayKT = reader.GetDateTime("NgayKetThuc");
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
            string query = "select MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc from NamHoc where MaNamHoc=@MaNamHoc";
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
                            namHoc.MaNamHoc = reader.GetString("MaNamHoc");
                            namHoc.TenNamHoc = reader.GetString("TenNamHoc");
                            namHoc.NgayBD = reader.GetDateTime("NgayBatDau");
                            namHoc.NgayKT = reader.GetDateTime("NgayKetThuc");
                        }
                    }
                }
            }
            return namHoc;
        }
        public NamHocDTO LayNamHocTheoTen(string tenNamHoc)
        {
            NamHocDTO namHoc = null;
            string query = "select MaNamHoc, NgayBatDau, NgayKetThuc from NamHoc where TenNamHoc=@TenNamHoc";
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
                            namHoc.MaNamHoc = reader.GetString("MaNamHoc");
                            namHoc.TenNamHoc = tenNamHoc;
                            namHoc.NgayBD = reader.GetDateTime("NgayBatDau");
                            namHoc.NgayKT = reader.GetDateTime("NgayKetThuc");
                        }
                    }
                }
            }return namHoc;
        }
        public bool updateNamHoc(NamHocDTO namHoc)
        {
            string query = "update NamHoc set TenNamHoc=@TenNamHoc,NgayBatDau=@NgayBatDau,NgayKetThuc=@NgayKetThuc where MaNamHoc=@MaNamHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenNamHoc", namHoc.TenNamHoc);
                    cmd.Parameters.AddWithValue("@NgayBatDau", namHoc.NgayBD);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", namHoc.NgayKT);
                    cmd.Parameters.AddWithValue("@MaNamHoc", namHoc.MaNamHoc);
                    int result=cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
        public bool XoaNamHoc(int maNamHoc)
        {
            string query = "DELETE FROM NamHoc WHERE MaNamHoc = @MaNamHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}
