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
    public class ReservaHotelsController : Controller
    {
        private readonly AgenciaVContext _context;

        public ReservaHotelsController(AgenciaVContext context)
        {
            _context = context;
        }

        // GET: ReservaHotels
        public async Task<IActionResult> Index()
        {
            var agenciaVContext = _context.ReservaHotels.Include(r => r.DniNavigation).Include(r => r.EstadoNavigation).Include(r => r.IdHotelNavigation).Include(r => r.IdProductoThNavigation);
            return View(await agenciaVContext.ToListAsync());
        }

        // GET: ReservaHotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReservaHotels == null)
            {
                return NotFound();
            }

            var reservaHotel = await _context.ReservaHotels
                .Include(r => r.DniNavigation)
                .Include(r => r.EstadoNavigation)
                .Include(r => r.IdHotelNavigation)
                .Include(r => r.IdProductoThNavigation)
                .FirstOrDefaultAsync(m => m.IdPrh == id);
            if (reservaHotel == null)
            {
                return NotFound();
            }

            return View(reservaHotel);
        }

        // GET: ReservaHotels/Create
        public IActionResult Create()
        {

            var modeloUsuarios = _context.Usuarios.Where(m => m.Estado == "HABILITADO").ToList();
            var modeloHotels = _context.Hotels.Where(m => m.Estado == "HABILITADO").ToList();
            var thaabitacion = _context.Thabitacions.Where(m => m.Disponibilidad == "DISPONIBLE").ToList();

            ViewData["Dni"] = new SelectList(modeloUsuarios, "Dni", "Dni");
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1");
            ViewData["IdHotel"] = new SelectList(modeloHotels, "IdHotel", "Nombre");
            ViewData["IdProductoTh"] = new SelectList(thaabitacion, "IdProductoTh", "IdProductoTh");
            return View();
        }

        // POST: ReservaHotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrh,Dni,IdHotel,IdProductoTh,FechaEntrada,FechaSalida,Total,Estado")] ReservaHotel reservaHotel)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(reservaHotel.Estado))
                {
                    reservaHotel.Estado = "HABILITADO"; // Establece el valor por defecto si no se especifica
                }
                // Cambiar el estado de la habitación a "OCUPADO"
                var habitacion = _context.Thabitacions.FirstOrDefault(th => th.IdProductoTh == reservaHotel.IdProductoTh);
                if (habitacion != null)
                {
                    habitacion.Disponibilidad = "OCUPADO";
                    _context.Update(habitacion);
                    await _context.SaveChangesAsync();
                }

                _context.Add(reservaHotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Dni"] = new SelectList(_context.Usuarios, "Dni", "Dni", reservaHotel.Dni);
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", reservaHotel.Estado);
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "Nombre", reservaHotel.IdHotel);
            ViewData["IdProductoTh"] = new SelectList(_context.Thabitacions, "IdProductoTh", "IdProductoTh", reservaHotel.IdProductoTh);
            return View(reservaHotel);
        }

        // GET: ReservaHotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReservaHotels == null)
            {
                return NotFound();
            }

            var reservaHotel = await _context.ReservaHotels.FindAsync(id);
            if (reservaHotel == null)
            {
                return NotFound();
            }
            ViewData["Dni"] = new SelectList(_context.Usuarios, "Dni", "Dni", reservaHotel.Dni);
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", reservaHotel.Estado);
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "Nombre", reservaHotel.IdHotel);
            ViewData["IdProductoTh"] = new SelectList(_context.Thabitacions, "IdProductoTh", "IdProductoTh", reservaHotel.IdProductoTh);
            return View(reservaHotel);
        }

        // POST: ReservaHotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrh,Dni,IdHotel,IdProductoTh,FechaEntrada,FechaSalida,Total,Estado")] ReservaHotel reservaHotel)
        {
            if (id != reservaHotel.IdPrh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservaHotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaHotelExists(reservaHotel.IdPrh))
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
            ViewData["Dni"] = new SelectList(_context.Usuarios, "Dni", "Dni", reservaHotel.Dni);
            ViewData["Estado"] = new SelectList(_context.Estados, "Estado1", "Estado1", reservaHotel.Estado);
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "IdHotel", reservaHotel.IdHotel);
            ViewData["IdProductoTh"] = new SelectList(_context.Thabitacions, "IdProductoTh", "IdProductoTh", reservaHotel.IdProductoTh);
            return View(reservaHotel);
        }

        // GET: ReservaHotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReservaHotels == null)
            {
                return NotFound();
            }

            var reservaHotel = await _context.ReservaHotels
                .Include(r => r.DniNavigation)
                .Include(r => r.EstadoNavigation)
                .Include(r => r.IdHotelNavigation)
                .Include(r => r.IdProductoThNavigation)
                .FirstOrDefaultAsync(m => m.IdPrh == id);
            if (reservaHotel == null)
            {
                return NotFound();
            }

            return View(reservaHotel);
        }

        // POST: ReservaHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReservaHotels == null)
            {
                return Problem("Entity set 'AgenciaVContext.ReservaHotels'  is null.");
            }
            var reservaHotel = await _context.ReservaHotels.FindAsync(id);
            if (reservaHotel != null)
            {
                reservaHotel.Estado = "DESHABILITADO"; // Cambiar el estado en lugar de eliminar
                _context.Update(reservaHotel); // Actualizar el estado en la base de datos
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult GetProductoThByHotel(int idHotel)
        {
            var productoThList = _context.Thabitacions
                .Where(th => th.IdHotel == idHotel && th.Disponibilidad=="DISPONIBLE")
                .Select(th => new { value = th.IdProductoTh, text = th.Habitacion })
                .ToList();

            return Json(productoThList);
        }

        [HttpGet]
        public IActionResult GetPrecioByIdProductoTh(int idProductoTh)
        {
            var precio = _context.Thabitacions
                .Where(th => th.IdProductoTh == idProductoTh)
                .Select(th => th.Precio)
                .FirstOrDefault();

            return Json(precio);
        }




        private bool ReservaHotelExists(int id)
        {
            return (_context.ReservaHotels?.Any(e => e.IdPrh == id)).GetValueOrDefault();
        }
    }



}
