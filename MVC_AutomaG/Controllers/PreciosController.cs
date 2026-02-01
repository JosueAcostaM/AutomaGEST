using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC_AutomaG.Controllers
{
    public class PreciosController : Controller
    {
        // GET: PreciosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PreciosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PreciosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreciosController/Create
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

        // GET: PreciosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PreciosController/Edit/5
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

        // GET: PreciosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PreciosController/Delete/5
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
