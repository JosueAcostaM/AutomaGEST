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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUsuarios(string id, IFormCollection form)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return BadRequest("ID de usuario vacío.");

                var usuario = CRUD<Usuarios>.GetById(id);
                if (usuario == null)
                    return NotFound("Usuario no encontrado.");

                usuario.nombreusu = form["nombreusu"];
                usuario.emailusu = form["emailusu"];
                usuario.activousu = form["activousu"] == "true";

                var nueva = form["nuevapassword"].ToString();

                //SOLO si escribió algo, tocamos passwordhash
                if (!string.IsNullOrWhiteSpace(nueva))
                {
                    usuario.passwordhash = nueva; // texto plano (API lo hashea)
                }
                // else: NO tocar usuario.passwordhash (se queda con el hash actual)

                bool ok = CRUD<Usuarios>.Update(id, usuario);
                if (ok) return Ok("Usuario actualizado correctamente");

                return StatusCode(500, "No se pudo actualizar el usuario.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        //suspendido o no suspendido
        [HttpPost]
        public IActionResult CambiarEstado(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return BadRequest("ID vacío.");

                var usuario = CRUD<Usuarios>.GetById(id);
                if (usuario == null)
                    return NotFound("Usuario no encontrado.");

                usuario.activousu = !usuario.activousu;

                bool ok = CRUD<Usuarios>.Update(id, usuario);
                if (ok) return Ok("OK");

                return StatusCode(500, "No se pudo cambiar el estado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }



    }
}
