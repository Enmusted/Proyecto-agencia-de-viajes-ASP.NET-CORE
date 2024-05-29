
//
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
    public class VueloesController : Controller
    {
        private readonly AgenciaVContext _context;

        public VueloesController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: Vueloes
        public async Task<IActionResult> Index()
        {
            var agenciaVContext = _context.Vuelos.Include(v => v.EstadoNavigation).Include(v => v.IdAerolineaNavigation).Include(v => v.IdAeropuertoDestinoNavigation).Include(v => v.IdAeropuertoOrigenNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: Vueloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelos
                .Include(v => v.EstadoNavigation)
                .Include(v => v.IdAerolineaNavigation)
                .Include(v => v.IdAeropuertoDestinoNavigation)
                .Include(v => v.IdAeropuertoOrigenNavigation)
                .FirstOrDefaultAsync(m => m.IdVuelo == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        // GET: Vueloes/Create
        public IActionResult Create()
        {
            // Filtrar los elementos cuyo estado es "HABILITADO"
            var modeloAerolineas = _context.Aerolineas.Where(m => m.Estado == "HABILITADO").ToList();
            var modeloAeropuerts = _context.Aeropuerts.Where(m => m.Estado == "HABILITADO").ToList();
            var paisesfiltro = _context.Aeropuerts.Where(m => m.Estado == "HABILITADO").Select(a => a.Pais).Distinct().ToList();


            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            ViewData["IdAerolinea"] = new SelectList(modeloAerolineas, "IdAerolinea", "Aerolinea1");
            ViewData["PaisOrigen"] = new SelectList(paisesfiltro);
            ViewData["ProvinciaOrigen"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            ViewData["PaisDestino"] = new SelectList(paisesfiltro);
            ViewData["ProvinciaDestino"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            ViewData["IdAeropuertoDestino"] = new SelectList(modeloAeropuerts, "IdAeropuerto", "Aeropuerto");
            ViewData["IdAeropuertoOrigen"] = new SelectList(modeloAeropuerts, "IdAeropuerto", "Aeropuerto");
            return View();
        }

        // POST: Vueloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVuelo,IdAerolinea,FechaInicio,FechaFinal,IdAeropuertoOrigen,IdAeropuertoDestino,PrecioV,Estado")] Vuelo vuelo)
        {

            if (ModelState.IsValid)
            {

                if (string.IsNullOrWhiteSpace(vuelo.Estado))
                {
                    vuelo.Estado = "HABILITADO"; // Establece el valor por defecto si no se especifica
                }


                _context.Add(vuelo);
                await _context.SaveChangesAsync();

                CrearAsientoA(vuelo);
                CrearAsientoB(vuelo);
                CrearAsientoC(vuelo);
                CrearAsientoD(vuelo);
                CrearAsientoE(vuelo);
                CrearAsientoF(vuelo);

                return RedirectToAction(nameof(Index));
            }
            var paisesfiltro = _context.Aeropuerts.Where(m => m.Estado == "HABILITADO").Select(a => a.Pais).Distinct().ToList();

            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            ViewData["IdAerolinea"] = new SelectList(_context.Aerolineas, "IdAerolinea", "Aerolinea1");
            ViewData["PaisOrigen"] = new SelectList(paisesfiltro);
            ViewData["ProvinciaOrigen"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            ViewData["PaisDestino"] = new SelectList(paisesfiltro);
            ViewData["ProvinciaDestino"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            ViewData["IdAeropuertoDestino"] = new SelectList(_context.Aeropuerts, "IdAeropuerto", "Aeropuerto", vuelo.IdAeropuertoDestino);
            ViewData["IdAeropuertoOrigen"] = new SelectList(_context.Aeropuerts, "IdAeropuerto", "Aeropuerto", vuelo.IdAeropuertoOrigen);
            return View(vuelo);
        }


        //ASIENTOS CREACIÓN
        private void CrearAsientoA(Vuelo vuelo)
        {
            // Obtener el ID del vuelo recién creado
            var nuevoVueloId = vuelo.IdVuelo;

            for (int numeroAsiento = 1; numeroAsiento <= 29; numeroAsiento++)
            {

                if (numeroAsiento == 4)
                {
                    // Saltar el número 4
                    continue;
                };
                if (numeroAsiento >= 11 && numeroAsiento <= 17)
                {
                    // Saltar el rango del 11 al 17
                    continue;
                }


                var numeroAsientoFormateadoA = "A" + numeroAsiento.ToString("D2");

                var asientoPrimeraClaseA = new Asiento
                {
                    NumAsiento = numeroAsientoFormateadoA,
                    Clase = ObtenerClaseDelAsiento(numeroAsiento),
                    Disponibilidad = "DISPONIBLE",
                    PrecioV = ObtenerPrecioDeLaClase(ObtenerClaseDelAsiento(numeroAsiento)),
                    IdVuelo = nuevoVueloId
                };


                _context.Asientos.Add(asientoPrimeraClaseA);
            }

            _context.SaveChanges();
        }

        //ASIENTO F
        private void CrearAsientoF(Vuelo vuelo)
        {
            // Obtener el ID del vuelo recién creado
            var nuevoVueloId = vuelo.IdVuelo;

            for (int numeroAsiento = 1; numeroAsiento <= 29; numeroAsiento++)
            {

                if (numeroAsiento == 4)
                {
                    // Saltar el número 4
                    continue;
                };
                if (numeroAsiento >= 11 && numeroAsiento <= 17)
                {
                    // Saltar el rango del 11 al 17
                    continue;
                }


                var numeroAsientoFormateadoA = "F" + numeroAsiento.ToString("D2");

                var asientoPrimeraClaseA = new Asiento
                {
                    NumAsiento = numeroAsientoFormateadoA,
                    Clase = ObtenerClaseDelAsiento(numeroAsiento),
                    Disponibilidad = "DISPONIBLE",
                    PrecioV = ObtenerPrecioDeLaClase(ObtenerClaseDelAsiento(numeroAsiento)),
                    IdVuelo = nuevoVueloId
                };


                _context.Asientos.Add(asientoPrimeraClaseA);
            }

            _context.SaveChanges();
        }


        //ASIENTO B

        private void CrearAsientoB(Vuelo vuelo)
        {
            // Obtener el ID del vuelo recién creado
            var nuevoVueloId = vuelo.IdVuelo;

            for (int numeroAsiento = 1; numeroAsiento <= 29; numeroAsiento++)
            {

                if (numeroAsiento == 4)
                {
                    // Saltar el número 4
                    continue;
                };
                if (numeroAsiento >= 11 && numeroAsiento <= 16)
                {
                    // Saltar el rango del 11 al 17
                    continue;
                }


                var numeroAsientoFormateadoA = "B" + numeroAsiento.ToString("D2");

                var asientoPrimeraClaseA = new Asiento
                {
                    NumAsiento = numeroAsientoFormateadoA,
                    Clase = ObtenerClaseDelAsiento(numeroAsiento),
                    Disponibilidad = "DISPONIBLE",
                    PrecioV = ObtenerPrecioDeLaClase(ObtenerClaseDelAsiento(numeroAsiento)),
                    IdVuelo = nuevoVueloId
                };


                _context.Asientos.Add(asientoPrimeraClaseA);
            }

            _context.SaveChanges();
        }


        //ASIENTO E
        private void CrearAsientoE(Vuelo vuelo)
        {
            // Obtener el ID del vuelo recién creado
            var nuevoVueloId = vuelo.IdVuelo;

            for (int numeroAsiento = 1; numeroAsiento <= 29; numeroAsiento++)
            {

                if (numeroAsiento == 4)
                {
                    // Saltar el número 4
                    continue;
                };
                if (numeroAsiento >= 11 && numeroAsiento <= 16)
                {
                    // Saltar el rango del 11 al 17
                    continue;
                }


                var numeroAsientoFormateadoA = "E" + numeroAsiento.ToString("D2");

                var asientoPrimeraClaseA = new Asiento
                {
                    NumAsiento = numeroAsientoFormateadoA,
                    Clase = ObtenerClaseDelAsiento(numeroAsiento),
                    Disponibilidad = "DISPONIBLE",
                    PrecioV = ObtenerPrecioDeLaClase(ObtenerClaseDelAsiento(numeroAsiento)),
                    IdVuelo = nuevoVueloId
                };


                _context.Asientos.Add(asientoPrimeraClaseA);
            }

            _context.SaveChanges();
        }

        //ASIENTO C
        private void CrearAsientoC(Vuelo vuelo)
        {
            // Obtener el ID del vuelo recién creado
            var nuevoVueloId = vuelo.IdVuelo;

            for (int numeroAsiento = 1; numeroAsiento <= 29; numeroAsiento++)
            {

                if (numeroAsiento <= 4)
                {
                    // Saltar el número 4
                    continue;
                };
                if (numeroAsiento >= 11 && numeroAsiento <= 16)
                {
                    // Saltar el rango del 11 al 17
                    continue;
                }


                var numeroAsientoFormateadoA = "C" + numeroAsiento.ToString("D2");

                var asientoPrimeraClaseA = new Asiento
                {
                    NumAsiento = numeroAsientoFormateadoA,
                    Clase = ObtenerClaseDelAsiento(numeroAsiento),
                    Disponibilidad = "DISPONIBLE",
                    PrecioV = ObtenerPrecioDeLaClase(ObtenerClaseDelAsiento(numeroAsiento)),
                    IdVuelo = nuevoVueloId
                };


                _context.Asientos.Add(asientoPrimeraClaseA);
            }

            _context.SaveChanges();
        }


        //ASIENTO C
        private void CrearAsientoD(Vuelo vuelo)
        {
            // Obtener el ID del vuelo recién creado
            var nuevoVueloId = vuelo.IdVuelo;

            for (int numeroAsiento = 1; numeroAsiento <= 29; numeroAsiento++)
            {

                if (numeroAsiento <= 4)
                {
                    // Saltar el número 4
                    continue;
                };
                if (numeroAsiento >= 11 && numeroAsiento <= 16)
                {
                    // Saltar el rango del 11 al 17
                    continue;
                }


                var numeroAsientoFormateadoA = "D" + numeroAsiento.ToString("D2");

                var asientoPrimeraClaseA = new Asiento
                {
                    NumAsiento = numeroAsientoFormateadoA,
                    Clase = ObtenerClaseDelAsiento(numeroAsiento),
                    Disponibilidad = "DISPONIBLE",
                    PrecioV = ObtenerPrecioDeLaClase(ObtenerClaseDelAsiento(numeroAsiento)),
                    IdVuelo = nuevoVueloId
                };


                _context.Asientos.Add(asientoPrimeraClaseA);
            }

            _context.SaveChanges();
        }


        //clases de asiento

        private string ObtenerClaseDelAsiento(int numeroAsiento)
        {
            if (numeroAsiento >= 1 && numeroAsiento <= 3)
            {
                return "Clase Ejecutiva";
            }
            else if (numeroAsiento == 5)
            {
                return "Premium";
            }
            else if (numeroAsiento >= 6 && numeroAsiento <= 10)
            {
                return "Favorable";
            }
            else if (numeroAsiento >= 17 && numeroAsiento <= 18)
            {
                return "Salida de Emergencia";
            }
            else if (numeroAsiento >= 19 && numeroAsiento <= 29)
            {
                return "Regular";
            }
            else
            {
                // Manejar otros casos si es necesario
                return "";
            }
        }

        private decimal ObtenerPrecioDeLaClase(string clase)
        {
            // Define los precios para cada clase según tus necesidades
            if (clase == "Clase Ejecutiva")
            {
                return 100;
            }
            else if (clase == "Premium")
            {
                return 70;
            }
            else if (clase == "Favorable")
            {
                return 30;
            }
            else if (clase == "Salida de Emergencia")
            {
                return 20;
            }
            else if (clase == "Regular")
            {
                return 15;
            }
            else
            {
                // Manejar otros casos si es necesario
                return 0;
            }
        }


        // GET: Vueloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelos.FindAsync(id);
            if (vuelo == null)
            {
                return NotFound();
            }
                        // Filtrar los elementos cuyo estado es "HABILITADO"
            var modeloAerolineas = _context.Aerolineas.Where(m => m.Estado == "HABILITADO").ToList();
            var modeloAeropuerts = _context.Aeropuerts.Where(m => m.Estado == "HABILITADO").ToList();

            var paisesfiltro = _context.Aeropuerts.Where(m => m.Estado == "HABILITADO").Select(a => a.Pais).Distinct().ToList();


            ViewData["PaisOrigen"] = new SelectList(paisesfiltro);
            ViewData["ProvinciaOrigen"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            ViewData["PaisDestino"] = new SelectList(paisesfiltro);
            ViewData["ProvinciaDestino"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", vuelo.Estado);
            ViewData["IdAerolinea"] = new SelectList(modeloAerolineas, "IdAerolinea", "Aerolinea1", vuelo.IdAerolinea);
            ViewData["IdAeropuertoDestino"] = new SelectList(modeloAeropuerts, "IdAeropuerto", "Aeropuerto", vuelo.IdAeropuertoDestino);
            ViewData["IdAeropuertoOrigen"] = new SelectList(modeloAeropuerts, "IdAeropuerto", "Aeropuerto", vuelo.IdAeropuertoOrigen);
            return View(vuelo);
        }

        // POST: Vueloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVuelo,IdAerolinea,FechaInicio,FechaFinal,IdAeropuertoOrigen,IdAeropuertoDestino,PrecioV,Estado")] Vuelo vuelo)
        {
            if (id != vuelo.IdVuelo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vuelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VueloExists(vuelo.IdVuelo))
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

            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            ViewData["IdAerolinea"] = new SelectList(_context.Aerolineas, "IdAerolinea", "Aerolinea1");
            ViewData["PaisOrigen"] = new SelectList(paisesfiltro);
            ViewData["ProvinciaOrigen"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            ViewData["PaisDestino"] = new SelectList(paisesfiltro);
            ViewData["ProvinciaDestino"] = new SelectList(_context.Aeropuerts, "Provincia", "Provincia");
            ViewData["IdAeropuertoDestino"] = new SelectList(_context.Aeropuerts, "IdAeropuerto", "Aeropuerto", vuelo.IdAeropuertoDestino);
            ViewData["IdAeropuertoOrigen"] = new SelectList(_context.Aeropuerts, "IdAeropuerto", "Aeropuerto", vuelo.IdAeropuertoOrigen);
            return View(vuelo);
        }

        // GET: Vueloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelos
                .Include(v => v.EstadoNavigation)
                .Include(v => v.IdAerolineaNavigation)
                .Include(v => v.IdAeropuertoDestinoNavigation)
                .Include(v => v.IdAeropuertoOrigenNavigation)
                .FirstOrDefaultAsync(m => m.IdVuelo == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        // POST: Vueloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vuelos == null)
            {
                return Problem("Entity set 'AgenciaVContext.Vuelos'  is null.");
            }
            var vuelo = await _context.Vuelos.FindAsync(id);
            if (vuelo != null)
            {
                vuelo.Estado = "DESHABILITADO"; // Cambiar el estado en lugar de eliminar
                _context.Update(vuelo); // Actualizar el estado en la base de datos
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

        [HttpGet]
        [HttpGet]
        public IActionResult GetAeropuertos(string provincia)
        {
            // Obtén los aeropuertos correspondientes al país y provincia desde tu base de datos
            var aeropuertos = _context.Aeropuerts
                .Where(a => a.Provincia == provincia && a.Estado=="HABILITADO")
                .Select(a => new { value = a.IdAeropuerto, text = a.Aeropuerto })
                .ToList();

            return Json(aeropuertos);
        }



        private bool VueloExists(int id)
        {
            return (_context.Vuelos?.Any(e => e.IdVuelo == id)).GetValueOrDefault();
        }
    }
}



//CREAR ASIENTOS A



