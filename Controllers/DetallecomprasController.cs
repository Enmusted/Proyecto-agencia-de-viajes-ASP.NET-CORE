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
    public class DetallecomprasController : Controller
    {
        private readonly AgenciaVContext _context;

        public DetallecomprasController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: Detallecompras
        public async Task<IActionResult> Index()
        {
            var agenciaVContext = _context.Detallecompras.Include(d => d.DniNavigation).Include(d => d.IdPrhNavigation).Include(d => d.IdProductoNavigation).Include(d => d.IdRvueloNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: Detallecompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Detallecompras == null)
            {
                return NotFound();
            }

            var detallecompra = await _context.Detallecompras
                .Include(d => d.DniNavigation)
                .Include(d => d.IdPrhNavigation)
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdRvueloNavigation)
                .FirstOrDefaultAsync(m => m.NumeroVenta == id);
            if (detallecompra == null)
            {
                return NotFound();
            }

            return View(detallecompra);
        }

        // GET: Detallecompras/Create
        public IActionResult Create()
        {


            var modeloUsuarios = _context.Usuarios.Where(m => m.Estado == "HABILITADO").ToList();
            var modeloHotels = _context.ReservaHotels.Where(m => m.Estado == "HABILITADO").ToList();
            var modeloTours = _context.Tours.Where(m => m.Estado == "HABILITADO" && m.Capacidad > 0).ToList();
            var modeloRegistroVuelos = _context.RegistroVuelos.Where(m => m.Estado == "HABILITADO").ToList();


            ViewData["Dni"] = new SelectList(modeloUsuarios, "Dni", "Dni");
            ViewData["IdPrh"] = new SelectList(modeloHotels, "IdPrh", "IdPrh");
            ViewData["IdProducto"] = new SelectList(modeloTours, "IdProducto", "IdProducto");
            ViewData["IdRvuelo"] = new SelectList(modeloRegistroVuelos, "IdRvuelo", "IdRvuelo");

            ViewBag.TourDisponibles = modeloTours; // Pasar la lista de vuelos disponibles a través de ViewBag
            ViewBag.VueloDisponibles = modeloRegistroVuelos; // Pasar la lista de vuelos disponibles a través de ViewBag

            return View();
        }

        // POST: Detallecompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroVenta,Dni,IdPrh,IdRvuelo,IdProducto,TarjetaCredito,TVencimiento,Cvv,FechaCompra,Total")] Detallecompra detallecompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallecompra);
                await _context.SaveChangesAsync();

                // Obtén el producto correspondiente
                var producto = await _context.Tours.FindAsync(detallecompra.IdProducto);

                

                if (producto != null)
                {
                    // Resta 1 del stock del producto
                    producto.Capacidad--;
                    // Actualiza los registros en las respectivas tablas
                    _context.Tours.Update(producto);
                    await _context.SaveChangesAsync();

                }

                if (detallecompra.FechaCompra == null || detallecompra.FechaCompra == DateTime.MinValue)
                {
                    detallecompra.FechaCompra = DateTime.Now; // Establece la fecha de hoy como valor por defecto si es nula o tiene el valor mínimo
                }

                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));

            }
            ViewData["Dni"] = new SelectList(_context.Usuarios, "Dni", "Dni", detallecompra.Dni);
            ViewData["IdPrh"] = new SelectList(_context.ReservaHotels, "IdPrh", "IdPrh", detallecompra.IdPrh);
            ViewData["IdProducto"] = new SelectList(_context.Tours, "IdProducto", "IdProducto", detallecompra.IdProducto);
            ViewData["IdRvuelo"] = new SelectList(_context.RegistroVuelos, "IdRvuelo", "IdRvuelo", detallecompra.IdRvuelo);
            return View(detallecompra);
        }

        // GET: Detallecompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Detallecompras == null)
            {
                return NotFound();
            }

            var detallecompra = await _context.Detallecompras.FindAsync(id);
            if (detallecompra == null)
            {
                return NotFound();
            }

            var modeloUsuarios = _context.Usuarios.Where(m => m.Estado == "HABILITADO").ToList();
            var modeloHotels = _context.ReservaHotels.Where(m => m.Estado == "HABILITADO").ToList();
            var modeloTours = _context.Tours.Where(m => m.Estado == "HABILITADO" && m.Capacidad >0).ToList();
            var modeloRegistroVuelos = _context.RegistroVuelos.Where(m => m.Estado == "HABILITADO").ToList();


            ViewData["Dni"] = new SelectList(modeloUsuarios, "Dni", "Dni", detallecompra.Dni);
            ViewData["IdPrh"] = new SelectList(modeloHotels, "IdPrh", "IdPrh", detallecompra.IdPrh);
            ViewData["IdProducto"] = new SelectList(modeloTours, "IdProducto", "IdProducto", detallecompra.IdProducto);
            ViewData["IdRvuelo"] = new SelectList(modeloRegistroVuelos, "IdRvuelo", "IdRvuelo", detallecompra.IdRvuelo);
            return View(detallecompra);
        }

        // POST: Detallecompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumeroVenta,Dni,IdPrh,IdRvuelo,IdProducto,TarjetaCredito,TVencimiento,Cvv,FechaCompra,Total")] Detallecompra detallecompra)
        {
            if (id != detallecompra.NumeroVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallecompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallecompraExists(detallecompra.NumeroVenta))
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
            ViewData["Dni"] = new SelectList(_context.Usuarios, "Dni", "Dni", detallecompra.Dni);
            ViewData["IdPrh"] = new SelectList(_context.ReservaHotels, "IdPrh", "IdPrh", detallecompra.IdPrh);
            ViewData["IdProducto"] = new SelectList(_context.Tours, "IdProducto", "IdProducto", detallecompra.IdProducto);
            ViewData["IdRvuelo"] = new SelectList(_context.RegistroVuelos, "IdRvuelo", "IdRvuelo", detallecompra.IdRvuelo);
            return View(detallecompra);
        }

        // GET: Detallecompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Detallecompras == null)
            {
                return NotFound();
            }

            var detallecompra = await _context.Detallecompras
                .Include(d => d.DniNavigation)
                .Include(d => d.IdPrhNavigation)
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdRvueloNavigation)
                .FirstOrDefaultAsync(m => m.NumeroVenta == id);
            if (detallecompra == null)
            {
                return NotFound();
            }

            return View(detallecompra);
        }

        // POST: Detallecompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Detallecompras == null)
            {
                return Problem("Entity set 'AgenciaVContext.Detallecompras'  is null.");
            }
            var detallecompra = await _context.Detallecompras.FindAsync(id);
            if (detallecompra != null)
            {
                _context.Detallecompras.Remove(detallecompra);

                // Guarda el ID del producto antes de eliminar la compra
                var productId = detallecompra.IdProducto;

                await _context.SaveChangesAsync();

                // Recupera el producto asociado a la compra
                var product = await _context.Tours.FindAsync(productId);

                if (product != null)
                {
                    // Incrementa la capacidad en 1 al eliminar una compra
                    product.Capacidad++;
                    _context.Tours.Update(product);
                    await _context.SaveChangesAsync();
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult GetPrecioRh(int id)
        {

            var precio = _context.ReservaHotels
                .Where(a => a.IdPrh == id)
                .Select(a => a.Total)
                .FirstOrDefault();

            return Json(precio);
        }

        public IActionResult GetPrecioAsiento(int id)
        {

            var precio = _context.RegistroVuelos
                .Where(a => a.IdRvuelo == id)
                .Select(a => a.PrecioT)
                .FirstOrDefault();

            return Json(precio);
        }

        public IActionResult GetPrecioTour(int id)
        {

            var precio = _context.Tours
                .Where(a => a.IdProducto == id)
                .Select(a => a.Precio)
                .FirstOrDefault();

            return Json(precio);
        }

        public IActionResult GetIdPrH(string dni)
        {

            var asientos = _context.ReservaHotels
                .Where(a => a.Dni == dni)
                .Select(a => new { value = a.IdPrh, text = a.IdPrh })
                .ToList();

            return Json(asientos);
        }
        public IActionResult GetIdRvuelo(string dni)
        {
            var idsComprados = _context.Detallecompras
                                    .Where(dc => dc.Dni == dni)
                                    .Select(dc => dc.IdRvuelo)
                                    .ToList();

            var asientosDisponibles = _context.RegistroVuelos
                                        .Where(a => a.Dni == dni && !idsComprados.Contains(a.IdRvuelo))
                                        .Select(a => new { value = a.IdRvuelo, text = a.IdRvuelo })
                                        .ToList();

            return Json(asientosDisponibles);
        }

        public JsonResult GetRegistroVuelo(string dni)
        {
            var idsComprados = _context.Detallecompras
                        .Where(dc => dc.Dni == dni)
                        .Select(dc => dc.IdRvuelo)
                        .ToList();
            // Suponiendo que DbContext sea tu contexto de base de datos
            var registroVueloData = _context.RegistroVuelos
       .Where(a => a.Dni == dni && !idsComprados.Contains(a.IdRvuelo))
       .Select(a => new {
           IdRvuelo = a.IdRvuelo,
           IdVuelo1 = a.IdVueloNavigation.IdAeropuertoDestinoNavigation.Pais,
           IdVuelo2 = a.IdVueloNavigation.IdAeropuertoDestinoNavigation.Provincia,
           IdVuelo = a.IdVueloNavigation.IdAeropuertoDestinoNavigation.Aeropuerto,
           IdAsiento = a.IdAsientoNavigation.NumAsiento,
           Dni = a.Dni
       })
       .ToList();

            return Json(registroVueloData);

        }



        private bool DetallecompraExists(int id)
        {
            return (_context.Detallecompras?.Any(e => e.NumeroVenta == id)).GetValueOrDefault();
        }

    }

}
