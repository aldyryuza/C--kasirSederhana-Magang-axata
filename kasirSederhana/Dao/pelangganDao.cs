using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kasirSederhana.Model;
using FirebirdSql.Data.FirebirdClient;

namespace kasirSederhana.Dao
{
    public class pelangganDao
    {
        private FbConnection conn;
        private string strsql = string.Empty;

        //constructor

        public pelangganDao(FbConnection conn)
        {
            this.conn = conn;
        }


        //Query Tambah 
        public int Tambah(pelangganModel pelanggan)
        {
            strsql = @"INSERT INTO pelanggan  VALUES (gen_id(gen_pelanggan_id, 1), @1,@2,@3,@4,@5)";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", pelanggan.Nama);
                cmd.Parameters.AddWithValue("@2", pelanggan.Alamat);
                cmd.Parameters.AddWithValue("@3", pelanggan.JenisKelamin);
                cmd.Parameters.AddWithValue("@4", pelanggan.NoTelp);
                cmd.Parameters.AddWithValue("@5", pelanggan.Keterangan);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

        }

        //Query Update
        public int Update(pelangganModel pelanggan)
        {
            strsql = @"Update pelanggan set 
                        NAMA = @1,
                        ALAMAT = @2,
                        JENIS_KELAMIN = @3,
                        NO_TELP = @4,
                        KETERANGAN = @5
                        Where ID = @6";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", pelanggan.Nama);
                cmd.Parameters.AddWithValue("@2", pelanggan.Alamat);
                cmd.Parameters.AddWithValue("@3", pelanggan.JenisKelamin);
                cmd.Parameters.AddWithValue("@4", pelanggan.NoTelp);
                cmd.Parameters.AddWithValue("@5", pelanggan.Keterangan);
                cmd.Parameters.AddWithValue("@6", pelanggan.Id);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        //Query Hapus
        public int Hapus(int Id)
        {
            strsql = "DELETE FROM pelanggan WHERE ID = @6";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@6", Id);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        private pelangganModel MappingRowToObject(FbDataReader dtr)
        {
            pelangganModel pelanggan = new pelangganModel();
            pelanggan.Id = int.Parse(dtr["ID"] is DBNull ? string.Empty : dtr["ID"].ToString());
            pelanggan.Nama = dtr["NAMA"] is DBNull ? string.Empty : dtr["NAMA"].ToString();
            pelanggan.Alamat = dtr["ALAMAT"] is DBNull ? string.Empty : dtr["ALAMAT"].ToString();
            pelanggan.JenisKelamin = dtr["JENIS_KELAMIN"] is DBNull ? string.Empty : dtr["JENIS_KELAMIN"].ToString();
            pelanggan.NoTelp = dtr["NO_TELP"] is DBNull ? string.Empty : dtr["NO_TELP"].ToString();
            pelanggan.Keterangan = dtr["KETERANGAN"] is DBNull ? string.Empty : dtr["KETERANGAN"].ToString();

            return pelanggan;
        }

        public List<pelangganModel> GetAll()
        {
            List<pelangganModel> daftarPelanggan = new List<pelangganModel>();
            strsql = "SELECT * FROM pelanggan ORDER BY ID DESC";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                using (FbDataReader dtr = cmd.ExecuteReader())
                {

                    while (dtr.Read())
                    {
                        daftarPelanggan.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return daftarPelanggan;
        }

        public List<pelangganModel> GetByName(string nama)
        {
            List<pelangganModel> daftarPelanggan = new List<pelangganModel>();
            strsql = @"SELECT * FROM pelanggan WHERE NAMA LIKE @1 OR ALAMAT LIKE @1  ORDER BY NAMA";

            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", "%" + nama + "%");
                using (FbDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        daftarPelanggan.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return daftarPelanggan;
        }

    }
}

