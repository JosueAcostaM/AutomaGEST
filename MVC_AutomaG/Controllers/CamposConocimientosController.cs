using API_Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos_AutomaG;

namespace MVC_AutomaG.Controllers
{
    public class CamposConocimientosController : Controller
    {
        // LISTADO
        public IActionResult ListCamposConocimientos()
        {
            try
            {
                var campos = CRUD<CamposConocimiento>.GetAll();
                return View(campos);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View(new List<CamposConocimiento>());
            }
        }

        // DETAILS
        public IActionResult DetailsCamposConocimientos(string id)
        {
            try
            {
                var campo = CRUD<CamposConocimiento>.GetById(id);

                if (campo == null)
                    return NotFound();

                return View(campo);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View();
            }
        }
        // GET: Niveles/Create
        public IActionResult Create()
        {
            return View(new CamposConocimiento());
        }

        // POST: CamposConocimientos/Create
        [Authorize(Roles = "Super Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CamposConocimiento campos)
        {
            try
            {
                // Si tu API genera idniv, mandamos TEMP
                if (string.IsNullOrWhiteSpace(campos.idcam))
                    campos.idcam = "CAM";

                campos.codigocam= campos.idcam.ToUpper();
                var creado = CRUD<CamposConocimiento>.Create(campos);

                if (creado == null || string.IsNullOrEmpty(creado.idcam))
                {
                    ViewBag.Error = "La API rechazó la creación del campo. Verifica los datos.";
                    return View(campos);
                }

                return RedirectToAction(nameof(ListCamposConocimientos));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View(campos);
            }
        }
    }
}
