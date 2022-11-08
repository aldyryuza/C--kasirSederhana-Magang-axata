using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using kasirSederhana.View.Pembelian;
using FirebirdSql.Data.FirebirdClient;
using kasirSederhana.Dao;

namespace kasirSederhana.View.Barang
{
    public partial class FormBarangSearchPembelian : Form
    {
        private DBConection conn = null;
        private barangDao brgDao = null;
        public static string passing = "";

        //untuk menampung return value dari operasi CRUD
        private int result = 0;
        int dataPassing1;
        string dataPasing;
        public FormBarangSearchPembelian(string data, int rowId)
        {
            conn = DBConection.GetInstance();

            InitializeComponent();
            dataPasing = data;
            dataPassing1 = rowId;
            loadBarang(dataPasing);
            textBox1.Enabled = false;
        }
        private void loadBarang(string nama)
        {

            string strsql = "select barang.kode, barang.harga_beli ,barang.nama,satuan.nama as satuan from barang inner join satuan on barang.satuan_id = satuan.id  where barang.nama LIKE '%" + nama + "%'";
            using (FbCommand cmd = new FbCommand(strsql, conn.GetConnection()))
            {
                using (FbDataReader dtr = cmd.ExecuteReader())
                {

                    while (dtr.Read())
                    {
                        int noUrut = listView1.Items.Count + 1;

                        ListViewItem item = new ListViewItem(noUrut.ToString());

                        item.SubItems.Add(dtr["nama"].ToString());
                        item.SubItems.Add(dtr["harga_beli"].ToString());
                        item.SubItems.Add(dtr["satuan"].ToString());
                        item.SubItems.Add(dtr["kode"].ToString());
                        listView1.Items.Add(item);


                    }
                }

            }




        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string nNama = this.listView1.SelectedItems[0].SubItems[1].Text;
                string nSatuan = this.listView1.SelectedItems[0].SubItems[3].Text;
                string nHb = this.listView1.SelectedItems[0].SubItems[2].Text;
                string nKode = this.listView1.SelectedItems[0].SubItems[4].Text;
                





                FormPembelian form = (FormPembelian)Application.OpenForms["FormPembelian"];
                form.dataGridView1.Rows[dataPassing1].Cells[0].Value = nNama;
                form.dataGridView1.Rows[dataPassing1].Cells[3].Value = nHb;
                form.dataGridView1.Rows[dataPassing1].Cells[1].Value = 1;
                
                form.dataGridView1.Rows[dataPassing1].Cells[2].Value = nSatuan;
                form.dataGridView1.Rows[dataPassing1].Cells[5].Value = nKode;



                this.Close();




            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
        }
    }
}
