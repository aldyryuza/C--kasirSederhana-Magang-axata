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


namespace kasirSederhana.View.Suplier
{
    public partial class FormSuplier : Form
    {

        private DBConection conn = null;
        private suplierDao supDao = null;

        //untuk menampung return value dari operasi CRUD
        private int result = 0;

        public FormSuplier()
        {
        
        InitializeComponent();
            //membuat object conn untuk menghandle koneksi ke database
            conn = DBConection.GetInstance();

            //membuat object brgDao untuk mengakses operasi database
            supDao = new suplierDao(conn.GetConnection());
            LoadDataSuplier();
            clear();
        }
        //Method Clear
        void clear()
        {
            textBoxNama.Text = "";
            textBoxAlamat.Text = "";
            textBoxKeterangan.Text = "";
            textBoxTelepon.Text = "";
            idSuplierTExt.Enabled = false;
            idSuplierTExt.Text = "";
            textBoxSeacrh.Text = "";
        }

        // function Search Data
        private void LoadDataSuplier(string nama)
        {
            listView1.Items.Clear();

            List<suplierModel> daftarSpl = supDao.GetByNameSuplier(nama);
            foreach (suplierModel sup in daftarSpl)
            {
                FillToListView(sup); // panggil method FillToListView
            }
        }


        //View Data
        private void FillToListView(suplierModel brg)
        {

            int noUrut = listView1.Items.Count + 1;

            ListViewItem item = new ListViewItem(noUrut.ToString());

            item.SubItems.Add(brg.Nama);
            item.SubItems.Add(brg.Alamat);
            item.SubItems.Add(brg.Telepon);
            item.SubItems.Add(brg.Keterangan);
            item.SubItems.Add(brg.Id.ToString());




            listView1.Items.Add(item);

        }
        private void LoadDataSuplier()
        {
            listView1.Items.Clear();

            List<suplierModel> dftSuplier = supDao.GetAllSuplier();
            foreach (suplierModel spl in dftSuplier)
            {
                FillToListView(spl); // panggil method FillToListView
            }
        }

        //Simpan Data
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text.Trim() == "" || textBoxAlamat.Text.Trim() == "" || textBoxKeterangan.Text.Trim() == "" || textBoxTelepon.Text.Trim() == "")
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //buat object Suplier dahulu
                suplierModel spl = new suplierModel();
                //isi nilai masing" propereti
                spl.Nama = textBoxNama.Text;
                spl.Alamat = textBoxAlamat.Text;
                spl.Telepon = textBoxTelepon.Text; ;
                spl.Keterangan = textBoxKeterangan.Text;

                result = supDao.TambahSuplier(spl);

                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Disimpan!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadDataSuplier();
                }
                else
                {
                    MessageBox.Show("Data Gagal Disimpan!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clear();
                    LoadDataSuplier();
                    textBoxSeacrh.Focus();
                }
            }
        }

        //Ambil Value Dari ListView
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string nNama = this.listView1.SelectedItems[0].SubItems[1].Text;
                string nAlamat = this.listView1.SelectedItems[0].SubItems[2].Text;
                string nTelepon = this.listView1.SelectedItems[0].SubItems[3].Text;
                string nKeterangan = this.listView1.SelectedItems[0].SubItems[4].Text;
                string nId = this.listView1.SelectedItems[0].SubItems[5].Text;

                textBoxNama.Text = nNama;
                textBoxAlamat.Text = nAlamat;
                textBoxTelepon.Text = nTelepon;
                textBoxKeterangan.Text = nKeterangan;
                idSuplierTExt.Text = nId;

                button1.Enabled = false;
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
        }
        //Update Data
        private void button2_Click(object sender, EventArgs e)
        {
            if (
                textBoxNama.Text.Trim() == "" 
                || textBoxAlamat.Text.Trim() == "" 
                || textBoxKeterangan.Text.Trim() == "" 
                || textBoxTelepon.Text.Trim() == ""
                )
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //buat object Suplier dahulu
                suplierModel spl = new suplierModel();
                //isi nilai masing" propereti
                spl.Nama = textBoxNama.Text;
                spl.Alamat = textBoxAlamat.Text;
                spl.Telepon = textBoxTelepon.Text; ;
                spl.Keterangan = textBoxKeterangan.Text;
                spl.Id = int.Parse(idSuplierTExt.Text);

                result = supDao.UpdateSuplier(spl);

                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Diupdate!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadDataSuplier();
                }
                else
                {
                    MessageBox.Show("Data Gagal Diupdate!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadDataSuplier();
                    textBoxSeacrh.Focus();
                }
            }
        }
        //Delete Data
        private void button3_Click(object sender, EventArgs e)
        {
            if (
                textBoxNama.Text.Trim() == ""
                || textBoxAlamat.Text.Trim() == ""
                || textBoxKeterangan.Text.Trim() == ""
                || textBoxTelepon.Text.Trim() == ""
                )
            {
                MessageBox.Show("Anda Harus memlih data dulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MessageBox.Show("Apakah Anda benar ingin menghapus barang " + textBoxNama.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                result = supDao.HapusSuplier(int.Parse(idSuplierTExt.Text));
                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Dihapus!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadDataSuplier();

                    textBoxSeacrh.Focus();
                }
                else
                {
                    MessageBox.Show("Data Gagal Dihapus!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clear();
                    LoadDataSuplier();
                    textBoxSeacrh.Focus();
                }

            }
        }

        //Refresh
        private void button4_Click(object sender, EventArgs e)
        {
            clear();
            LoadDataSuplier();
            button1.Enabled = true;

        }

        private void textBoxSeacrh_TextChanged(object sender, EventArgs e)
        {
            LoadDataSuplier(textBoxSeacrh.Text);
        }
    }
    }

