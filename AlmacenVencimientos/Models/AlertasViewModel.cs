using System;
using System.Collections.Generic;

namespace AlmacenVencimientos.Models
{
    public class AlertasViewModel
    {
        public int DiasAvisoVencimiento { get; set; }
        public int StockMinimo { get; set; }

        public List<Lote> LotesVencidos { get; set; } = new();
        public List<Lote> LotesPorVencer { get; set; } = new();
        public List<Lote> LotesStockBajo { get; set; } = new();
    }
}
