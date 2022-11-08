using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using kasirSederhana.Dao;
using kasirSederhana.Model;
using kasirSederhana;
using kasirSederhana.View.Satuan;
using kasirSederhana.View.Kategori;

namespace kasirSederhana.View.Barang
{
    public partial class FormBarang : Form
    {

        private DBConection conn = null ;
        private barangDao brgDao = null;
        private satuanDao satDao = null;
        private kategoriDao katDao = null;
        


        //untuk menampung return value dari operasi CRUD
        private int result = 0;


        //constructor
        public FormBarang()
        {
            InitializeComponent();
            //membuat object conn untuk menghandle koneksi ke database
            conn = DBConection.GetInstance();

            //membuat object brgDao untuk mengakses operasi database
            brgDao = new barangDao(conn.GetConnection());
            ambilSatuan();
            ambilKategori();
            status();
            LoadDataBarang();
            hitungNo();

           
        }

        //Method Clear
        void clear()
        {
            kdBarangText.Enabled = false;
            satuanComboBox.Text = "pilih satuan ---";
            ktgrComboBox.Text = "pilih satuan ---";
            namaTextBox.Text = "";
            hbTextBox.Text = "0";
            hjTextBox.Text = "0";
            stokTextBox.Text = "0";
            keteranganTextBox.Text = "";
            statsucomboBox.Text = "pilih status ----";
            searchTextBox.Text = "";
            
            hitungNo();
        }


        // Ambil Value Dari Satuan
        private void ambilSatuan()
        {
            //membuat object satDao untuk mengakses operasi database
            satDao = new satuanDao(conn.GetConnection());
            listView1.Items.Clear();

            List<satuanModel> daftarSat = satDao.GetAll();
            foreach (satuanModel sat in daftarSat)
            {
                //FillToListView(brg); // panggil method FillToListView
                satuanComboBox.Items.Add(sat.Nama);
            }
            satuanComboBox.ValueMember = "Id";
            satuanComboBox.DisplayMember = "Nama";
            satuanComboBox.DataSource = daftarSat;
        }

        // Ambil Value Dari Kategori
        private void ambilKategori()
        {
            //membuat object brgDao untuk mengakses operasi database
            katDao = new kategoriDao(conn.GetConnection());
            listView1.Items.Clear();

            List<kategoriModel> daftarKat = katDao.GetAll();
            foreach (kategoriModel sat in daftarKat)
            {
                //FillToListView(brg); // panggil method FillToListView
                ktgrComboBox.Items.Add(sat.Nama);
            }
            ktgrComboBox.ValueMember = "Id";
            ktgrComboBox.DisplayMember = "Nama";
            ktgrComboBox.DataSource = daftarKat;
        }

        //nilai status
        void status()
        {
            statsucomboBox.Items.Clear();
            statsucomboBox.Items.Add(1);
            statsucomboBox.Items.Add(0);
        }

        //VIEW DATA 
        private void FillToListView(barangModel brg)
        {

            int noUrut = listView1.Items.Count + 1;

            ListViewItem item = new ListViewItem(noUrut.ToString());

            item.SubItems.Add(brg.KodeBarang);
            item.SubItems.Add(brg.Kategori.ToString());
            item.SubItems.Add(brg.Satuan.ToString());
            item.SubItems.Add(brg.NamaBarang);
            item.SubItems.Add(brg.HargaBeli.ToString());
            item.SubItems.Add(brg.HargaJual.ToString());
            item.SubItems.Add(brg.Stok.ToString());
            item.SubItems.Add(brg.Keterangan);
            item.SubItems.Add(brg.Status.ToString());



            listView1.Items.Add(item);

        }
        private void LoadDataBarang()
        {
            listView1.Items.Clear();

            List<barangModel> daftarBrg = brgDao.GetAll();
            foreach (barangModel brg in daftarBrg)
            {
                FillToListView(brg); // panggil method FillToListView
                /*ktgrComboBox.Items.Add(brg.NamaBarang);*/
            }
            /*ktgrComboBox.ValueMember = "KodeBarang";
            ktgrComboBox.DisplayMember = "NamaBarang";
            ktgrComboBox.DataSource = daftarBrg;*/

        }

        private void hitungNo()
        {
            int noUrut = listView1.Items.Count +1;
            kdBarangText.Text = "BRG00" + noUrut + "";
        }
        

        //search By Name OR KODE Barang
        private void LoadDataBarang(string nama)
        {
            listView1.Items.Clear();

            List<barangModel> daftarBrg = brgDao.GetByName(nama);
            foreach (barangModel brg in daftarBrg)
            {
                FillToListView(brg); // panggil method FillToListView
            }
        }

        private void FormBarang_Load(object sender, EventArgs e)
        {
            clear();
            


        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clear();
            btnTambah.Enabled = true;
            
            LoadDataBarang();
        }

        //Simpan Data
        private void btnTambah_Click(object sender, EventArgs e)
        {
     
            if (kdBarangText.Text.Trim() == "" || ktgrComboBox.Text.Trim() == "" || satuanComboBox.Text.Trim() == "" || namaTextBox.Text.Trim() == "" || hbTextBox.Text.Trim() == "" || hjTextBox.Text.Trim() == "" || stokTextBox.Text.Trim() == "" || keteranganTextBox.Text.Trim() == "" )
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //buat object brg
                barangModel brg = new barangModel();
                //isi nilai masing properti
                brg.KodeBarang = kdBarangText.Text;
                brg.Kategori = int.Parse(ktgrComboBox.SelectedValue.ToString());
                brg.Satuan = int.Parse(satuanComboBox.SelectedValue.ToString());
                brg.NamaBarang = namaTextBox.Text;
                brg.HargaBeli = int.Parse(hbTextBox.Text);
                brg.HargaJual = int.Parse(hjTextBox.Text);
                brg.Stok = int.Parse(stokTextBox.Text);
                brg.Keterangan = keteranganTextBox.Text;
                brg.Status = int.Parse(statsucomboBox.Text);

                result = brgDao.Tambah(brg);

                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Disimpan!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataBarang();
                    clear();

                    satuanComboBox.Focus();
                }
                else
                {
                    MessageBox.Show("Data Gagal Disimpan!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadDataBarang();
                    clear();
                    satuanComboBox.Focus();
                }
            }
            

        }

        //ambil data dari listView
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string nKode = this.listView1.SelectedItems[0].SubItems[1].Text;
                string nKategori = this.listView1.SelectedItems[0].SubItems[2].Text;
                string nSatuan = this.listView1.SelectedItems[0].SubItems[3].Text;
                string nNama = this.listView1.SelectedItems[0].SubItems[4].Text;
                string nHb = this.listView1.SelectedItems[0].SubItems[5].Text;
                string nHj = this.listView1.SelectedItems[0].SubItems[6].Text;
                string nStok = this.listView1.SelectedItems[0].SubItems[7].Text;
                string nKeterangan = this.listView1.SelectedItems[0].SubItems[8].Text;
                string nStatus = this.listView1.SelectedItems[0].SubItems[9].Text;

                kdBarangText.Text = nKode;
                satuanComboBox.Text = nSatuan;
                ktgrComboBox.Text = nKategori;
                namaTextBox.Text = nNama;
                hbTextBox.Text = nHb;
                hjTextBox.Text = nHj;
                stokTextBox.Text = nStok;
                keteranganTextBox.Text = nKeterangan;
                statsucomboBox.Text = nStatus;

                btnTambah.Enabled = false;

            }
            catch(Exception G)
            {
                MessageBox.Show(G.ToString());
            }
        }
        //Update Data
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (kdBarangText.Text.Trim() == "" || ktgrComboBox.Text.Trim() == "" || satuanComboBox.Text.Trim() == "" || namaTextBox.Text.Trim() == "" || hbTextBox.Text.Trim() == "" || hjTextBox.Text.Trim() == "" || stokTextBox.Text.Trim() == "" || keteranganTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //buat object brg
                barangModel brg = new barangModel();
                //isi nilai masing properti
                brg.KodeBarang = kdBarangText.Text;
                brg.Kategori = int.Parse(ktgrComboBox.Text);
                brg.Satuan = int.Parse(satuanComboBox.Text);
                brg.NamaBarang = namaTextBox.Text;
                brg.HargaBeli = int.Parse(hbTextBox.Text);
                brg.HargaJual = int.Parse(hjTextBox.Text);
                brg.Stok = int.Parse(stokTextBox.Text);
                brg.Keterangan = keteranganTextBox.Text;
                brg.Status = int.Parse(statsucomboBox.Text);

                result = brgDao.Update(brg);

                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Diupdate!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataBarang();
                    clear();

                    satuanComboBox.Focus();
                }
                else
                {
                    MessageBox.Show("Data Gagal Diupdate!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadDataBarang();
                    satuanComboBox.Focus();
                    clear();
                }
            }
        }
        //Hapus Data
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (kdBarangText.Text.Trim() == "" || ktgrComboBox.Text.Trim() == "" || satuanComboBox.Text.Trim() == "" || namaTextBox.Text.Trim() == "" || hbTextBox.Text.Trim() == "" || hjTextBox.Text.Trim() == "" || stokTextBox.Text.Trim() == "" || keteranganTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Anda harus memilih data terlebih dahulu!!!", " Oppss...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MessageBox.Show("Apakah Anda benar ingin menghapus barang " + namaTextBox.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                result = brgDao.Hapus(kdBarangText.Text);
                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Dihapus!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataBarang();
                    satuanComboBox.Focus();
                    clear();
                }
                else
                {
                    MessageBox.Show("Data Gagal Dihapus!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadDataBarang();
                    satuanComboBox.Focus();
                    clear();
                }

            }
        }
        //Searching Data
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            LoadDataBarang(searchTextBox.Text);
        }
        //Searching Data
        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataBarang(searchTextBox.Text);
        }

        private void ktgrComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(ktgrComboBox.SelectedValue.ToString());
        }

        private void kdBarangText_TextChanged(object sender, EventArgs e)
        {
            /*urutanBarang();*/
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FormKategori frmKategori = new FormKategori();
            frmKategori.Show();
            
        }

        private void btnSatuan_Click(object sender, EventArgs e)
        {
            FormSatuan frmSatuan = new FormSatuan();
            frmSatuan.Show();
        }
    }
}

