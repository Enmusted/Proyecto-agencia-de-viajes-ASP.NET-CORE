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
    public class AeropuertsController : Controller
    {
        private readonly AgenciaVContext _context;

        public AeropuertsController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: Aeropuerts
        public async Task<IActionResult> Index()
        {
            var agenciaVContext = _context.Aeropuerts.Include(a => a.EstadoNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: Aeropuerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aeropuerts == null)
            {
                return NotFound();
            }

            var aeropuert = await _context.Aeropuerts
                .Include(a => a.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdAeropuerto == id);
            if (aeropuert == null)
            {
                return NotFound();
            }

            return View(aeropuert);
        }

        // GET: Aeropuerts/Create
        public IActionResult Create()
        {
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            return View();
        }

        // POST: Aeropuerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAeropuerto,Aeropuerto,Provincia,Pais,Estado")] Aeropuert aeropuert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aeropuert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", aeropuert.Estado);
            return View(aeropuert);
        }

        // GET: Aeropuerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aeropuerts == null)
            {
                return NotFound();
            }

            var aeropuert = await _context.Aeropuerts.FindAsync(id);
            if (aeropuert == null)
            {
                return NotFound();
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", aeropuert.Estado);
            return View(aeropuert);
        }

        // POST: Aeropuerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAeropuerto,Aeropuerto,Provincia,Pais,Estado")] Aeropuert aeropuert)
        {
            if (id != aeropuert.IdAeropuerto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aeropuert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AeropuertExists(aeropuert.IdAeropuerto))
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
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", aeropuert.Estado);
            return View(aeropuert);
        }

        // GET: Aeropuerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aeropuerts == null)
            {
                return NotFound();
            }

            var aeropuert = await _context.Aeropuerts
                .Include(a => a.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdAeropuerto == id);
            if (aeropuert == null)
            {
                return NotFound();
            }

            return View(aeropuert);
        }

        // POST: Aeropuerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aeropuerts == null)
            {
                return Problem("Entity set 'AgenciaVContext.Aeropuerts'  is null.");
            }
            var aeropuert = await _context.Aeropuerts.FindAsync(id);
            if (aeropuert != null)
            {
                aeropuert.Estado = "DESHABILITADO"; // Cambiar el estado en lugar de eliminar
                _context.Update(aeropuert); // Actualizar el estado en la base de datos
                await _context.SaveChangesAsync();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AeropuertExists(int id)
        {
          return (_context.Aeropuerts?.Any(e => e.IdAeropuerto == id)).GetValueOrDefault();
        }
    }
}
