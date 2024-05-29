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
    public class HotelsController : Controller
    {
        private readonly AgenciaVContext _context;

        public HotelsController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            var agenciaVContext = _context.Hotels.Include(h => h.EstadoNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdHotel == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            ViewData["PaisOrigen"] = new SelectList(_context.Aeropuerts, "Pais", "Pais");
            ViewData["ProvinciaOrigen"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHotel,Nombre,Provincia,Direccion,Estado")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(hotel.Estado))
                {
                    hotel.Estado = "HABILITADO"; // Establece el valor por defecto si no se especifica
                }
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", hotel.Estado);
            ViewData["PaisOrigen"] = new SelectList(_context.Aeropuerts, "Pais", "Pais");
            ViewData["ProvinciaOrigen"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", hotel.Estado);
            ViewData["PaisOrigen"] = new SelectList(_context.Aeropuerts, "Pais", "Pais");
            ViewData["ProvinciaOrigen"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHotel,Nombre,Provincia,Direccion,Estado")] Hotel hotel)
        {
            if (id != hotel.IdHotel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.IdHotel))
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
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", hotel.Estado);
            ViewData["PaisOrigen"] = new SelectList(_context.Aeropuerts, "Pais", "Pais");
            ViewData["ProvinciaOrigen"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdHotel == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hotels == null)
            {
                return Problem("Entity set 'AgenciaVContext.Hotels'  is null.");
            }
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                hotel.Estado = "DESHABILITADO"; // Cambiar el estado en lugar de eliminar
                _context.Update(hotel); // Actualizar el estado en la base de datos
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetProvincias(string pais)
        {
            // Obtén las provincias correspondientes al país desde tu base de datos
            var provincias = _context.Aeropuerts
                .Where(a => a.Pais == pais)
                .Select(a => a.Provincia)
                .Distinct()
                .ToList();

            return Json(provincias);
        }
        private bool HotelExists(int id)
        {
            return (_context.Hotels?.Any(e => e.IdHotel == id)).GetValueOrDefault();
        }
    }
}
