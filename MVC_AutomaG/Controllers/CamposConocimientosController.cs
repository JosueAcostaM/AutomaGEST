using API_Consumer;
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
    }
}
