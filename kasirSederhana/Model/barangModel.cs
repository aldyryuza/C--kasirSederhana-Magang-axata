using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kasirSederhana.Model
{
    public class barangModel
    {
        public string KodeBarang { get; set; }
        public int Satuan{ get; set; }
        public int Kategori{ get; set; }
        public string NamaBarang { get; set; }
        public double HargaBeli { get; set; }
        public double HargaJual { get; set; }
        public int Stok { get; set; }
        public string Keterangan { get; set; }
        public int Status { get; set; }

        

    }
}
