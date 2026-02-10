using API_Consumer;
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

        /// POST: ProgramasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Programas programa, IFormCollection form)
        {
            try
            {
                ViewBag.CamposConocimientos = CRUD<CamposConocimiento>.GetAll();
                ViewBag.Nivel = CRUD<Niveles>.GetAll();
                ViewBag.Modalidad = CRUD<Modalidades>.GetAll();

                programa.estadopro = "activo";

                // Validación precios
                if (string.IsNullOrWhiteSpace(form["matriculapre"]) ||
                    string.IsNullOrWhiteSpace(form["arancelpre"]) ||
                    string.IsNullOrWhiteSpace(form["inscripcionpre"]) ||
                    string.IsNullOrWhiteSpace(form["monedapre"]))
                {
                    throw new Exception("Debe llenar Matrícula, Arancel, Inscripción y Moneda.");
                }

                // 1) Crear precio (API genera PRE#)
                var precioNuevo = new Precios
                {
                    matriculapre = decimal.Parse(form["matriculapre"]),
                    arancelpre = decimal.Parse(form["arancelpre"]),
                    inscripcionpre = decimal.Parse(form["inscripcionpre"]),
                    monedapre = form["monedapre"]
                };

                var precioCreado = CRUD<Precios>.Create(precioNuevo);

                if (precioCreado == null || string.IsNullOrWhiteSpace(precioCreado.idpre))
                    throw new Exception("No se pudo crear el precio (idpre vacío).");

                // 2) Asignar idpre al programa
                programa.idpre = precioCreado.idpre;

                // 3) Crear programa (NO enviar idpro - la API lo genera)
                // IMPORTANTE: Asegurar que el idpro sea null para que la API lo genere
                programa.idpro = null;

                var progCreado = CRUD<Programas>.Create(programa);

                if (progCreado == null || string.IsNullOrWhiteSpace(progCreado.idpro))
                    throw new Exception("La API no generó el idpro. Verifique la API.");

                return RedirectToAction(nameof(ListProgramas));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(programa);
            }
        }

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

                // 3) OJO: NO CAMBIAMOS idpre (se queda el mismo)
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
