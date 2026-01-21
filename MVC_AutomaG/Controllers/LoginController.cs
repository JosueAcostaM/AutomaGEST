using API_Consumer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos_AutomaG;
using Servicios_AutomaG.Interfaces;

namespace MVC_AutomaG.Controllers
{
    public class LoginController : Controller
    {

        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }


        // GET: LoginController
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (await _authService.Login(email, password))
            {
                
                // Redirigir a la página principal o dashboard
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Mostrar mensaje de error
                ViewBag.ErrorMessage = "Email o contraseña incorrectos.";
                return View("Index");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string nombre,string apellido,string password)
        {
            if (await _authService.Register(email, nombre, apellido,password))
            {
                // Redirigir a la página de inicio de sesión
                return RedirectToAction("Index", "Login");
            }
            else
            {
                // Mostrar mensaje de error
                ViewBag.ErrorMessage = "Error al registrar el usuario.";
                return View();
            }
        }
        [HttpGet]
        public IActionResult RecuperarPassword()
        {
            return View();
        }


        /*[HttpPost]
        public async Task<IActionResult> RecuperarPassword(string email)
        {
            var usuario = CRUD<Usuario>.GetAll().FirstOrDefault(u => u.Email == email);
            if (usuario == null)
            {
                ViewBag.ErrorMessage = "El correo electrónico no está registrado.";
                return View("Index");
            }
            // Enviar correo electrónico de recuperación de contraseña
            await _emailService.enviarEmailRecuperacionPassword(email);
            ViewBag.SuccessMessage = "Se ha enviado un correo electrónico para recuperar la contraseña.";
            return RedirectToAction("Index", "Login");
        }*/

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Login");
        }
    }
}
