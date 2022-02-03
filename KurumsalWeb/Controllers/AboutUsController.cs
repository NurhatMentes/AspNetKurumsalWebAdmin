using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class AboutUsController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext(); 
        // GET: AboutUs
        public ActionResult Index()
        {
            var aboutUs = db.AboutUs.ToList();
            return View(aboutUs);
        }

        public ActionResult Edit(int id)
        {
            var aboutUs = db.AboutUs.Where(x => x.AboutUsId == id).FirstOrDefault();
            return View(aboutUs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {
                var about = db.AboutUs.Where(x => x.AboutUsId == id).FirstOrDefault();
                about.Description = aboutUs.Description;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(aboutUs);
        }
    }
}