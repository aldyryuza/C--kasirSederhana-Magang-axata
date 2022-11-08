using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kasirSederhana.Model
{
    public class detailPembelianModel
    {
        public int Id { get; set; }
        public int PembelianId { get; set; }
        public string KodeBarang { get; set; }
        public int Jumlah { get; set; }
        public int HargaBeli { get; set; }
        public int JumlahHarga { get; set; }
        public int TotalHarga { get; set; }
    }
}
