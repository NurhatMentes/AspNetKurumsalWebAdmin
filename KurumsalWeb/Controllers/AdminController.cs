using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;


namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();
        // GET: Admin

        [Route("yonetimpaneli")]
        public ActionResult Index()
        {
            ViewBag.CommentConf=db.Comments.Where(x=>x.Confirmation == false).Count();
            ViewBag.Comment = db.Comments.Where(x => x.Confirmation == false).ToList();

            var categoryList = db.Categories.ToList();
            return View(categoryList);
        }

        [Route("yonetimpaneli/giris")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admins.Where((x => x.Email == admin.Email || x.Password == admin.Password)).FirstOrDefault();

            if (login != null)
            {
                if (login.Password == admin.Password && login.Email == admin.Email)
                {
                    Session["adminid"] = login.AdminId;
                    Session["email"] = login.Email;
                    Session["Auth"] = login.Auth;

                    return RedirectToAction("Index", "Admin");
                }
            }

            ViewBag.Danger = "E-posta veya Şifre hatalı";
            return View(admin);
        }

        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["email"] = null;
            Session["Auth"] = null;
            Session.Abandon();

            return RedirectToAction("Login", "Admin");
        }

        public ActionResult CommentPartial()
        {
            ViewBag.CommentConf = db.Comments.Where(x => x.Confirmation == false).Count();
            return View(db.Comments.Where(x => x.Confirmation == false).ToList());
        }
    }
}