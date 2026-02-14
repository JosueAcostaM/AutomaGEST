using API_Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos_AutomaG;

namespace MVC_AutomaG.Controllers
{
    public class AccountsController : Controller
    {
        public ActionResult Index() => View();

        public IActionResult ListUsuarios()
        {
            try
            {
                // La API ya trae los roles incluidos gracias al .Include() que pusimos antes
                var usuarios = CRUD<Usuarios>.GetAll();
                return View(usuarios);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View(new List<Usuarios>());
            }
        }

        // GET: Vista de Detalles y Edición
        public IActionResult DetailsUsuarios(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            try
            {
                // Traemos el usuario (la API ya debe tener el .Include para roles)
                var usuario = CRUD<Usuarios>.GetById(id);
                if (usuario == null) return NotFound();

                // PASO CLAVE: Cargar roles para el select de edición
                ViewBag.RolesDisponibles = CRUD<Roles>.GetAll();

                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al obtener detalles: " + ex.Message;
                return RedirectToAction(nameof(ListUsuarios));
            }
        }
        [Authorize(Roles = "Super Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUsuarios(string id, IFormCollection form)
        {
            try
            {
                var usuario = CRUD<Usuarios>.GetById(id);
                if (usuario == null) return NotFound();

                // 1. Actualizar datos del usuario
                usuario.nombreusu = form["nombreusu"];
                usuario.emailusu = form["emailusu"];
                usuario.activousu = form["activousu"] == "true";

                if (!string.IsNullOrWhiteSpace(form["nuevapassword"]))
                {
                    usuario.passwordhash = form["nuevapassword"];
                    Console.WriteLine("Contraseña actualizada para el usuario " + id + usuario.passwordhash);
                }
                else
                {
                    usuario.passwordhash = "vacio";
                }

                    // LIMPIEZA: Evita que la API de Usuarios falle por relaciones circulares
                    usuario.UsuarioRoles = null;

                bool ok = CRUD<Usuarios>.Update(id, usuario);

                // 2. GUARDAR EL ROL (Aquí es donde estaba fallando)
                string nuevoRolId = form["idrol"];
                if (ok && !string.IsNullOrEmpty(nuevoRolId))
                {
                    // Configuramos el EndPoint por si acaso
                    CRUD<UsuarioRoles>.EndPoint = "https://localhost:7166/api/UsuarioRoles";

                    var relacion = new UsuarioRoles
                    {
                        idusu = id,
                        idrol = nuevoRolId,
                        Usuario = null, // IMPORTANTE: Debe ser null para PostgreSQL
                        Rol = null      // IMPORTANTE: Debe ser null para PostgreSQL
                    };

                    // Intentamos crear la relación
                    CRUD<UsuarioRoles>.Create(relacion);
                }

                return Ok("Usuario actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
        [Authorize(Roles = "Super Administrador")]
        [HttpPost]
        public IActionResult CambiarEstado(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id)) return BadRequest("ID vacío.");

                var usuario = CRUD<Usuarios>.GetById(id);
                if (usuario == null) return NotFound("Usuario no encontrado.");

                usuario.activousu = !usuario.activousu;
                usuario.UsuarioRoles = null; // Limpieza

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