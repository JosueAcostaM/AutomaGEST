using API_Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos_AutomaG;
using ClosedXML.Excel;

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
        [Authorize(Roles = "Administrador,Super Administrador")]
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
        //Exportar a Excel
        [HttpGet]
        public IActionResult ReporteExcel(string texto = "", string programa = "", string estado = "")
        {
            var aspirantes = CRUD<Aspirantes>.GetAll() ?? new List<Aspirantes>();
            string t = (texto ?? "").Trim().ToLower();
            string p = (programa ?? "").Trim().ToLower();
            string e = (estado ?? "").Trim().ToLower();

            var filtrados = aspirantes.Where(a =>
            {
                string nombre = $"{a.nombreasp} {a.apellidoasp}".ToLower();
                string ciudad = (a.ciudadasp ?? "").ToLower();
                string prog = (a.programainteres ?? "").ToLower();
                string est = (a.estadoasp ?? "").Trim().ToLower();
                string nivel = (a.nivelinteres ?? "").ToLower();

                bool cumpleTexto = string.IsNullOrEmpty(t) ||
                                   nombre.Contains(t) ||
                                   ciudad.Contains(t) ||
                                   prog.Contains(t) ||
                                   est.Contains(t);

                bool cumplePrograma = string.IsNullOrEmpty(p) || prog.Contains(p);
                bool cumpleEstado = string.IsNullOrEmpty(e) || est == e;

                return cumpleTexto && cumplePrograma && cumpleEstado;
            }).ToList();

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Aspirantes");

            ws.Cell(1, 1).Value = "Número";
            ws.Cell(1, 2).Value = "Programa/Carrera de interés";
            ws.Cell(1, 3).Value = "Nombre";
            ws.Cell(1, 4).Value = "Apellido";
            ws.Cell(1, 5).Value = "Ciudad";
            ws.Cell(1, 6).Value = "Provincia";
            ws.Cell(1, 7).Value = "Estado";

            int r = 2;
            foreach (var a in filtrados)
            {
                ws.Cell(r, 1).Value = a.Contacto?.telefonocon ?? "";
                ws.Cell(r, 2).Value = a.programainteres ?? "";
                ws.Cell(r, 3).Value = a.nombreasp ?? "";
                ws.Cell(r, 4).Value = a.apellidoasp ?? "";
                ws.Cell(r, 5).Value = a.ciudadasp ?? "";
                ws.Cell(r, 6).Value = a.provinciaasp ?? "";
                ws.Cell(r, 7).Value = a.estadoasp ?? "";
                r++;
            }

            ws.Range(1, 1, 1, 7).Style.Font.Bold = true;
            ws.Columns().AdjustToContents();

            // Nombre dinámico
            if(estado == "revisado")
            {
                estado += "s";
            }
            string estadoNombre = string.IsNullOrWhiteSpace(estado) ? "General" : estado;  // "En revisión" con tilde
            string programaNombre = string.IsNullOrWhiteSpace(programa) ? "" : "_" + programa;

            // limpiar caracteres inválidos del nombre
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                estadoNombre = estadoNombre.Replace(c.ToString(), "");
                programaNombre = programaNombre.Replace(c.ToString(), "");
            }

            var fileName = $"Aspirantes_{estadoNombre}{programaNombre}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
    }
}
