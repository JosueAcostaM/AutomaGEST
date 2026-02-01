using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Consumer;
using Modelos_AutomaG;

namespace MVC_AutomaG.Controllers
{
    public class AspirantesController : Controller
    {
        // GET: AspirantesController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult ListAspirantes()
        {
            try
            {
                var aspirantes = CRUD<Aspirantes>.GetAll();
                return View(aspirantes);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View(new List<Aspirantes>());
            }
        }

        public IActionResult DetailsAspirantes(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
 
                var aspirante = CRUD<Aspirantes>.GetById(id);

                if (aspirante == null)
                {
                    return NotFound();
                }

                return View(aspirante);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al obtener detalles: " + ex.Message;
                return RedirectToAction(nameof(ListAspirantes));
            }
        }
    }
}
