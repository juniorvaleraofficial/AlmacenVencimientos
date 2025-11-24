using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AlmacenVencimientos.Data;
using AlmacenVencimientos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlmacenVencimientos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var hoy = DateTime.Today;

            var productosCount = await _context.Productos.CountAsync();
            var lotes = await _context.Lotes
                .Include(l => l.Producto)
                .ToListAsync();
            var movimientosCount = await _context.MovimientosInventario.CountAsync();

            var v7 = hoy.AddDays(7);
            var v15 = hoy.AddDays(15);
            var v30 = hoy.AddDays(30);

            var model = new DashboardViewModel
            {
                TotalProductos = productosCount,
                TotalLotes = lotes.Count,
                TotalLotesConStock = lotes.Count(l => l.CantidadActual > 0),
                TotalMovimientos = movimientosCount,

                LotesVencidos = lotes.Count(l =>
                    l.CantidadActual > 0 && l.FechaVencimiento < hoy),

                LotesVencen7Dias = lotes.Count(l =>
                    l.CantidadActual > 0 &&
                    l.FechaVencimiento >= hoy &&
                    l.FechaVencimiento <= v7),

                LotesVencen8a15 = lotes.Count(l =>
                    l.CantidadActual > 0 &&
                    l.FechaVencimiento > v7 &&
                    l.FechaVencimiento <= v15),

                LotesVencen16a30 = lotes.Count(l =>
                    l.CantidadActual > 0 &&
                    l.FechaVencimiento > v15 &&
                    l.FechaVencimiento <= v30),

                ProximosAVencer = lotes
                    .Where(l => l.CantidadActual > 0 && l.FechaVencimiento >= hoy)
                    .OrderBy(l => l.FechaVencimiento)
                    .Take(5)
                    .ToList()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
