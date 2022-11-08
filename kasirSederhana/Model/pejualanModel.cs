using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kasirSederhana.Model
{
    public class pejualanModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PelangganId { get; set; }
        public double BiayaKirim { get; set; }
        public double Potongan { get; set; }
        public int NoBukti { get; set; }
        public double SubTotal { get; set; }
        public double Kembalian { get; set; }
        public string Tanggal { get; set; }
        public double Bayar { get; set; }
        public string Keterangan { get; set; }
    }
}


