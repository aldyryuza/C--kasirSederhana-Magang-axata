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

namespace kasirSederhana.View.Pelanggan
{
    public partial class FormPelanggan : Form
    {
        private DBConection conn = null;
        private pelangganDao plgDao = null;

        //untuk menampung return value dari operasi CRUD
        private int result = 0;
        public FormPelanggan()
        {
            //membuat object conn untuk menghandle koneksi ke database
            conn = DBConection.GetInstance();

            //membuat object plgDao untuk mengakses operasi database
            plgDao = new pelangganDao(conn.GetConnection());
            InitializeComponent();
            LoadDataUser();
            clear();
            isiComboBox();
        }
        void clear()
        {
            textBox1.Text = "";
            textBoxAlamat.Text = "";
            textBoxKet.Text = "";
            textBoxNama.Text = "";
            textBoxtlp.Text = "";
            textBoxId.Enabled = false;
            comboBoxJK.Text = "pilih ---";
            btnTambah.Enabled = true;
        }
        void isiComboBox()
        {
            comboBoxJK.Items.Clear();
            comboBoxJK.Items.Add("Laki-Laki");
            comboBoxJK.Items.Add("Perempuan");
        }

        //VIEW DATA 
        private void FillToListView(pelangganModel pelanggan)
        {

            int noUrut = listView1.Items.Count + 1;

            ListViewItem item = new ListViewItem(noUrut.ToString());

            item.SubItems.Add(pelanggan.Nama);
            item.SubItems.Add(pelanggan.Alamat);
            item.SubItems.Add(pelanggan.JenisKelamin);
            item.SubItems.Add(pelanggan.NoTelp);
            item.SubItems.Add(pelanggan.Keterangan);
            item.SubItems.Add(pelanggan.Id.ToString());




            listView1.Items.Add(item);

        }
        private void LoadDataUser()
        {
            listView1.Items.Clear();

            List<pelangganModel> daftarPelanggan = plgDao.GetAll();
            foreach (pelangganModel pelanggan in daftarPelanggan)
            {
                FillToListView(pelanggan); // panggil method FillToListView
                
            }
        }
        private void LoadDataUser(string nama)
        {
            listView1.Items.Clear();

            List<pelangganModel> daftarPelanggan = plgDao.GetByName(nama);
            foreach (pelangganModel plg in daftarPelanggan)
            {
                FillToListView(plg); // panggil method FillToListView
            }
        }

        /*Tambah Data*/
        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text.Trim() == "" || textBoxAlamat.Text.Trim() == "" || textBoxtlp.Text.Trim() == "" || textBoxKet.Text.Trim() == "" || comboBoxJK.Text.Trim() == "")
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pelangganModel plg = new pelangganModel();
                plg.Nama = textBoxNama.Text;
                plg.Alamat = textBoxAlamat.Text;
                plg.JenisKelamin = comboBoxJK.Text;
                plg.NoTelp = textBoxtlp.Text;
                plg.Keterangan = textBoxKet.Text;
                result = plgDao.Tambah(plg);
                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Disimpan!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataUser();
                    clear();
                    isiComboBox();

                }
                else
                {
                    MessageBox.Show("Data Gagal Disimpan!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadDataUser();
                    clear();
                    isiComboBox();

                }

            }
        }
/*Ambil Data*/
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string nama = this.listView1.SelectedItems[0].SubItems[1].Text;
                string alamat = this.listView1.SelectedItems[0].SubItems[2].Text;
                string jenisKelamin = this.listView1.SelectedItems[0].SubItems[3].Text;
                string telp = this.listView1.SelectedItems[0].SubItems[4].Text;
                string keterangan = this.listView1.SelectedItems[0].SubItems[5].Text;
                string id = this.listView1.SelectedItems[0].SubItems[6].Text;



                textBoxNama.Text = nama;
                textBoxAlamat.Text = alamat;
                comboBoxJK.Text = jenisKelamin;
                textBoxKet.Text = keterangan;
                textBoxtlp.Text = telp;
                textBoxId.Text = id;

                btnTambah.Enabled = false;

            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
        }
/*Update Data*/
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text.Trim() == "" || textBoxAlamat.Text.Trim() == "" || textBoxtlp.Text.Trim() == "" || textBoxKet.Text.Trim() == "" || comboBoxJK.Text.Trim() == "")
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pelangganModel plg = new pelangganModel();
                plg.Nama = textBoxNama.Text;
                plg.Alamat = textBoxAlamat.Text;
                plg.JenisKelamin = comboBoxJK.Text;
                plg.NoTelp = textBoxtlp.Text;
                plg.Keterangan = textBoxKet.Text;
                plg.Id = int.Parse(textBoxId.Text);
                result = plgDao.Update(plg);
                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Diudpate!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataUser();
                    clear();
                    isiComboBox();

                }
                else
                {
                    MessageBox.Show("Data Gagal Diudpate!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadDataUser();
                    clear();
                    isiComboBox();

                }

            }
        }
/*Hapus Data*/
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text.Trim() == "" || textBoxAlamat.Text.Trim() == "" || textBoxtlp.Text.Trim() == "" || textBoxKet.Text.Trim() == "" || comboBoxJK.Text.Trim() == "")
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MessageBox.Show("Apakah Anda benar ingin menghapus barang " + textBoxNama.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                pelangganModel plg = new pelangganModel();
                
                plg.Id = int.Parse(textBoxId.Text);
                result = plgDao.Hapus(plg.Id);
                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Dihapus!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataUser();
                    clear();
                    isiComboBox();

                }
                else
                {
                    MessageBox.Show("Data Gagal Dihapus!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadDataUser();
                    clear();
                    isiComboBox();

                }

            }
        }
/*REFRESH Data*/
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataUser();
            clear();
            isiComboBox();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadDataUser(textBox1.Text);
        }
    }
}

