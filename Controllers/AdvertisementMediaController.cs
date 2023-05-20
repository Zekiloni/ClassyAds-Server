using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassyAdsServer.Controllers
{
    public class AdvertisementMediaController : Controller
    {
        // GET: AdvertisementMediaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdvertisementMediaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdvertisementMediaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvertisementMediaController/Create
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

        // GET: AdvertisementMediaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdvertisementMediaController/Edit/5
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

        // GET: AdvertisementMediaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdvertisementMediaController/Delete/5
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
