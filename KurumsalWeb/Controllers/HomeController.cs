using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
            //Service ana sayfada da lazım olduğundan burada var
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

        public ActionResult AboutUs()
        {
            return View(db.AboutUs.SingleOrDefault());
        }

        public ActionResult Service()
        {
            return View(db.Services.ToList().OrderByDescending(x => x.ServiceId));
        }

        public ActionResult Contact()
        {
            return View(db.Contacts.SingleOrDefault());
        }

        [HttpPost]
        public ActionResult Contact(string email = null, string subject = null, string message = null, string name = null)
        {
            if (name != null && email != null && subject != null && message != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "strworktest@gmail.com";
                WebMail.Password = "Test123.";
                WebMail.SmtpPort = 587;
                WebMail.Send("strworktest@gmail.com", subject, email + '\n' + message);
                ViewBag.Danger = "Mesajınız başarı ile gönderilmiştir.";
            }
            else
            {
                ViewBag.Danger = "Hata oluştu. Tekrar deneyiniz.";
            }

            return View();
        }

        public ActionResult FooterPartial()
        {
            ViewBag.Contact = db.Contacts.SingleOrDefault();
            ViewBag.Blog = db.Blogs.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Service = db.Services.ToList().OrderByDescending(x => x.ServiceId);

            return PartialView();
        }
    }
}