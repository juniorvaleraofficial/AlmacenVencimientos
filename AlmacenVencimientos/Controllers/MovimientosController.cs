using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlmacenVencimientos.Data;
using AlmacenVencimientos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlmacenVencimientos.Controllers
{
    public class MovimientosController : Controller
    {
        private readonly AppDbContext _context;

        public MovimientosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Movimientos
        public async Task<IActionResult> Index()
        {
            var movimientos = _context.MovimientosInventario
                .Include(m => m.Lote)
                    .ThenInclude(l => l.Producto)
                .OrderByDescending(m => m.Fecha);

            return View(await movimientos.ToListAsync());
        }

        // GET: Movimientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _context.MovimientosInventario
                .Include(m => m.Lote)
                    .ThenInclude(l => l.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // GET: Movimientos/Create
        public IActionResult Create()
        {
            CargarLotesDropDown();
            CargarTiposMovimiento();
            return View();
        }

        // POST: Movimientos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoteId,Tipo,Cantidad,Fecha,Motivo")] MovimientoInventario movimiento)
        {
            if (!ModelState.IsValid)
            {
                CargarLotesDropDown(movimiento.LoteId);
                CargarTiposMovimiento(movimiento.Tipo);
                return View(movimiento);
            }

            var lote = await _context.Lotes.FindAsync(movimiento.LoteId);
            if (lote == null)
            {
                ModelState.AddModelError("", "El lote seleccionado no existe.");
                CargarLotesDropDown(movimiento.LoteId);
                CargarTiposMovimiento(movimiento.Tipo);
                return View(movimiento);
            }

            // Reglas de negocio simples:
            // Entrada: suma cantidad
            // Salida: resta cantidad (no permite negativo)
            // Ajuste: interpreta Cantidad como NUEVA existencia
            switch (movimiento.Tipo)
            {
                case TipoMovimiento.Entrada:
                    lote.CantidadActual += movimiento.Cantidad;
                    break;

                case TipoMovimiento.Salida:
                    if (movimiento.Cantidad > lote.CantidadActual)
                    {
                        ModelState.AddModelError("", "No hay suficiente existencia en el lote para esta salida.");
                        CargarLotesDropDown(movimiento.LoteId);
                        CargarTiposMovimiento(movimiento.Tipo);
                        return View(movimiento);
                    }
                    lote.CantidadActual -= movimiento.Cantidad;
                    break;

                case TipoMovimiento.Ajuste:
                    lote.CantidadActual = movimiento.Cantidad;
                    break;
            }

            _context.MovimientosInventario.Add(movimiento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Movimientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Para simplificar, normalmente los movimientos no se editan.
            // Podríamos desactivar esta acción o dejarla solo lectura.
            return RedirectToAction(nameof(Index));
        }

        // GET: Movimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Igualmente, para no dañar el historial,
            // mejor no borrarlos. Redirigimos a Index.
            return RedirectToAction(nameof(Index));
        }

        private void CargarLotesDropDown(int? loteSeleccionado = null)
        {
            var lotes = _context.Lotes
                .Include(l => l.Producto)
                .OrderBy(l => l.Producto!.Nombre)
                .ThenBy(l => l.FechaVencimiento)
                .ToList();

            var items = lotes.Select(l => new
            {
                l.Id,
                Descripcion = $"{l.Producto!.Nombre} - Lote {l.CodigoLote} - Vence {l.FechaVencimiento:dd/MM/yyyy}"
            });

            ViewData["LoteId"] = new SelectList(items, "Id", "Descripcion", loteSeleccionado);
        }

        private void CargarTiposMovimiento(TipoMovimiento? tipoSeleccionado = null)
        {
            var tipos = System.Enum.GetValues(typeof(TipoMovimiento))
                .Cast<TipoMovimiento>()
                .Select(t => new
                {
                    Id = t,
                    Nombre = t.ToString()
                });

            ViewData["Tipo"] = new SelectList(tipos, "Id", "Nombre", tipoSeleccionado);
        }
    }
}
