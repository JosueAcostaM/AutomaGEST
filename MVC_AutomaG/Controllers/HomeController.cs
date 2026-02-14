using API_Consumer;
using Microsoft.AspNetCore.Mvc;
using Modelos_AutomaG;
using MVC_AutomaG.Models;
using System.Diagnostics;

namespace MVC_AutomaG.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // 🔹 Traer datos desde la API
            var aspirantes = CRUD<Aspirantes>.GetAll();
            var programas = CRUD<Programas>.GetAll();

            // 🔹 Calcular métricas
            var totalAspirantes = aspirantes.Count;

            var enProceso = aspirantes
                .Where(a => a.estadoasp.Equals("En revisión", StringComparison.OrdinalIgnoreCase)
                     || a.estadoasp.Equals("Revisado", StringComparison.OrdinalIgnoreCase))

                .Count();

            var nuevos = aspirantes
                .Where(a => a.fecharegistro >= DateTime.Now.AddDays(-7))
                .Count();

            var maestriasActivas = programas
                .Where(p => p.estadopro.Equals("activo", StringComparison.OrdinalIgnoreCase))
                .Count();

            // 🔹 Últimos 5 registros recientes
            var actividadReciente = aspirantes
                .OrderByDescending(a => a.fecharegistro)
                .Take(5)
                .ToList();

            ViewBag.ActividadReciente = actividadReciente;


            // 🔹 Enviar a la vista
            ViewBag.TotalAspirantes = totalAspirantes;
            ViewBag.EnProceso = enProceso;
            ViewBag.Nuevos = nuevos;
            ViewBag.MaestriasActivas = maestriasActivas;
            ViewBag.ActividadReciente = actividadReciente;

            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
