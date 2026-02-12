using API_Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos_AutomaG;

namespace MVC_AutomaG.Controllers
{
    public class ProgramasController : Controller
    {
        // GET: ProgramasController
        public IActionResult ListProgramas()
        {
            try
            {
                var programas = CRUD<Programas>.GetAll();

                ViewBag.CamposConocimientos = CRUD<CamposConocimiento>.GetAll();
                ViewBag.Nivel = CRUD<Niveles>.GetAll();
                ViewBag.Modalidad = CRUD<Modalidades>.GetAll();
                ViewBag.Precio = CRUD<Precios>.GetAll();

                return View(programas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View(new List<Programas>());
            }
        }

        // GET: ProgramasController/DetailsProgramas/MST-01
        public IActionResult DetailsProgramas(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var programa = CRUD<Programas>.GetById(id);
            if (programa == null) return NotFound();

            ViewBag.CamposConocimientos = CRUD<CamposConocimiento>.GetAll() ?? new List<CamposConocimiento>();
            ViewBag.Nivel = CRUD<Niveles>.GetAll() ?? new List<Niveles>();
            ViewBag.Modalidad = CRUD<Modalidades>.GetAll() ?? new List<Modalidades>();
            ViewBag.Precio = CRUD<Precios>.GetAll() ?? new List<Precios>();

            return View(programa);
        }

        // GET: ProgramasController/Create
        [Authorize(Roles = "Super Administrador")]
        public ActionResult Create()
        {
            try
            {
                ViewBag.CamposConocimientos = CRUD<CamposConocimiento>.GetAll();
                ViewBag.Nivel = CRUD<Niveles>.GetAll();
                ViewBag.Modalidad = CRUD<Modalidades>.GetAll();
                return View(new Programas());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar catálogos: " + ex.Message;
                return View(new Programas());
            }
        }
        [Authorize(Roles = "Super Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Programas programa, IFormCollection form)
        {
            // Recarga de catálogos para no perder los selects en la vista
            ViewBag.CamposConocimientos = CRUD<CamposConocimiento>.GetAll();
            ViewBag.Nivel = CRUD<Niveles>.GetAll();
            ViewBag.Modalidad = CRUD<Modalidades>.GetAll();

            try
            {
                // --- DEPUREMOS LOS DATOS DEL FORMULARIO ---
                // Si el sistema usa comas y tú mandas puntos (o viceversa), el decimal.Parse falla
                if (!decimal.TryParse(form["matriculapre"], out decimal matricula) ||
                    !decimal.TryParse(form["arancelpre"], out decimal arancel) ||
                    !decimal.TryParse(form["inscripcionpre"], out decimal inscripcion))
                {
                    ViewBag.Error = "Error de formato: Los precios deben ser números válidos (revisa el uso de puntos o comas).";
                    return View(programa);
                }

                // 1. Crear el objeto Precio (Paso 1)
                var precioNuevo = new Precios
                {
                    idpre = "TEMP", // Enviamos un temporal porque la API lo reemplaza
                    matriculapre = matricula,
                    arancelpre = arancel,
                    inscripcionpre = inscripcion,
                    monedapre = form["monedapre"],
                    vigente = true,
                    Programas = null
                };

                // Intentar crear el Precio
                var precioCreado = CRUD<Precios>.Create(precioNuevo);

                if (precioCreado == null || string.IsNullOrEmpty(precioCreado.idpre))
                {
                    ViewBag.Error = "La API rechazó la creación del PRECIO (BadRequest). Verifica que todos los campos del precio estén llegando a la DB.";
                    return View(programa);
                }

                programa.idpre = precioCreado.idpre;
                programa.idpro = "TEMP"; 
                programa.estadopro = "activo";

                // Limpieza de objetos de navegación (Crucial)
                programa.Campo = null;
                programa.Nivel = null;
                programa.Modalidad = null;
                programa.Precio = null;
                programa.ProgramasHorarios = null;

                // Intentar crear el Programa
                var progCreado = CRUD<Programas>.Create(programa);

                if (progCreado == null)
                {
                    ViewBag.Error = $"Se creó el precio {precioCreado.idpre}, pero falló el PROGRAMA. Posible causa: ID de Nivel o Modalidad no existen en la DB.";
                    return View(programa);
                }

                return RedirectToAction(nameof(ListProgramas));
            }
            catch (Exception ex)
            {
                // Aquí capturamos el error exacto que pasaste
                ViewBag.Error = "PASO DONDE FALLÓ: " + (programa.idpre == null ? "Creando Precio" : "Creando Programa") +
                                " | DETALLE: " + ex.Message;
                return View(programa);
            }
        }
        [Authorize(Roles = "Super Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProgramas(string id, IFormCollection form)
        {
            try
            {
                var programa = CRUD<Programas>.GetById(id);
                if (programa == null) return NotFound("Programa no encontrado");

                if (string.IsNullOrWhiteSpace(form["matriculapre"]) ||
                    string.IsNullOrWhiteSpace(form["arancelpre"]) ||
                    string.IsNullOrWhiteSpace(form["inscripcionpre"]) ||
                    string.IsNullOrWhiteSpace(form["monedapre"]))
                {
                    return StatusCode(500, "Debe llenar Matrícula, Arancel, Inscripción y Moneda.");
                }

                // 1) ACTUALIZAR EL MISMO PRECIO DEL PROGRAMA
                var precioActual = CRUD<Precios>.GetById(programa.idpre);
                if (precioActual == null)
                    return StatusCode(500, "No se encontró el precio del programa.");

                precioActual.matriculapre = decimal.Parse(form["matriculapre"]);
                precioActual.arancelpre = decimal.Parse(form["arancelpre"]);
                precioActual.inscripcionpre = decimal.Parse(form["inscripcionpre"]);
                precioActual.monedapre = form["monedapre"];

                // ⚠️ Importante: aquí usamos UPDATE, NO CREATE
                bool okPrecio = CRUD<Precios>.Update(precioActual.idpre, precioActual);
                if (!okPrecio)
                    return StatusCode(500, "No se pudo actualizar el precio.");

                // 2) Actualizar datos del programa
                programa.nombrepro = form["nombrepro"];
                programa.duracionpro = form["duracionpro"];
                programa.descripcionpro = form["descripcionpro"];
                programa.idcam = form["idcam"];
                programa.idniv = form["idniv"];
                programa.idmod = form["idmod"];

                if (string.IsNullOrWhiteSpace(programa.estadopro))
                    programa.estadopro = "activo";

                // 3) NO CAMBIAMOS idpre (se queda el mismo)
                bool okPrograma = CRUD<Programas>.Update(id, programa);

                if (okPrograma) return Ok("Programa actualizado correctamente");
                return StatusCode(500, "Error al guardar los cambios del programa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}
