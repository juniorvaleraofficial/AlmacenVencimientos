using AlmacenVencimientos.Models;
using Microsoft.EntityFrameworkCore;

namespace AlmacenVencimientos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Lote> Lotes => Set<Lote>();
        public DbSet<MovimientoInventario> MovimientosInventario => Set<MovimientoInventario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Índice único opcional para el código del producto
            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.Codigo)
                .IsUnique(false); // cámbialo a true si quieres obligatorio y único

            // Índice para código de lote
            modelBuilder.Entity<Lote>()
                .HasIndex(l => l.CodigoLote);

            // Relaciones
            modelBuilder.Entity<Lote>()
                .HasOne(l => l.Producto)
                .WithMany(p => p.Lotes)
                .HasForeignKey(l => l.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovimientoInventario>()
                .HasOne(m => m.Lote)
                .WithMany(l => l.Movimientos)
                .HasForeignKey(m => m.LoteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
