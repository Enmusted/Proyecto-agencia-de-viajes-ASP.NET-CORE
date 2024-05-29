using AgenciaViajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AgenciaViajes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AgenciaVContext _context;

        public HomeController(ILogger<HomeController> logger, AgenciaVContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult VendedorLogin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UsuarioLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string contraseña)
        {
            var user = _context.Credenciales.FirstOrDefault(u => u.Usuario == usuario && u.Contraseña == contraseña && u.Rol == "adminitrador");

            if (user != null)
            {
                // Usuario autenticado
                return RedirectToAction("Index");
            }
            else
            {
                // Credenciales incorrectas, establece un mensaje de error y redirige a la vista Login
                TempData["ErrorMensaje"] = "Usuario o contraseña incorrecta.";
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult VendedorLogin(string usuario, string contraseña)
        {
            var vendedor = _context.Vendedors.FirstOrDefault(v => v.Usuario == usuario && v.Contraseña == contraseña);

            if (vendedor != null)
            {
                TempData["UserRole"] = "Vendedor";
                return RedirectToAction("Indexvendedor");
            }
            else
            {
                TempData["ErrorMensaje"] = "Usuario o contraseña incorrecta.";
                return View("VendedorLogin");
            }
        }

        public IActionResult Indexvendedor()
        {
            var userRole = TempData["UserRole"] as string;

            if (userRole == "Vendedor")
            {
                return View("Indexvendedor", "_Layoutvendedor");
            }
            else
            {
                return RedirectToAction("Welcome");
            }
        }

        [HttpPost]
        public IActionResult UsuarioLogin(string usuario, string contraseña)
        {
            // Verifica las credenciales del usuario en tu base de datos aquí
            var user = _context.Usuarios.FirstOrDefault(u => u.Dni == usuario && u.Contraseña == contraseña);

            if (user != null)
            {
                ViewData["UserRole"] = "Usuario";
                return RedirectToAction("Index");
            }
            else
            {
                // Credenciales incorrectas, establece un mensaje de error y redirige a la vista UsuarioLogin
                TempData["ErrorMensaje"] = "Usuario o contraseña incorrecta.";
                return RedirectToAction("UsuarioLogin");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
