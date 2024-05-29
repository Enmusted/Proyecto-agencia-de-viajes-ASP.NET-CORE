using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgenciaViajes.Models;
using Microsoft.Data.SqlClient;

namespace AgenciaViajes.Controllers
{
    public class VendedorsController : Controller
    {
        private readonly AgenciaVContext _context;

        public VendedorsController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: Vendedors
        public async Task<IActionResult> Index()
        {
            var agenciaVContext = _context.Vendedors.Include(v => v.EstadoNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: Vendedors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Vendedors == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedors
                .Include(v => v.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.Usuario == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // GET: Vendedors/Create
        public IActionResult Create()
        {
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            return View();
        }

        // POST: Vendedors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Usuario,Nombres,Apellidos,Contraseña,Estado")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(vendedor.Estado))
                {
                    vendedor.Estado = "HABILITADO"; // Establece el valor por defecto si no se especifica
                }
                try
                {
                    _context.Add(vendedor);
                    _context.SaveChanges(); // Intenta guardar el nuevo usuario en la base de datos

                    // Redirecciona a la acción Index si todo sale bien
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Si ocurre una excepción al guardar en la base de datos
                    if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                    {
                        // DNI duplicado, establece una variable ViewBag
                        ViewBag.DniError = true;
                        // Puedes usar esta variable en la vista para mostrar el cuadro de diálogo de error
                        return View(vendedor);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar los datos.");
                        // En caso de otros errores, agrega un error de modelo genérico
                    }
                }


            }
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", vendedor.Estado);
            return View(vendedor);
        }

        // GET: Vendedors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Vendedors == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedors.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", vendedor.Estado);
            return View(vendedor);
        }

        // POST: Vendedors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Usuario,Nombres,Apellidos,Contraseña,Estado")] Vendedor vendedor)
        {
            if (id != vendedor.Usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedorExists(vendedor.Usuario))
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
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", vendedor.Estado);
            return View(vendedor);
        }

        // GET: Vendedors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Vendedors == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedors
                .Include(v => v.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.Usuario == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // POST: Vendedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Vendedors == null)
            {
                return Problem("Entity set 'AgenciaVContext.Vendedors'  is null.");
            }
            var vendedor = await _context.Vendedors.FindAsync(id);
            if (vendedor != null)
            {
                vendedor.Estado = "DESHABILITADO"; // Cambiar el estado en lugar de eliminar
                _context.Update(vendedor); // Actualizar el estado en la base de datos
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool VendedorExists(string id)
        {
          return (_context.Vendedors?.Any(e => e.Usuario == id)).GetValueOrDefault();
        }
    }
}
