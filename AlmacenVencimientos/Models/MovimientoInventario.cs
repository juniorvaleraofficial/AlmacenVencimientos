using System;
using System.ComponentModel.DataAnnotations;

namespace AlmacenVencimientos.Models
{
    public enum TipoMovimiento
    {
        Entrada = 1,
        Salida = 2,
        Ajuste = 3
    }

    public class MovimientoInventario
    {
        public int Id { get; set; }

        [Required]
        public int LoteId { get; set; }

        [Required]
        public TipoMovimiento Tipo { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [StringLength(200)]
        public string? Motivo { get; set; }

        // Navegación
        public Lote? Lote { get; set; }
    }
}
