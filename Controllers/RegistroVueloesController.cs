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
    public class RegistroVueloesController : Controller
    {
        private readonly AgenciaVContext _context;

        public RegistroVueloesController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: RegistroVueloes
        public async Task<IActionResult> Index()
        {

            var modeloVuelos = _context.Vuelos
               .Include(v => v.IdAerolineaNavigation) // Incluir datos de la tabla de aerolíneas
               .Include(v => v.IdAeropuertoDestinoNavigation)
               .Include(v => v.IdAeropuertoOrigenNavigation)
               .Where(m => m.Estado == "HABILITADO" && m.FechaInicio > DateTime.Today)
               .ToList();
            ViewData["IdVuelo"] = new SelectList(modeloVuelos, "IdVuelo", "IdVuelo");

            ViewBag.VuelosDisponibles = modeloVuelos; // Pasar la lista de vuelos disponibles a través de ViewBag

            var agenciaVContext = _context.RegistroVuelos.Include(r => r.DniNavigation).Include(r => r.EstadoNavigation).Include(r => r.IdAsientoNavigation).Include(r => r.IdVueloNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: RegistroVueloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RegistroVuelos == null)
            {
                return NotFound();
            }

            var registroVuelo = await _context.RegistroVuelos
                .Include(r => r.DniNavigation)
                .Include(r => r.EstadoNavigation)
                .Include(r => r.IdAsientoNavigation)
                .Include(r => r.IdVueloNavigation)
                .FirstOrDefaultAsync(m => m.IdRvuelo == id);
            if (registroVuelo == null)
            {
                return NotFound();
            }

            return View(registroVuelo);
        }

        // GET: RegistroVueloes/Create
        public IActionResult Create()
        {

            var modeloUsuarios = _context.Usuarios.Where(m => m.Estado == "HABILITADO").ToList();
            var modeloVuelos = _context.Vuelos
                .Include(v => v.IdAerolineaNavigation) // Incluir datos de la tabla de aerolíneas
                .Include(v => v.IdAeropuertoDestinoNavigation) 
                .Include(v => v.IdAeropuertoOrigenNavigation) 
                .Where(m => m.Estado == "HABILITADO" && m.FechaInicio > DateTime.Today)
                .ToList();
            var ASIENTOS = _context.Asientos.Where(m => m.Disponibilidad == "DISPONIBLE").ToList();


            ViewData["Dni"] = new SelectList(modeloUsuarios, "Dni", "Dni");
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            ViewData["IdAsiento"] = new SelectList(ASIENTOS, "IdAsiento", "IdAsiento");
            ViewData["IdVuelo"] = new SelectList(modeloVuelos, "IdVuelo", "IdVuelo");

            ViewBag.VuelosDisponibles = modeloVuelos; // Pasar la lista de vuelos disponibles a través de ViewBag

            return View();
        }

        // POST: RegistroVueloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRvuelo,PrecioT,IdVuelo,IdAsiento,Dni,Estado")] RegistroVuelo registroVuelo)
        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrWhiteSpace(registroVuelo.Estado))
                {
                    registroVuelo.Estado = "HABILITADO"; // Establece el valor por defecto si no se especifica
                }
                var asiento = _context.Asientos.FirstOrDefault(a => a.IdAsiento == registroVuelo.IdAsiento);
                if (asiento != null)
                {
                    asiento.Disponibilidad = "OCUPADO";
                    _context.Update(asiento);
                    await _context.SaveChangesAsync();
                }

                _context.Add(registroVuelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var ASIENTOS = _context.Asientos.Where(m => m.Disponibilidad == "DISPONIBLE").ToList();

            ViewData["Dni"] = new SelectList(_context.Usuarios, "Dni", "Dni", registroVuelo.Dni);
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", registroVuelo.Estado);
            ViewData["IdAsiento"] = new SelectList(ASIENTOS, "IdAsiento", "IdAsiento", registroVuelo.IdAsiento);
            ViewData["IdVuelo"] = new SelectList(_context.Vuelos, "IdVuelo", "IdVuelo", registroVuelo.IdVuelo);
            return View(registroVuelo);
        }

        // GET: RegistroVueloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RegistroVuelos == null)
            {
                return NotFound();
            }

            var registroVuelo = await _context.RegistroVuelos.FindAsync(id);
            if (registroVuelo == null)
            {
                return NotFound();
            }

            var modeloUsuarios = _context.Usuarios.Where(m => m.Estado == "HABILITADO").ToList();
            var modeloVuelos = _context.Vuelos
                .Where(m => m.Estado == "HABILITADO" && m.FechaInicio > DateTime.Today)
                .ToList();
            var ASIENTOS = _context.Asientos.Where(m => m.Disponibilidad == "DISPONIBLE").ToList();


            ViewData["Dni"] = new SelectList(modeloUsuarios, "Dni", "Dni", registroVuelo.Dni);
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", registroVuelo.Estado);
            ViewData["IdAsiento"] = new SelectList(ASIENTOS, "IdAsiento", "IdAsiento", registroVuelo.IdAsiento);
            ViewData["IdVuelo"] = new SelectList(modeloVuelos, "IdVuelo", "IdVuelo", registroVuelo.IdVuelo);
            return View(registroVuelo);
        }

        // POST: RegistroVueloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRvuelo,PrecioT,IdVuelo,IdAsiento,Dni,Estado")] RegistroVuelo registroVuelo)
        {
            if (id != registroVuelo.IdRvuelo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var registroActual = await _context.RegistroVuelos.AsNoTracking().FirstOrDefaultAsync(r => r.IdRvuelo == id);
                    var asientoActual = await _context.Asientos.FirstOrDefaultAsync(a => a.IdAsiento == registroActual.IdAsiento);
                    var nuevoAsiento = await _context.Asientos.FirstOrDefaultAsync(a => a.IdAsiento == registroVuelo.IdAsiento);

                    if (asientoActual.IdAsiento != nuevoAsiento.IdAsiento)
                    {
                        asientoActual.Disponibilidad = "DISPONIBLE";
                        nuevoAsiento.Disponibilidad = "OCUPADO";

                        _context.Update(asientoActual);
                        _context.Update(nuevoAsiento);
                        await _context.SaveChangesAsync();
                    }


                    if (registroVuelo.Estado == "HABILITADO")
                    {
                        nuevoAsiento.Disponibilidad = "OCUPADO";
                    }
                    else if (registroVuelo.Estado == "DESHABILITADO")
                    {
                        nuevoAsiento.Disponibilidad = "DISPONIBLE";
                    }

                    _context.Update(registroVuelo);
                    _context.Update(nuevoAsiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroVueloExists(registroVuelo.IdRvuelo))
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
            ViewData["Dni"] = new SelectList(_context.Usuarios, "Dni", "Dni", registroVuelo.Dni);
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", registroVuelo.Estado);
            ViewData["IdAsiento"] = new SelectList(_context.Asientos, "IdAsiento", "IdAsiento", registroVuelo.IdAsiento);
            ViewData["IdVuelo"] = new SelectList(_context.Vuelos, "IdVuelo", "IdVuelo", registroVuelo.IdVuelo);
            return View(registroVuelo);
        }

        // GET: RegistroVueloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RegistroVuelos == null)
            {
                return NotFound();
            }

            var registroVuelo = await _context.RegistroVuelos
                .Include(r => r.DniNavigation)
                .Include(r => r.EstadoNavigation)
                .Include(r => r.IdAsientoNavigation)
                .Include(r => r.IdVueloNavigation)
                .FirstOrDefaultAsync(m => m.IdRvuelo == id);
            if (registroVuelo == null)
            {
                return NotFound();
            }

            return View(registroVuelo);
        }

        // POST: RegistroVueloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RegistroVuelos == null)
            {
                return Problem("Entity set 'AgenciaVContext.RegistroVuelos'  is null.");
            }
            var registroVuelo = await _context.RegistroVuelos.FindAsync(id);
            if (registroVuelo != null)
            {
                registroVuelo.Estado = "DESHABILITADO"; // Cambiar el estado en lugar de eliminar
                var asiento = await _context.Asientos.FirstOrDefaultAsync(a => a.IdAsiento == registroVuelo.IdAsiento);
                if (asiento != null)
                {
                    asiento.Disponibilidad = "DISPONIBLE"; // Cambiar el estado del asiento a DISPONIBLE
                    _context.Update(asiento); // Actualizar el estado del asiento en la base de datos
                }
                _context.Update(registroVuelo); // Actualizar el estado en la base de datos
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult GetPrecio(int asiento)
        {

            var precio = _context.Asientos
                .Where(a => a.IdAsiento == asiento)
                .Select(a => a.PrecioV)
                .FirstOrDefault();

            return Json(precio);
        }
        [HttpGet]
        public IActionResult GetPrecioVuelo(int id)
        {

            var precio = _context.Vuelos
                .Where(a => a.IdVuelo == id && a.Estado=="HABILITADO")
                .Select(a => a.PrecioV)
                .FirstOrDefault();

            return Json(precio);
        }
        [HttpGet]
        public IActionResult GetAsientos(string clase, int? idVuelo)
        {
            if (idVuelo.HasValue)
            {
                var asientos = _context.Asientos
                    .Where(a => a.Clase == clase && a.IdVuelo == idVuelo && (a.Disponibilidad == "DISPONIBLE" || a.Disponibilidad != "OCUPADO"))
                    .Select(a => new { value = a.IdAsiento, text = a.NumAsiento })
                    .ToList();

                return Json(asientos);
            }
            else
            {
                return BadRequest("Se requiere especificar un IdVuelo válido.");
            }
        }


        private bool RegistroVueloExists(int id)
        {
          return (_context.RegistroVuelos?.Any(e => e.IdRvuelo == id)).GetValueOrDefault();
        }
    }
}
