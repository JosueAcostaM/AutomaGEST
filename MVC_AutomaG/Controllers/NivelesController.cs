using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC_AutomaG.Controllers
{
    public class NivelesController : Controller
    {
        // GET: NivelesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NivelesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NivelesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NivelesController/Create
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

        // GET: NivelesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NivelesController/Edit/5
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

        // GET: NivelesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NivelesController/Delete/5
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
