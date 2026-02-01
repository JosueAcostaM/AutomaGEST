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
                return View(programas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                return View(new List<Programas>());
            }
        }
        // GET: ProgramasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProgramasController/Create
        public ActionResult Create()
        {
            return View();
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
    }
}
