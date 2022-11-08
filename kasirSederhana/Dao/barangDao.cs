using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//tambahkan 2 library dibawah ini 
using FirebirdSql.Data.FirebirdClient;
using kasirSederhana.Model;
using kasirSederhana;

namespace kasirSederhana.Dao
{
    public class barangDao
    {
        private FbConnection conn;
        private string strsql = string.Empty;

        //constructor
        
        public barangDao(FbConnection conn)
        {
            this.conn = conn;
        }

        //Method Tambah Barang
        public int Tambah(barangModel brg)
        {
            strsql = "INSERT INTO BARANG values (@1,@2,@3,@4,@5,@6,@7,@8,@9)";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", brg.KodeBarang); 
                cmd.Parameters.AddWithValue("@2", brg.Kategori);
                cmd.Parameters.AddWithValue("@3", brg.Satuan);
                cmd.Parameters.AddWithValue("@4", brg.NamaBarang);
                cmd.Parameters.AddWithValue("@5", brg.HargaBeli);
                cmd.Parameters.AddWithValue("@6", brg.HargaJual);
                cmd.Parameters.AddWithValue("@7", brg.Stok);
                cmd.Parameters.AddWithValue("@8", brg.Keterangan);
                cmd.Parameters.AddWithValue("@9", brg.Status);

                return cmd.ExecuteNonQuery();

                cmd.Dispose();
            }
            
        }
        //Method Update Barang
        public int Update(barangModel brg)
        {
            strsql = @"UPDATE BARANG set 
                KETEGORI_ID = @2,
                SATUAN_ID = @3,
                NAMA = @4,
                HARGA_BELI = @5,
                HARGA_JUAL = @6,
                STOK = @7,
                KETERANGAN = @8,
                STATUS=@9 
                WHERE KODE = @1";
            using (FbCommand cmd = new FbCommand(strsql,conn))
            {
                cmd.Parameters.AddWithValue("@1", brg.KodeBarang);
                cmd.Parameters.AddWithValue("@2", brg.Kategori);
                cmd.Parameters.AddWithValue("@3", brg.Satuan);
                cmd.Parameters.AddWithValue("@4", brg.NamaBarang);
                cmd.Parameters.AddWithValue("@5", brg.HargaBeli);
                cmd.Parameters.AddWithValue("@6", brg.HargaJual);
                cmd.Parameters.AddWithValue("@7", brg.Stok);
                cmd.Parameters.AddWithValue("@8", brg.Keterangan);
                cmd.Parameters.AddWithValue("@9", brg.Status);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }
        //Method Hapus Barang
        public int Hapus(string kodeBarang)
        {
            strsql = "DELETE FROM BARANG WHERE KODE = @1";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", kodeBarang);

                return cmd.ExecuteNonQuery();
            }
        }
        
        private barangModel MappingRowToObject(FbDataReader dtr)
        {
            barangModel brg = new barangModel();

            brg.KodeBarang = dtr["KODE"] is DBNull ? string.Empty : dtr["KODE"].ToString();
            brg.Satuan = int.Parse(dtr["SATUAN_ID"] is DBNull ? string.Empty : dtr["SATUAN_ID"].ToString());
            brg.Kategori = int.Parse(dtr["KETEGORI_ID"] is DBNull ? string.Empty : dtr["KETEGORI_ID"].ToString());
            brg.NamaBarang = dtr["NAMA"] is DBNull ? string.Empty : dtr["NAMA"].ToString();
            brg.HargaBeli= int.Parse(dtr["HARGA_BELI"] is DBNull ? string.Empty : dtr["HARGA_BELI"].ToString());
            brg.HargaJual = int.Parse(dtr["HARGA_JUAL"] is DBNull ? string.Empty : dtr["HARGA_JUAL"].ToString());
            brg.Stok = int.Parse(dtr["STOK"] is DBNull ? string.Empty : dtr["STOK"].ToString());
            brg.Keterangan = dtr["KETERANGAN"] is DBNull ? string.Empty : dtr["KETERANGAN"].ToString();
            brg.Status = int.Parse(dtr["STATUS"] is DBNull ? string.Empty : dtr["STATUS"].ToString());

            return brg;
        }

        public List<barangModel> GetAll()
        {
            List<barangModel> daftarBrg = new List<barangModel>();
            strsql = "SELECT * FROM BARANG ORDER BY KODE";
            using (FbCommand cmd = new FbCommand(strsql,conn)) 
            {
                using (FbDataReader dtr = cmd.ExecuteReader())
                {
                 
                    while (dtr.Read())
                    {
                        daftarBrg.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return daftarBrg;
        }

        public List<barangModel> GetByName(string nama)
        {
            List<barangModel> daftarBrg = new List<barangModel>();
            strsql = @"SELECT * FROM BARANG WHERE NAMA LIKE @1  OR KODE LIKE @1 ORDER BY NAMA";

            using (FbCommand cmd = new FbCommand(strsql,conn))
            {
                cmd.Parameters.AddWithValue("@1","%" + nama + "%");
                using (FbDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        daftarBrg.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return daftarBrg;
        }

        public List<barangModel> Hitung()
        {
            long hitung;
            string urutan;
            List<barangModel> daftarBrg = new List<barangModel>();
            strsql = "SELECT * FROM BARANG Where KODE in (select max(KODE) from BARANG) ORDER BY KODE DESC";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                using (FbDataReader dtr = cmd.ExecuteReader())
                {

                    dtr.Read();
                    if (dtr.HasRows)
                    {
                        hitung = Convert.ToInt64(dtr[0].ToString().Substring(dtr["KODE"].ToString().Length - 3, 3)) + 1;
                        string joinstr = "000" + hitung;
                        urutan = "BRG" + joinstr.Substring(joinstr.Length - 3, 3);
                    }
                    else
                    {
                        urutan = "BRG0001";
                    }
                    dtr.Close();
                    //while (dtr.Read())
                    //{
                    //    daftarBrg.Add(MappingRowToObject(dtr));
                    //}
                }
            }
            return daftarBrg;
        }




        

        public List<barangModel> GetById(string nama)
        {
            List<barangModel> daftarBrg = new List<barangModel>();

            //strsql = @"select barang.nama,barang.harga_jual,satuan.nama from barang inner join satuan on barang.satuan_id = satuan.id  where barang.kode = @1";
            strsql = @"select barang.nama as nama, barang.harga_jual as harga_jual, satuan.nama as satuan from barang inner join satuan on barang.satuan_id = satuan.id  where barang.kode = @1";


            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", nama );
                using (FbDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        daftarBrg.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return daftarBrg;
        }

    }
}
