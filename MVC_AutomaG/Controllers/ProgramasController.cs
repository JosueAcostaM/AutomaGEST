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
                var camposconocimiento= CRUD<CamposConocimiento>.GetAll();
                ViewBag.CamposConocimientos = camposconocimiento;
                var nivel = CRUD<Niveles>.GetAll();
                ViewBag.Nivel = nivel;
                var modalidad = CRUD<Modalidades>.GetAll();
                ViewBag.Modalidad = modalidad;
                var precio = CRUD<Precios>.GetAll();
                ViewBag.Precio = precio;
                return View(programas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;

                return View(new List<Programas>());
            }
        }
        // GET: ProgramasController/Details/5
        // GET: ProgramasController/DetailsProgramas/MST-01
        public IActionResult DetailsProgramas(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var programa = CRUD<Programas>.GetById(id);
            if (programa == null) return NotFound();

            var campos = CRUD<CamposConocimiento>.GetAll();
            var niveles = CRUD<Niveles>.GetAll();
            var modalidades = CRUD<Modalidades>.GetAll();
            var precios = CRUD<Precios>.GetAll();
            //Edicion viewbag para multiple opcion
            ViewBag.CamposConocimientos = campos ?? new List<CamposConocimiento>();
            ViewBag.Nivel = niveles ?? new List<Niveles>();
            ViewBag.Modalidad = modalidades ?? new List<Modalidades>();
            ViewBag.Precio = precios ?? new List<Precios>();
            //viewbag para traer los datos asociados por programa
            ViewBag.CampoNombre = campos?.FirstOrDefault(c => c.idcam == programa.idcam)?.nombrecam;
            ViewBag.NivelNombre = niveles?.FirstOrDefault(n => n.idniv == programa.idniv)?.nombreniv;
            ViewBag.ModalidadNombre = modalidades?.FirstOrDefault(m => m.idmod == programa.idmod)?.nombremod;
            ViewBag.PrecioMatricula = precios?.FirstOrDefault(p => p.idpre == programa.idpre)?.matriculapre;

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
                ViewBag.Precio = CRUD<Precios>.GetAll();
                return View(new Programas());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar catálogos: " + ex.Message;
                return View(new Programas());
            }
        }

        // POST: ProgramasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Programas Programa)
        {
            CRUD<Programas>.Create(Programa);
            try
            {
                return RedirectToAction(nameof(ListProgramas));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProgramasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProgramasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(ListProgramas));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProgramasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProgramasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(ListProgramas));
            }
            catch
            {
                return View();
            }
        }
        //editar programa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProgramas(string id, IFormCollection form)
        {
            try
            {

                var programa = CRUD<Programas>.GetById(id);
                if (programa == null) return NotFound();

                programa.nombrepro = form["nombrepro"];
                programa.duracionpro = form["duracionpro"];
                programa.descripcionpro = form["descripcionpro"];

                programa.idcam = form["idcam"];
                programa.idniv = form["idniv"];
                programa.idmod = form["idmod"];
                programa.idpre = form["idpre"];

                bool ok = CRUD<Programas>.Update(id, programa);

                if (ok) return Ok("Programa actualizado correctamente");
                return StatusCode(500, "Error al guardar los cambios");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

    }
}
