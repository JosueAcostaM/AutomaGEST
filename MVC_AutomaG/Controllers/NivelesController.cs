using API_Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos_AutomaG;

namespace MVC_AutomaG.Controllers
{
    public class NivelesController : Controller
    {
        // GET: Niveles/ListNiveles
        public IActionResult ListNiveles()
        {
            try
            {
                var niveles = CRUD<Niveles>.GetAll();
                return View(niveles);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View(new List<Niveles>());
            }
        }

        // GET: Niveles/DetailsNiveles/NIV-01
        public IActionResult DetailsNiveles(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            try
            {
                var nivel = CRUD<Niveles>.GetById(id);
                if (nivel == null) return NotFound();

                return View(nivel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View(new Niveles());
            }
        }

        // GET: Niveles/Create
        public IActionResult Create()
        {
            return View(new Niveles());
        }

        // POST: Niveles/Create
        [Authorize(Roles = "Super Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Niveles nivel)
        {
            try
            {
                // Limpieza navegación (por si acaso)
                nivel.Programas = null;

                // Si tu API genera idniv, mandamos TEMP
                if (string.IsNullOrWhiteSpace(nivel.idniv))
                    nivel.idniv = "TEMP";

                nivel.codigoniv = nivel.nombreniv.ToUpper();
                var creado = CRUD<Niveles>.Create(nivel);

                if (creado == null || string.IsNullOrEmpty(creado.idniv))
                {
                    ViewBag.Error = "La API rechazó la creación del NIVEL. Verifica los datos.";
                    return View(nivel);
                }

                return RedirectToAction(nameof(ListNiveles));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View(nivel);
            }
        }
    }
}
