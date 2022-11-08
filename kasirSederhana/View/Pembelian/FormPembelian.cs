using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using kasirSederhana.Dao;
using kasirSederhana.Model;
using kasirSederhana.View.Barang;
using FirebirdSql.Data.FirebirdClient;

namespace kasirSederhana.View.Pembelian
{
    public partial class FormPembelian : Form
    {
        /*FUNGSI*/

        void clear()
        {
            textBoxAlamat.Text = "";
            textBoxBiayaKirim.Text = "";
            textBoxketerangan.Text = "";
            textBoxNoBukti.Text = "";
            //comboBox1.Text = " pilih -- ";
            textBoxPotongan.Text = "";
            textBoxSubTotal.Text = "";
            textBoxTotalHarga.Text = "";
            dataGridView1.Rows.Clear();
            label6.Text = "0";
            textBoxSubTotal.Enabled = false;
            textBoxTotalHarga.Enabled = false;
            textBoxAlamat.Enabled = false;
            textBoxJumlahUang.Text = "";
            textBoxKembalian.Text = "";
            hitungNo();
        }
        void hitungJumlah()
        {
            int sum = 0;
            int harga = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                harga += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }
            lebelJumlah.Text = sum.ToString();
            label6.Text = harga.ToString();
            textBoxSubTotal.Text = harga.ToString();
            textBoxTotalHarga.Text = harga.ToString();




        }
        private void hitungNo()
        {
            var randomInteger = new Random();

            textBoxNoBukti.Text = randomInteger.Next().ToString();
        }

        private void panggilSuplier()
        {

            //membuat object plgDao untuk mengakses operasi database
            spl = new suplierDao(conn.GetConnection());


            List<suplierModel> daftarPlg = spl.GetAllSuplier();
            foreach (suplierModel plg in daftarPlg)
            {
                //FillToListView(brg); // panggil method FillToListView
                comboBox1.Items.Add(plg.Nama);
                /*textBoxAlamat.Text = plg.Id.ToString();*/
                textBoxAlamat.Text = plg.Alamat.ToString();
            }
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Nama";
            comboBox1.DataSource = daftarPlg;





        }
        private void loadAlamat()
        {
            //membuat object plgDao untuk mengakses operasi database
            spl = new suplierDao(conn.GetConnection());

            List<suplierModel> alamat = spl.GetByNameSuplier(comboBox1.Text);
            foreach (suplierModel plg in alamat)
            {

                textBoxAlamat.Text = plg.Alamat;
                textBoxketerangan.Text = plg.Keterangan;
            }
        }

        private void loadUser()
        {
            //membuat object plgDao untuk mengakses operasi database
            usrDao = new userDao(conn.GetConnection());

            List<userModel> user = usrDao.GetAll();
            foreach (userModel plg in user)
            {

                comboBoxUser.Items.Add(plg.Nama);
            }
            comboBoxUser.ValueMember = "Id";
            comboBoxUser.DisplayMember = "Nama";
            comboBoxUser.DataSource = user;
        }

        private void hitungKembalian()
        {
            int kembalian, jumlahBayar;
            jumlahBayar = int.Parse(textBoxJumlahUang.Text);
            kembalian = jumlahBayar - int.Parse(textBoxTotalHarga.Text);
            textBoxKembalian.Text = kembalian.ToString();
        }
        /*END FUNGSI*/

        /*VARIABLE*/
        private DBConection conn = null;
        private penjualanDao pnjDao = null;
        private suplierDao spl = null;
        /*private  brgDao = null;*/
        private userDao usrDao = null;


        //untuk menampung return value dari operasi CRUD
        private int result = 0;
        public FormPembelian()
        {
            InitializeComponent();
            conn = DBConection.GetInstance();
            clear();
            panggilSuplier();
            loadUser();

            tanggalText.Text = DateTime.Now.ToString();
            textBoxKembalian.Enabled = false;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    string nama1 = dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString();
                    FormBarangSearchPembelian frmCariBarang = new FormBarangSearchPembelian(nama1, e.RowIndex);

                    if (frmCariBarang.ShowDialog() == DialogResult.OK)
                    {
                    }

                    int total, satuan, jumlah;
                    jumlah = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    satuan = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    total = satuan * jumlah;
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = total.ToString();

                }

                if (e.ColumnIndex == 1)
                {
                    int total, satuan, jumlah;
                    jumlah = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    satuan = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    total = satuan * jumlah;
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = total.ToString();
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clear();
            loadAlamat();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            hitungJumlah();
        }

        private void textBoxPotongan_TextChanged(object sender, EventArgs e)
        {
            int potongan, total, hasil;
            potongan = int.Parse(textBoxPotongan.Text);
            total = int.Parse(textBoxSubTotal.Text);

            if (potongan != null)
            {
                hasil = total - potongan;
                textBoxTotalHarga.Text = hasil.ToString();
                label6.Text = hasil.ToString();
            }
        }

        private void textBoxBiayaKirim_TextChanged(object sender, EventArgs e)
        {
            int biayaKirim, potongan, subTotal, hasil, hasil1;
            biayaKirim = int.Parse(textBoxBiayaKirim.Text);
            potongan = int.Parse(textBoxPotongan.Text);
            subTotal = int.Parse(textBoxSubTotal.Text);


            if (biayaKirim != null)
            {
                hasil = subTotal - potongan;
                hasil1 = hasil + biayaKirim;
                textBoxTotalHarga.Text = hasil1.ToString();
                label6.Text = hasil1.ToString();

            }
        }

        private void btnBayar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("Anda Harus Mengisi Data Barang ", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                try
                {
                    string strsql = "INSERT INTO pembelian  VALUES (gen_id(gen_pembelian_id, 1), " +
                               int.Parse(comboBoxUser.SelectedValue.ToString()) + "," +
                               int.Parse(comboBox1.SelectedValue.ToString()) + "," +
                               int.Parse(textBoxBiayaKirim.Text) + "," +
                               int.Parse(textBoxPotongan.Text) + "," +
                               int.Parse(textBoxNoBukti.Text) + "," +
                               int.Parse(textBoxSubTotal.Text) + "," +
                               int.Parse(textBoxKembalian.Text) + "," +
                               "'" + tanggalText.Text + "'," +
                               int.Parse(textBoxJumlahUang.Text) + "," +
                               "'test')";
                    using (FbCommand cmd = new FbCommand(strsql, conn.GetConnection()))
                    {
                        using (FbDataReader dtr = cmd.ExecuteReader())
                        {

                        }

                    }
                    string id_plg = "select id from pembelian where id in (select max(id) from pembelian)";
                    string idPmb;
                    string detail;
                    using (FbCommand cmd = new FbCommand(id_plg, conn.GetConnection()))
                    {
                        using (FbDataReader dtr = cmd.ExecuteReader())
                        {

                            while (dtr.Read())
                            {
                                idPmb = dtr["id"].ToString();
                                detailPembelianModel dtPmb = new detailPembelianModel();
                                dtPmb.TotalHarga = int.Parse(textBoxTotalHarga.Text);

                                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                                {
                                    dtPmb.PembelianId = int.Parse(idPmb.ToString());
                                    dtPmb.KodeBarang = dataGridView1.Rows[i].Cells[5].Value.ToString();
                                    dtPmb.Jumlah = int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                                    dtPmb.HargaBeli = int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                    dtPmb.JumlahHarga = int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                                    

                                    //result = pnjDao.TambahDetail(dtPnj);
                                    string insert = "INSERT INTO detail_pembelian  VALUES (gen_id(gen_detail_pembelian_id, 1)," +
                                                       dtPmb.PembelianId + "," +
                                                       "'" + dtPmb.KodeBarang + "'," +
                                                       dtPmb.Jumlah + "," +
                                                       dtPmb.HargaBeli + "," +
                                                       dtPmb.JumlahHarga + "," +
                                                       dtPmb.TotalHarga + ")";

                                    using (FbCommand cmd1 = new FbCommand(insert, conn.GetConnection()))
                                    {
                                        using (FbDataReader dtr1 = cmd1.ExecuteReader())
                                        {
                                            
                                        }
                                    }

                                }

                            }

                        }



                    }
                    MessageBox.Show("Pembelian Berhasil ", " Success !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    loadAlamat();
                }
                catch (Exception)
                {

                    throw;
                }
                /*pejualanModel pnj = new pejualanModel();
                pnj.UserId = int.Parse(comboBoxUser.SelectedValue.ToString());
                pnj.PelangganId = int.Parse(comboBox1.SelectedValue.ToString());
                pnj.BiayaKirim = int.Parse(textBoxBiayaKirim.Text);
                pnj.Potongan = int.Parse(textBoxPotongan.Text);
                pnj.NoBukti = int.Parse(textBoxNoBukti.Text);
                pnj.SubTotal = int.Parse(textBoxSubTotal.Text);
                pnj.Kembalian = int.Parse(textBoxKembalian.Text);
                pnj.Tanggal = tanggalText.Text;
                pnj.Bayar = int.Parse(textBoxJumlahUang.Text);
                pnj.Keterangan = "kosong";*/

                /*                result = pnjDao.TambahPenjualan(pnj);*/

                /*  if (result > 1)
                  {
                      MessageBox.Show("OK");


                  }*/

                /* detailPejualanModel dtPnj = new detailPejualanModel();


                 for (int i = 0; i < dataGridView1.Rows.Count; i++)
                 {
                     dtPnj.KodeBarang = dataGridView1.Rows[i].Cells[5].Value.ToString();
                     dtPnj.HargaJual = int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                     dtPnj.Jumlah = int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                     dtPnj.JumlahHarga = int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                     dtPnj.HargaBeli = int.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                     result = pnjDao.TambahDetail(dtPnj);

                 }*/
            }
        }

        private void textBoxJumlahUang_TextChanged(object sender, EventArgs e)
        {
            hitungKembalian();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAlamat();
        }
    }
}
