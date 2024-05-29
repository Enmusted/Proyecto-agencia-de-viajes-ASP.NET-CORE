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
    public class ThabitacionsController : Controller
    {
        private readonly AgenciaVContext _context;

        public ThabitacionsController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: Thabitacions
        public async Task<IActionResult> Index()
        {
            var agenciaVContext = _context.Thabitacions.Include(t => t.IdHotelNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: Thabitacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Thabitacions == null)
            {
                return NotFound();
            }

            var thabitacion = await _context.Thabitacions
                .Include(t => t.IdHotelNavigation)
                .FirstOrDefaultAsync(m => m.IdProductoTh == id);
            if (thabitacion == null)
            {
                return NotFound();
            }

            return View(thabitacion);
        }

        // GET: Thabitacions/Create
        public IActionResult Create()
        {
            var modeloHotels = _context.Hotels.Where(m => m.Estado == "HABILITADO").ToList();

            ViewData["IdHotel"] = new SelectList(modeloHotels, "IdHotel", "Nombre");
            return View();
        }

        // POST: Thabitacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProductoTh,IdHotel,TipoHabitacion,Habitacion,Precio,CantidadPersonas,Disponibilidad")] Thabitacion thabitacion)
        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrWhiteSpace(thabitacion.Disponibilidad))
                {
                    thabitacion.Disponibilidad = "DISPONIBLE"; // Establece el valor por defecto si no se especifica
                }


                _context.Add(thabitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "Nombre", thabitacion.IdHotel);
            return View(thabitacion);
        }

        // GET: Thabitacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Thabitacions == null)
            {
                return NotFound();
            }

            var thabitacion = await _context.Thabitacions.FindAsync(id);
            if (thabitacion == null)
            {
                return NotFound();
            }
            var modeloHotels = _context.Hotels.Where(m => m.Estado == "HABILITADO").ToList();

            ViewData["IdHotel"] = new SelectList(modeloHotels, "IdHotel", "Nombre", thabitacion.IdHotel);
            return View(thabitacion);
        }

        // POST: Thabitacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProductoTh,IdHotel,TipoHabitacion,Habitacion,Precio,CantidadPersonas,Disponibilidad")] Thabitacion thabitacion)
        {
            if (id != thabitacion.IdProductoTh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thabitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThabitacionExists(thabitacion.IdProductoTh))
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
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "IdHotel", thabitacion.IdHotel);
            return View(thabitacion);
        }

        // GET: Thabitacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Thabitacions == null)
            {
                return NotFound();
            }

            var thabitacion = await _context.Thabitacions
                .Include(t => t.IdHotelNavigation)
                .FirstOrDefaultAsync(m => m.IdProductoTh == id);
            if (thabitacion == null)
            {
                return NotFound();
            }

            return View(thabitacion);
        }

        // POST: Thabitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Thabitacions == null)
            {
                return Problem("Entity set 'AgenciaVContext.Thabitacions'  is null.");
            }
            var thabitacion = await _context.Thabitacions.FindAsync(id);
            if (thabitacion != null)
            {
                _context.Thabitacions.Remove(thabitacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThabitacionExists(int id)
        {
            return (_context.Thabitacions?.Any(e => e.IdProductoTh == id)).GetValueOrDefault();
        }
    }
}
