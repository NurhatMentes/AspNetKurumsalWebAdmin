using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;

namespace KurumsalWeb.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Contact = db.Contacts.SingleOrDefault();
            ViewBag.Blog = db.Blogs.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Service = db.Services.ToList().OrderByDescending(x => x.ServiceId);

            return View();
        }

        public ActionResult SliderPartial()
        {
            return View(db.Sliders.ToList().OrderByDescending(x=>x.SliderId));
        }

        public ActionResult ServicePartial()
        {
            return View(db.Services.ToList().OrderByDescending(x => x.ServiceId));
        }
    }
}