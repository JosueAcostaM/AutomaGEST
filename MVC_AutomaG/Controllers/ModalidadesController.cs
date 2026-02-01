using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC_AutomaG.Controllers
{
    public class ModalidadesController : Controller
    {
        // GET: ModalidadesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ModalidadesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ModalidadesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModalidadesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModalidadesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModalidadesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModalidadesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModalidadesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
