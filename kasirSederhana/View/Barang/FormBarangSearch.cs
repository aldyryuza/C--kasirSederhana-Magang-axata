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
using kasirSederhana.View.Penjualan;
using FirebirdSql.Data.FirebirdClient;

namespace kasirSederhana.View.Barang
{
    public partial class FormBarangSearch : Form
    {

        private DBConection conn = null;
        private barangDao brgDao = null;
        public static string passing = "";

        //untuk menampung return value dari operasi CRUD
        private int result = 0;
        int dataPassing1;
        string dataPasing;
        public FormBarangSearch(string data, int rowId)
        {
            //membuat object conn untuk menghandle koneksi ke database
            conn = DBConection.GetInstance();

            InitializeComponent();
            dataPasing =  data;
            dataPassing1 = rowId;
            loadBarang(dataPasing);
            textBox1.Enabled = false;
            
        }
        
        //VIEW DATA 
       

        private void loadBarang(string nama)
        {
       
            string strsql = "select barang.kode, barang.harga_beli ,barang.nama,barang.harga_jual,satuan.nama as satuan from barang inner join satuan on barang.satuan_id = satuan.id  where barang.nama LIKE '%" + nama + "%'";
            using (FbCommand cmd = new FbCommand(strsql, conn.GetConnection()))
            {
                using (FbDataReader dtr = cmd.ExecuteReader())
                {
                    
                    while (dtr.Read())
                    {
                        int noUrut = listView1.Items.Count + 1;

                        ListViewItem item = new ListViewItem(noUrut.ToString());

                        item.SubItems.Add(dtr["nama"].ToString());
                        item.SubItems.Add(dtr["harga_jual"].ToString());
                        item.SubItems.Add(dtr["satuan"].ToString());
                        item.SubItems.Add(dtr["kode"].ToString());
                        item.SubItems.Add(dtr["harga_beli"].ToString());
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
                string nSatuan = this.listView1.SelectedItems[0].SubItems[2].Text;
                string nHj = this.listView1.SelectedItems[0].SubItems[3].Text;
                string nKode = this.listView1.SelectedItems[0].SubItems[4].Text;
                string nHb = this.listView1.SelectedItems[0].SubItems[5].Text;





                FormPenjualan form = (FormPenjualan)Application.OpenForms["FormPenjualan"];
                form.dataGridView1.Rows[dataPassing1].Cells[0].Value = nNama;
                form.dataGridView1.Rows[dataPassing1].Cells[3].Value = nSatuan;
                form.dataGridView1.Rows[dataPassing1].Cells[1].Value = 1;
                form.dataGridView1.Rows[dataPassing1].Cells[2].Value = nHj;
                form.dataGridView1.Rows[dataPassing1].Cells[5].Value = nKode;
                form.dataGridView1.Rows[dataPassing1].Cells[6].Value = nHb;


                this.Close();
                



            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
//            this.DialogResult = DialogResult.OK;
           

        }
    }
}
