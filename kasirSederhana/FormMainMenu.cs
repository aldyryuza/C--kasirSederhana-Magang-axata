using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using kasirSederhana.View.Barang;
using kasirSederhana.View.Suplier;
using kasirSederhana.View.Satuan;
using kasirSederhana.View.Kategori;
using kasirSederhana.View.User;
using kasirSederhana.View.Pelanggan;
using kasirSederhana.View.Penjualan;
using kasirSederhana.View.Pembelian;
namespace kasirSederhana
{
    public partial class FormMainMenu : Form
    {
        public FormMainMenu()
        {
            InitializeComponent();
            
        }

        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;

        }

        private void dataBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBarang frmBarang = new FormBarang();
            frmBarang.Show();
            frmBarang.MdiParent = this;
        }

        private void dataSuplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSuplier frmSuplier = new FormSuplier();
            frmSuplier.Show();
            frmSuplier.MdiParent = this;
        }

        private void dataSatuanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSatuan frmSatuan = new FormSatuan();
            frmSatuan.Show();
            frmSatuan.MdiParent = this;
        }

        private void dataKategoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKategori frmKategori = new FormKategori();
            frmKategori.Show();
            frmKategori.MdiParent = this;
        }

        private void dataUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUser frmUser = new FormUser();
            frmUser.Show();
            frmUser.MdiParent = this;
            
        }

        private void dataPelangganToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPelanggan frmPelanggan = new FormPelanggan();
            frmPelanggan.Show();
            frmPelanggan.MdiParent = this;
            
        }

        private void penjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPenjualan frmPenjualan = new FormPenjualan();
            frmPenjualan.Show();
            frmPenjualan.MdiParent = this;
            
        }

        private void pembelianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPembelian frmPembelian = new FormPembelian();
            frmPembelian.Show();
            frmPembelian.MdiParent = this;
        }
    }
}