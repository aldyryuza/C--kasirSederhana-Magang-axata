using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kasirSederhana.Model;
using FirebirdSql.Data.FirebirdClient;

namespace kasirSederhana.Dao
{
    public class penjualanDao
    {

        private FbConnection conn;
        private string strsql = string.Empty;

        public penjualanDao(FbConnection conn)
        {
            this.conn = conn;
        }
      

        public int TambahPenjualan(pejualanModel pnj)
        {
            strsql = @"INSERT INTO penjualan  VALUES (gen_id(gen_penjualan_id, 1), @1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", pnj.UserId);
                cmd.Parameters.AddWithValue("@2", pnj.PelangganId);
                cmd.Parameters.AddWithValue("@3", pnj.BiayaKirim);
                cmd.Parameters.AddWithValue("@4", pnj.Potongan);
                cmd.Parameters.AddWithValue("@5", pnj.NoBukti);
                cmd.Parameters.AddWithValue("@6", pnj.SubTotal);
                cmd.Parameters.AddWithValue("@7", pnj.Kembalian);
                cmd.Parameters.AddWithValue("@8", pnj.Tanggal);
                cmd.Parameters.AddWithValue("@9", pnj.Bayar);
                cmd.Parameters.AddWithValue("@10", pnj.Keterangan);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

        }
        public int TambahDetail(detailPejualanModel pnj)
        {
            strsql = @"INSERT INTO detail_penjualan  VALUES (gen_id(gen_detail_penjualan_id, 1), @1,@2,@3,@4,@5,@6)";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", pnj.PenjualanId);
                cmd.Parameters.AddWithValue("@2", pnj.KodeBarang);
                cmd.Parameters.AddWithValue("@3", pnj.Jumlah);
                cmd.Parameters.AddWithValue("@4", pnj.HargaJual);
                cmd.Parameters.AddWithValue("@5", pnj.HargaBeli);
                cmd.Parameters.AddWithValue("@6", pnj.JumlahHarga);


                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }   




        private pejualanModel MappingRowToObject(FbDataReader dtr)
        {
            pejualanModel pnj = new pejualanModel();
            pnj.Id = int.Parse(dtr["ID"] is DBNull ? string.Empty : dtr["ID"].ToString());
            pnj.UserId = int.Parse(dtr["USER_ID"] is DBNull ? string.Empty : dtr["USER_ID"].ToString());
            pnj.PelangganId = int.Parse(dtr["PELANGGAN_ID"] is DBNull ? string.Empty : dtr["PELANGGAN_ID"].ToString());
            pnj.BiayaKirim = int.Parse(dtr["BIAYA_KIRIM"] is DBNull ? string.Empty : dtr["BIAYA_KIRIM"].ToString());
            pnj.Potongan = int.Parse(dtr["POTONGAN"] is DBNull ? string.Empty : dtr["POTONGAN"].ToString());
            pnj.NoBukti = int.Parse(dtr["NO_BUKTI"] is DBNull ? string.Empty : dtr["NO_BUKTI"].ToString());
            pnj.SubTotal = int.Parse(dtr["SUB_TOTAL"] is DBNull ? string.Empty : dtr["SUB_TOTAL"].ToString());
            pnj.Kembalian = int.Parse(dtr["KEMBALIAN"] is DBNull ? string.Empty : dtr["KEMBALIAN"].ToString());
            pnj.Tanggal = dtr["TANGGAL"] is DBNull ? string.Empty : dtr["TANGGAL"].ToString();
            pnj.Bayar = int.Parse(dtr["BAYAR"] is DBNull ? string.Empty : dtr["BAYAR"].ToString());
            pnj.Keterangan = dtr["KETERANGAN"] is DBNull ? string.Empty : dtr["KETERANGAN"].ToString();

            return pnj;
        }
        public List<pejualanModel> GetAll()
        {
            List<pejualanModel> daftarSuplier = new List<pejualanModel>();
            strsql = "SELECT * FROM suplier2 ORDER BY ID";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                using (FbDataReader dtr = cmd.ExecuteReader())
                {

                    while (dtr.Read())
                    {
                        daftarSuplier.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return daftarSuplier;
        }

        public List<pejualanModel> GetByNameSuplier(string nama)
        {
            List<pejualanModel> daftarSuplier = new List<pejualanModel>();
            strsql = @"SELECT * FROM suplier2 WHERE NAMA LIKE @1 OR ALAMAT LIKE @1  ORDER BY NAMA";

            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", "%" + nama + "%");
                using (FbDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        daftarSuplier.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return daftarSuplier;
        }
    }
}


