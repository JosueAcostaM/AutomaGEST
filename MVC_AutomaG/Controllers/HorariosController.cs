using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC_AutomaG.Controllers
{
    public class HorariosController : Controller
    {
        // GET: HorariosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HorariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HorariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HorariosController/Create
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

        // GET: HorariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HorariosController/Edit/5
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

        // GET: HorariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HorariosController/Delete/5
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
