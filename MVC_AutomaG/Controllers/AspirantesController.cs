using API_Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                var programas = CRUD<Programas>.GetAll(); // Traer todos los campos
                ViewBag.Programas = programas; // Pasar al ViewBag para usar en el dropdown
                return View(aspirantes);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                ViewBag.CamposConocimiento = new List<Programas>();
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

                // Obtener programas activos de la base de datos
                try
                {
                    var programas = CRUD<Programas>.GetAll();
                    ViewBag.Programas = programas?.Where(p => p.estadopro == "activo").ToList() ?? new List<Programas>();
                }
                catch
                {
                    // Si hay error, usar lista vacía
                    ViewBag.Programas = new List<Programas>();
                }

                return View(aspirante);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al obtener detalles: " + ex.Message;
                return RedirectToAction(nameof(ListAspirantes));
            }
        }
        //aspirantes por programa
        public IActionResult ListByPrograma(string idPrograma)
        {
            try
            {
                var todos = CRUD<Aspirantes>.GetAll();

                // Filtramos por programa
                var filtrados = todos.Where(a => a.programainteres == idPrograma).ToList();

                return View(filtrados);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View(new List<Aspirantes>());
            }
        }
        //Editar Informacion Aspirante
        // Agrega este método a tu controlador MVC AspirantesController
        [Authorize(Roles = "Super Administrador")]
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Update(string id, IFormCollection form)
{
    try
    {
        // Obtener el aspirante actual
        var aspirante = CRUD<Aspirantes>.GetById(id);
        
        if (aspirante == null)
        {
            return NotFound();
        }

        // Actualizar campos del aspirante
        aspirante.nombreasp = form["nombreasp"];
        aspirante.apellidoasp = form["apellidoasp"];
        aspirante.emailasp = form["emailasp"];
        aspirante.ciudadasp = form["ciudadasp"];
        aspirante.provinciaasp = form["provinciaasp"];
        aspirante.programainteres = form["programainteres"];

        // Actualizar teléfono del contacto
        if (aspirante.Contacto != null)
        {
            aspirante.Contacto.telefonocon = form["telefonocon"];
        }

        // Guardar cambios
        bool success = CRUD<Aspirantes>.Update(id, aspirante);

        if (success)
        {
            return Ok("Información actualizada correctamente");
        }
        else
        {
            return StatusCode(500, "Error al guardar los cambios");
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Error: " + ex.Message);
    }
    }
        //Cambiar Estado Aspirante
        [HttpPost]
        public IActionResult CambiarEstado(string id)
        { 
            var ok = CRUD<Aspirantes>.CambiarEstado(id);

            if (!ok)
            {
                return BadRequest("Error al Marcar revisado");
            }

            return Ok("Revisado");
        }

    }

}
