using System;
using System.Linq;
using System.Threading.Tasks;
using AlmacenVencimientos.Data;
using AlmacenVencimientos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlmacenVencimientos.Controllers
{
    public class ReportesController : Controller
    {
        private readonly AppDbContext _context;

        public ReportesController(AppDbContext context)
        {
            _context = context;
        }

        // Reporte de inventario general por lote
        public async Task<IActionResult> Inventario()
        {
            var lotes = await _context.Lotes
                .Include(l => l.Producto)
                .OrderBy(l => l.Producto!.Nombre)
                .ThenBy(l => l.FechaVencimiento)
                .ToListAsync();

            return View(lotes);
        }

        // Reporte de próximos a vencer (días configurables)
        public async Task<IActionResult> ProximosVencer(int? dias)
        {
            int diasAviso = dias ?? 30;
            var hoy = DateTime.Today;
            var hasta = hoy.AddDays(diasAviso);

            var lotes = await _context.Lotes
                .Include(l => l.Producto)
                .Where(l => l.CantidadActual > 0 &&
                            l.FechaVencimiento >= hoy &&
                            l.FechaVencimiento <= hasta)
                .OrderBy(l => l.FechaVencimiento)
                .ToListAsync();

            ViewData["Dias"] = diasAviso;

            return View(lotes);
        }
    }
}
