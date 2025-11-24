using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlmacenVencimientos.Models
{
    public class Lote
    {
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El código de lote es obligatorio")]
        [StringLength(50)]
        public string CodigoLote { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public int CantidadActual { get; set; }

        [DataType(DataType.Currency)]
        public decimal CostoUnitario { get; set; }

        [StringLength(100)]
        public string? Ubicacion { get; set; }

        // Navegación
        public Producto? Producto { get; set; }

        public ICollection<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();
    }
}
