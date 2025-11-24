using System;
using System.Linq;
using System.Threading.Tasks;
using AlmacenVencimientos.Data;
using AlmacenVencimientos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlmacenVencimientos.Controllers
{
    public class AlertasController : Controller
    {
        private readonly AppDbContext _context;

        // puedes ajustar estos valores como quieras
        private const int DIAS_AVISO_VENCIMIENTO = 30;
        private const int STOCK_MINIMO = 10;

        public AlertasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var hoy = DateTime.Today;
            var fechaLimite = hoy.AddDays(DIAS_AVISO_VENCIMIENTO);

            var lotes = await _context.Lotes
                .Include(l => l.Producto)
                .ToListAsync();

            var viewModel = new AlertasViewModel
            {
                DiasAvisoVencimiento = DIAS_AVISO_VENCIMIENTO,
                StockMinimo = STOCK_MINIMO,
                LotesVencidos = lotes
                    .Where(l => l.CantidadActual > 0 && l.FechaVencimiento < hoy)
                    .OrderBy(l => l.FechaVencimiento)
                    .ToList(),

                LotesPorVencer = lotes
                    .Where(l => l.CantidadActual > 0 &&
                                l.FechaVencimiento >= hoy &&
                                l.FechaVencimiento <= fechaLimite)
                    .OrderBy(l => l.FechaVencimiento)
                    .ToList(),

                LotesStockBajo = lotes
                    .Where(l => l.CantidadActual > 0 &&
                                l.CantidadActual <= STOCK_MINIMO)
                    .OrderBy(l => l.Producto!.Nombre)
                    .ThenBy(l => l.FechaVencimiento)
                    .ToList()
            };

            return View(viewModel);
        }
    }
}
