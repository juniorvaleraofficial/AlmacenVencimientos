using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlmacenVencimientos.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [StringLength(100)]
        public string? Categoria { get; set; }

        [StringLength(50)]
        public string? Codigo { get; set; }

        public bool Activo { get; set; } = true;

        // Relación 1 - N con Lotes
        public ICollection<Lote> Lotes { get; set; } = new List<Lote>();
    }
}
