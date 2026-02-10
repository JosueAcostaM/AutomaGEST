using API_Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos_AutomaG;

namespace MVC_AutomaG.Controllers
{
    public class AccountsController : Controller
    {
        // GET: AccountsController
        public ActionResult Index()
        {
            return View();
        }


        public IActionResult ListUsuarios()
        {
            try
            {
                var usuarios= CRUD <Usuarios>.GetAll();
                return View(usuarios);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;

                return View(new List<Usuarios>());
            }
        }


        // POST: ProgramasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuarios usuarios)
        {
            CRUD<Usuarios>.Create(usuarios);
            try
            {
                return RedirectToAction(nameof(ListUsuarios));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult DetailsUsuarios(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var usuarios = CRUD<Usuarios>.GetById(id);

                if (usuarios == null)
                {
                    return NotFound();
                }

                return View(usuarios);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al obtener detalles: " + ex.Message;
                return RedirectToAction(nameof(ListUsuarios));
            }
        }
    }
}
