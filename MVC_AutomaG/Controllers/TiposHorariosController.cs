using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC_AutomaG.Controllers
{
    public class TiposHorariosController : Controller
    {
        // GET: TiposHorariosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TiposHorariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TiposHorariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposHorariosController/Create
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

        // GET: TiposHorariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TiposHorariosController/Edit/5
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

        // GET: TiposHorariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TiposHorariosController/Delete/5
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
