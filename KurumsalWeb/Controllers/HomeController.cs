using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using PagedList;

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

        public ActionResult Blog(int pageNumber = 1)
        {
            //Include("Category") diyerek aynı şekilde kategoriyide çağırmış olduk
            return View(db.Blogs.Include("Category").OrderByDescending(x => x.BlogId).ToPagedList(pageNumber, 5));
        }

        public JsonResult Comment(string firstLastName, string email, string content, int blogId)
        {
            if (content==null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            db.Comments.Add(new Comment
            {
                FirstLastName = firstLastName, 
                CommentContent = content, 
                Email = email, 
                BlogId = blogId, 
                Confirmation = false
            });
            db.SaveChanges();

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlogDetail(int id)
        {
            var blog = db.Blogs.Include("Category").Where(x => x.BlogId == id).SingleOrDefault();

            return View(blog);
        }

        public ActionResult BlogCategoryPartial()
        {
            return PartialView(db.Categories.Include("Blogs").ToList().OrderBy(x => x.CategoryName));
        }

        public ActionResult BlogSavePartial()
        {
            return PartialView(db.Blogs.ToList().OrderBy(x => x.BlogId));
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