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

namespace kasirSederhana.View.User
{
    public partial class FormUser : Form
    {
        private DBConection conn = null;
        private userDao usrDao = null;

        //untuk menampung return value dari operasi CRUD
        private int result = 0;
        public FormUser()
        {
            //membuat object conn untuk menghandle koneksi ke database
            conn = DBConection.GetInstance();

            //membuat object brgDao untuk mengakses operasi database
            usrDao = new userDao(conn.GetConnection());
            
            InitializeComponent();
            LoadDataUser();
            clear();
        }
        void clear()
        {
            textBoxId.Enabled = false;
            textBoxUsername.Text = "";
            textBoxId.Text = "";
            textBoxNama.Text = "";
            textBoxPassword.Text = "";
            textBoxKeterangan.Text = "";
            textBoxPencarian.Text = "";
        }
        private void LoadDataUser(string nama)
        {
            listView1.Items.Clear();

            List<userModel> daftarUsr = usrDao.GetByName(nama);
            foreach (userModel brg in daftarUsr)
            {
                FillToListView(brg); // panggil method FillToListView
            }
        }

        //VIEW DATA 
        private void FillToListView(userModel user)
        {

            int noUrut = listView1.Items.Count + 1;

            ListViewItem item = new ListViewItem(noUrut.ToString());

            item.SubItems.Add(user.Nama);
            item.SubItems.Add(user.Username);
            item.SubItems.Add(user.Password);
            item.SubItems.Add(user.Keterangan);
            item.SubItems.Add(user.Id.ToString());




            listView1.Items.Add(item);

        }
        private void LoadDataUser()
        {
            listView1.Items.Clear();

            List<userModel> daftarUser = usrDao.GetAll();
            foreach (userModel user in daftarUser)
            {
                FillToListView(user); // panggil method FillToListView
                /*ktgrComboBox.Items.Add(brg.NamaBarang);*/
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxKeterangan.Text.Trim() == "" || textBoxNama.Text.Trim() == "" || textBoxPassword.Text.Trim() == "" || textBoxUsername.Text.Trim() == "" )
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //buat object brg
                userModel brg = new userModel();
                //isi nilai masing properti
                brg.Nama = textBoxNama.Text;
                brg.Username = textBoxUsername.Text;
                brg.Password = textBoxPassword.Text;
                brg.Keterangan = textBoxKeterangan.Text;
                

                result = usrDao.Tambah(brg);

                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Disimpan!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadDataUser();

                }
                else
                {
                    MessageBox.Show("Data Gagal Disimpan!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clear();
                    LoadDataUser();
                    
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxKeterangan.Text.Trim() == "" || textBoxNama.Text.Trim() == "" || textBoxPassword.Text.Trim() == "" || textBoxUsername.Text.Trim() == "")
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //buat object brg
                userModel brg = new userModel();
                //isi nilai masing properti
                brg.Nama = textBoxNama.Text;
                brg.Username = textBoxUsername.Text;
                brg.Password = textBoxPassword.Text;
                brg.Keterangan = textBoxKeterangan.Text;
                brg.Id = int.Parse(textBoxId.Text);


                result = usrDao.Update(brg);

                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Disimpan!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadDataUser();

                }
                else
                {
                    MessageBox.Show("Data Gagal Disimpan!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clear();
                    LoadDataUser();

                }
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string nama = this.listView1.SelectedItems[0].SubItems[1].Text;
                string username = this.listView1.SelectedItems[0].SubItems[2].Text;
                string password = this.listView1.SelectedItems[0].SubItems[3].Text;
                string keterangan = this.listView1.SelectedItems[0].SubItems[4].Text;
                string id = this.listView1.SelectedItems[0].SubItems[5].Text;
                


                textBoxNama.Text = nama;
                textBoxUsername.Text = username;
                textBoxPassword.Text = password;
                textBoxKeterangan.Text = keterangan;
                 textBoxId.Text= id;
                
                button1.Enabled = false;

            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxKeterangan.Text.Trim() == "" || textBoxNama.Text.Trim() == "" || textBoxPassword.Text.Trim() == "" || textBoxUsername.Text.Trim() == "")
            {
                MessageBox.Show("Anda Harus Mengisi Form Terlebih Dahulu!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MessageBox.Show("Apakah Anda benar ingin menghapus barang " + textBoxNama.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //buat object brg
                userModel brg = new userModel();
                //isi nilai masing properti
               
                brg.Id = int.Parse(textBoxId.Text);


                result = usrDao.Hapus(brg.Id);

                if (result > 0)
                {
                    MessageBox.Show("Data Berhasil Dihapus!!", " Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadDataUser();

                }
                else
                {
                    MessageBox.Show("Data Gagal Dihapus!!", " Oppsss....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clear();
                    LoadDataUser();

                }
            }
        }

        private void textBoxPencarian_TextChanged(object sender, EventArgs e)
        {
            LoadDataUser(textBoxPencarian.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
            LoadDataUser();
        }

    }

}

