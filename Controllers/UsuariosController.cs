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
    public class UsuariosController : Controller
    {
        private readonly AgenciaVContext _context;

        public UsuariosController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var agenciaVContext = _context.Usuarios.Include(u => u.EstadoNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.Dni == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Dni,Correo,Contraseña,Nombres,Apellidos,FechaNacimiento,Numero,Estado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(usuario.Estado))
                {
                    usuario.Estado = "HABILITADO"; // Establece el valor por defecto si no se especifica
                }

                try
                {
                    _context.Add(usuario);
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
                        return View(usuario);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar los datos.");
                        // En caso de otros errores, agrega un error de modelo genérico
                    }
                }
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", usuario.Estado);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", usuario.Estado);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Dni,Correo,Contraseña,Nombres,Apellidos,FechaNacimiento,Numero,Estado")] Usuario usuario)
        {
            if (id != usuario.Dni)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Dni))
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
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", usuario.Estado);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.Dni == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'AgenciaVContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.Estado = "DESHABILITADO"; // Cambiar el estado en lugar de eliminar
                _context.Update(usuario); // Actualizar el estado en la base de datos
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(string id)
        {
          return (_context.Usuarios?.Any(e => e.Dni == id)).GetValueOrDefault();
        }
    }
}
