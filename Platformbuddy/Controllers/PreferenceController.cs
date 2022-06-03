using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformbuddy.Models;

namespace Platformbuddy.Controllers
{
    public class PreferenceController : Controller
    {
        // GET: PreferenceController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PreferenceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PreferenceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreferenceController/Create
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

        // GET: PreferenceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PreferenceController/Edit/5
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

        // GET: PreferenceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PreferenceController/Delete/5
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
