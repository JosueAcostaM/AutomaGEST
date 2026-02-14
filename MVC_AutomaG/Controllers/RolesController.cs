using API_Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos_AutomaG;

namespace MVC_AutomaG.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles/ListRoles
        public IActionResult ListRoles()
        {
            try
            {
                var roles = CRUD<Roles>.GetAll();
                return View(roles);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View(new List<Roles>());
            }
        }

        // GET: Roles/DetailsRoles/ROL-01
        public IActionResult DetailsRoles(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            try
            {
                var rol = CRUD<Roles>.GetById(id);
                if (rol == null) return NotFound();

                return View(rol);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View(new Roles());
            }
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View(new Roles());
        }

        // POST: Roles/Create
        [Authorize(Roles = "Super Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Roles rol)
        {
            try
            {
                // Evitar enviar navegación
                rol.UsuarioRoles = null;

                // Si tu API genera idrol, enviamos TEMP
                if (string.IsNullOrWhiteSpace(rol.idrol))
                    rol.idrol = "TEMP";
                rol.codigorol= rol.nombrerol?.ToUpper().Replace(" ", "_") ?? "ROL_TEMP";

                var creado = CRUD<Roles>.Create(rol);

                if (creado == null || string.IsNullOrEmpty(creado.idrol))
                {
                    ViewBag.Error = "La API rechazó la creación del ROL. Verifica los datos.";
                    return View(rol);
                }

                return RedirectToAction(nameof(ListRoles));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View(rol);
            }
        }
    }
}
