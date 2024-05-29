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
    public class AerolineasController : Controller
    {
        private readonly AgenciaVContext _context;

        public AerolineasController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: Aerolineas
        public async Task<IActionResult> Index()
        {
            var agenciaVContext = _context.Aerolineas.Include(a => a.EstadoNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: Aerolineas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aerolineas == null)
            {
                return NotFound();
            }

            var aerolinea = await _context.Aerolineas
                .Include(a => a.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdAerolinea == id);
            if (aerolinea == null)
            {
                return NotFound();
            }

            return View(aerolinea);
        }

        // GET: Aerolineas/Create
        public IActionResult Create()
        {
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            return View();
        }

        // POST: Aerolineas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAerolinea,Aerolinea1,Estado")] Aerolinea aerolinea)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(aerolinea.Estado))
                {
                    aerolinea.Estado = "HABILITADO"; // Establece el valor por defecto si no se especifica
                }

                _context.Add(aerolinea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", aerolinea.Estado);
            return View(aerolinea);
        }

        // GET: Aerolineas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aerolineas == null)
            {
                return NotFound();
            }

            var aerolinea = await _context.Aerolineas.FindAsync(id);
            if (aerolinea == null)
            {
                return NotFound();
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", aerolinea.Estado);
            return View(aerolinea);
        }

        // POST: Aerolineas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAerolinea,Aerolinea1,Estado")] Aerolinea aerolinea)
        {
            if (id != aerolinea.IdAerolinea)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aerolinea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AerolineaExists(aerolinea.IdAerolinea))
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
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", aerolinea.Estado);
            return View(aerolinea);
        }

        // GET: Aerolineas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aerolineas == null)
            {
                return NotFound();
            }

            var aerolinea = await _context.Aerolineas
                .Include(a => a.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdAerolinea == id);
            if (aerolinea == null)
            {
                return NotFound();
            }

            return View(aerolinea);
        }

        // POST: Aerolineas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aerolineas == null)
            {
                return Problem("Entity set 'AgenciaVContext.Aerolineas'  is null.");
            }
            var aerolinea = await _context.Aerolineas.FindAsync(id);
            if (aerolinea != null)
            {
                aerolinea.Estado = "DESHABILITADO"; // Cambiar el estado en lugar de eliminar
                _context.Update(aerolinea); // Actualizar el estado en la base de datos
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool AerolineaExists(int id)
        {
          return (_context.Aerolineas?.Any(e => e.IdAerolinea == id)).GetValueOrDefault();
        }
    }
}
