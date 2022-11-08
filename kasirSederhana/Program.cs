using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using kasirSederhana.View.Barang;
using kasirSederhana.View.Suplier;
using kasirSederhana.View.Satuan;
using kasirSederhana.View.Kategori;
using kasirSederhana.View.User;
using kasirSederhana.View.Pelanggan;
using kasirSederhana.View.Penjualan;
using kasirSederhana.View.Pembelian;
using kasirSederhana.View.Laporan;

namespace kasirSederhana
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMainMenu());
            //Application.Run(new FormLogin());
            //Application.Run(new FormBarang());
            //Application.Run(new FormSuplier());
            //Application.Run(new FormSatuan());
            //Application.Run(new FormKategori());
            //Application.Run(new FormUser());
            //Application.Run(new FormPelanggan());
            //Application.Run(new FormPenjualan());
            //Application.Run(new FormBarangSearch());
            //Application.Run(new FormPembelian());
            Application.Run(new FormLaporanPenjualan());
        }
    }
}



