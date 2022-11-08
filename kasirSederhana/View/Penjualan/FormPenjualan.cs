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
using System.Collections;

namespace kasirSederhana.View.Penjualan
{
    public partial class FormPenjualan : Form
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

           textBoxNoBukti.Text= randomInteger.Next().ToString();
        }

        private void panggilPelanggan()
        {

            //membuat object plgDao untuk mengakses operasi database
            plgDao = new pelangganDao(conn.GetConnection());


            List<pelangganModel> daftarPlg = plgDao.GetAll();
            foreach (pelangganModel plg in daftarPlg)
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
            plgDao = new pelangganDao(conn.GetConnection());

            List<pelangganModel> alamat = plgDao.GetByName(comboBox1.Text);
            foreach (pelangganModel plg in alamat)
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
        private pelangganDao plgDao = null;
        /*private  brgDao = null;*/
        private userDao usrDao = null;


        //untuk menampung return value dari operasi CRUD
        private int result = 0;

        
        public FormPenjualan()
        {
            InitializeComponent();
            conn = DBConection.GetInstance();
            clear();
            panggilPelanggan();
            loadUser();
            
            tanggalText.Text = DateTime.Now.ToString();
            textBoxKembalian.Enabled = false;
            
        }
        public FormPenjualan(string data)
        {
            InitializeComponent();
            conn = DBConection.GetInstance();
     
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAlamat();
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

/*AMBIL DATA BARANG*/
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex==0)
                {
                    string nama1 = dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString();
                    FormBarangSearch frmCariBarang = new FormBarangSearch(nama1,e.RowIndex);

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
                    int total,satuan,jumlah;
                    jumlah = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    satuan = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    total = satuan * jumlah;
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value =  total.ToString();
                }
                

            }
            catch (Exception )
            {

                throw;
            }
        }
/*POTONGAN*/
        private void textBoxPotongan_TextChanged(object sender, EventArgs e)
        {
            int potongan,total,hasil;
            potongan = int.Parse(textBoxPotongan.Text);
            total = int.Parse(textBoxSubTotal.Text);

            if (potongan != null)
            {
                hasil = total - potongan;
                textBoxTotalHarga.Text = hasil.ToString();
                label6.Text = hasil.ToString();
            }
        }
/*BIAYA KIRIM*/
        private void textBoxBiayaKirim_TextChanged(object sender, EventArgs e)
        {
            int biayaKirim, potongan, subTotal,hasil,hasil1;
            biayaKirim = int.Parse(textBoxBiayaKirim.Text);
            potongan = int.Parse(textBoxPotongan.Text);
            subTotal = int.Parse(textBoxSubTotal.Text);
            

            if (biayaKirim != null)
            {
                hasil = subTotal - potongan ;
                hasil1 = hasil + biayaKirim;
                textBoxTotalHarga.Text = hasil1.ToString();
                label6.Text= hasil1.ToString();

            }
        }

/*BAYAR*/
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
                    string strsql = "INSERT INTO penjualan  VALUES (gen_id(gen_penjualan_id, 1), " +
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
                    string id_plg = "select id from penjualan where id in (select max(id) from penjualan)";
                    string idPlg;
                    string detail;
                    using (FbCommand cmd = new FbCommand(id_plg, conn.GetConnection()))
                    {
                        using (FbDataReader dtr = cmd.ExecuteReader())
                        {

                            while (dtr.Read())
                            {
                                idPlg = dtr["id"].ToString();
                                detailPejualanModel dtPnj = new detailPejualanModel();


                                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                                {
                                    dtPnj.PenjualanId = int.Parse(idPlg.ToString());
                                    dtPnj.KodeBarang = dataGridView1.Rows[i].Cells[5].Value.ToString();
                                    dtPnj.HargaJual = int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                    dtPnj.Jumlah = int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                                    dtPnj.JumlahHarga = int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                                    dtPnj.HargaBeli = int.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                                    //result = pnjDao.TambahDetail(dtPnj);
                                    string insert = "INSERT INTO detail_penjualan  VALUES (gen_id(gen_detail_penjualan_id, 1)," +
                                                       dtPnj.PenjualanId + "," +
                                                       "'" + dtPnj.KodeBarang + "'," +
                                                       dtPnj.HargaJual + "," +
                                                       dtPnj.Jumlah + "," +
                                                       dtPnj.JumlahHarga + "," +
                                                       dtPnj.HargaBeli +")";

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
    }
}

