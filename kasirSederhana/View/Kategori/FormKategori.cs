using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using kasirSederhana.Model;
using kasirSederhana.Dao;

namespace kasirSederhana.View.Kategori
{
    public partial class FormKategori : Form
    {
        private DBConection conn = null;
        private kategoriDao katDao = null;

        //untuk menampung return value dari operasi CRUD
        private int result = 0;
        public FormKategori()
        {
            InitializeComponent();
            //membuat object conn untuk menghandle koneksi ke database
            conn = DBConection.GetInstance();

            //membuat object brgDao untuk mengakses operasi database
            katDao = new kategoriDao(conn.GetConnection());
            LoadDataSuplier();
            clear();
        }
        void clear()
        {
            textBoxId.Enabled = false;
            textBoxKeterangan.Text = "";
            textBoxNama.Text = "";
            textBoxPencarian.Text = "";
            textBoxId.Text = "";
            button1.Enabled = true;
        }
        private void FillToListView(kategoriModel brg)
        {

            int noUrut = listView1.Items.Count + 1;

            ListViewItem item = new ListViewItem(noUrut.ToString());

            item.SubItems.Add(brg.Nama);
            item.SubItems.Add(brg.Keterangan);
            item.SubItems.Add(brg.Id.ToString());




            listView1.Items.Add(item);

        }
        private void LoadDataSuplier()
        {
            listView1.Items.Clear();

            List<kategoriModel> dftSuplier = katDao.GetAll();
            foreach (kategoriModel spl in dftSuplier)
            {
                FillToListView(spl); // panggil method FillToListView
            }
        }
        private void LoadDataSuplier(string nama)
        {
            listView1.Items.Clear();

            List<kategoriModel> daftarSpl = katDao.GetByName(nama);
            foreach (kategoriModel sup in daftarSpl)
            {
                FillToListView(sup); // panggil method FillToListView
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text.Trim() == "" || textBoxKeterangan.Text.Trim() == "")
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //buat object Suplier dahulu
                kategoriModel sat = new kategoriModel();
                //isi nilai masing" propereti
                sat.Nama = textBoxNama.Text;
                sat.Keterangan = textBoxKeterangan.Text;

                result = katDao.Tambah(sat);
                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Disimpan!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadDataSuplier();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Data Gagal Disimpan!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clear();
                    LoadDataSuplier();
                    this.Close();

                }
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string nNama = this.listView1.SelectedItems[0].SubItems[1].Text;
                string nKeterangan = this.listView1.SelectedItems[0].SubItems[2].Text;
                string nId = this.listView1.SelectedItems[0].SubItems[3].Text;

                textBoxNama.Text = nNama;

                textBoxKeterangan.Text = nKeterangan;
                textBoxId.Text = nId;

                button1.Enabled = false;
            }
            catch (Exception G)
            {

                MessageBox.Show(G.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text.Trim() == "" || textBoxKeterangan.Text.Trim() == "")
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //buat object Suplier dahulu
                kategoriModel sat = new kategoriModel();
                //isi nilai masing" propereti
                sat.Nama = textBoxNama.Text;
                sat.Keterangan = textBoxKeterangan.Text;
                sat.Id = int.Parse(textBoxId.Text);

                result = katDao.Update(sat);
                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Diupdate!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadDataSuplier();
                }
                else
                {
                    MessageBox.Show("Data Gagal Diupdate!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clear();
                    LoadDataSuplier();

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text.Trim() == "" || textBoxKeterangan.Text.Trim() == "")
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MessageBox.Show("Apakah Anda benar ingin menghapus barang " + textBoxNama.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //buat object Suplier dahulu
                kategoriModel kat = new kategoriModel();
                //isi nilai masing" propereti
                kat.Id = int.Parse(textBoxId.Text);

                result = katDao.Hapus(kat.Id);
                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Dihapus!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadDataSuplier();
                }
                else
                {
                    MessageBox.Show("Data Gagal Dihapus!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clear();
                    LoadDataSuplier();

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
            LoadDataSuplier();
        }

        private void textBoxPencarian_TextChanged(object sender, EventArgs e)
        {
            LoadDataSuplier(textBoxPencarian.Text);
        }
    }
}
