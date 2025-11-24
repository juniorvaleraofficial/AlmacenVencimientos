using System;
using System.Collections.Generic;

namespace AlmacenVencimientos.Models
{
    public class DashboardViewModel
    {
        public int TotalProductos { get; set; }
        public int TotalLotes { get; set; }
        public int TotalLotesConStock { get; set; }
        public int TotalMovimientos { get; set; }

        public int LotesVencidos { get; set; }
        public int LotesVencen7Dias { get; set; }
        public int LotesVencen8a15 { get; set; }
        public int LotesVencen16a30 { get; set; }

        public List<Lote> ProximosAVencer { get; set; } = new();
    }
}
