using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kasirSederhana.Model
{
    public  class detailPejualanModel
    {
        public int Id { get; set; }
        public int PenjualanId { get; set; }
        public string KodeBarang { get; set; }
        public int Jumlah { get; set; }
        public int HargaJual { get; set; }
        public int HargaBeli { get; set; }
        public int JumlahHarga { get; set; }

    }
}

