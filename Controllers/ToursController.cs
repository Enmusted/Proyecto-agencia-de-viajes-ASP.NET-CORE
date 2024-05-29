using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgenciaViajes.Models;

namespace AgenciaViajes.Controllers
{
    public class ToursController : Controller
    {
        private readonly AgenciaVContext _context;

        public ToursController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: Tours
        public async Task<IActionResult> Index()
        {
            var agenciaVContext = _context.Tours.Include(t => t.EstadoNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: Tours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tours == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .Include(t => t.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // GET: Tours/Create
        public IActionResult Create()
        {
            var paisesfiltro = _context.Aeropuerts.Where(m => m.Estado == "HABILITADO").Select(a => a.Pais).Distinct().ToList();

            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            ViewData["PaisOrigen"] = new SelectList(paisesfiltro);
            return View();
        }

        // POST: Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Precio,Descripción,NombreTour,Destinos,Pais,FechaInicio,FechaFin,DatosImagen,Capacidad,Estado")] Tour tour, IFormFile imagen)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(tour.Estado))
                {
                    tour.Estado = "HABILITADO";
                }

                if (imagen != null && imagen.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagen.CopyToAsync(memoryStream);
                        tour.DatosImagen = memoryStream.ToArray();
                    }
                }

                _context.Add(tour);
                await _context.SaveChangesAsync();
                // Después de guardar exitosamente, redirigir al usuario a la acción "Index" del controlador actual
                return RedirectToAction(nameof(Index));
            }
            var paisesfiltro = _context.Aeropuerts.Where(m => m.Estado == "HABILITADO").Select(a => a.Pais).Distinct().ToList();

            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", tour.Estado);
            ViewData["PaisOrigen"] = new SelectList(paisesfiltro);
            return View(tour);
        }

        // GET: Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tours == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            var paisesfiltro = _context.Aeropuerts.Where(m => m.Estado == "HABILITADO").Select(a => a.Pais).Distinct().ToList();

            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", tour.Estado);
            ViewData["PaisOrigen"] = new SelectList(paisesfiltro);
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Precio,Descripción,NombreTour,Destinos,Pais,FechaInicio,FechaFin,DatosImagen,Capacidad,Estado")] Tour tour, IFormFile imagen)
        {
            if (id != tour.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    if (imagen != null && imagen.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await imagen.CopyToAsync(memoryStream);
                            tour.DatosImagen = memoryStream.ToArray();
                        }
                    }

                    _context.Update(tour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.IdProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var paisesfiltro = _context.Aeropuerts.Where(m => m.Estado == "HABILITADO").Select(a => a.Pais).Distinct().ToList();

            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", tour.Estado);
            ViewData["PaisOrigen"] = new SelectList(paisesfiltro);
            return View(tour);
        }

        // GET: Tours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tours == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .Include(t => t.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tours == null)
            {
                return Problem("Entity set 'AgenciaVContext.Tours'  is null.");
            }
            var tour = await _context.Tours.FindAsync(id);
            if (tour != null)
            {
                tour.Estado = "DESHABILITADO"; // Cambiar el estado en lugar de eliminar
                _context.Update(tour); // Actualizar el estado en la base de datos
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
          return (_context.Tours?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
