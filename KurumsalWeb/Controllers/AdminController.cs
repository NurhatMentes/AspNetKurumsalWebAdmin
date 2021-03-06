using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
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

            ViewBag.Service = db.Services.Count();
            ViewBag.Blog = db.Blogs.Count();
            ViewBag.CommentNumber = db.Comments.Count();

            var categoryList = db.Categories.ToList();
            return View(categoryList);
        }

        [Route("yonetimpaneli/giris")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin, string password)
        {
            var md5pass = Crypto.Hash(password, "MD5");
            var login = db.Admins.Where(x => x.Email == admin.Email || x.Password == admin.Password).FirstOrDefault();

            if (login != null)
            {
                if (login.Password == md5pass && login.Email == admin.Email)
                {
                    Session["adminid"] = login.AdminId;
                    Session["email"] = login.Email;
                    Session["Auth"] = login.Auth;
                    Session["FullName"] = login.FullName;
                    Session["Job"] = login.Job;
                    Session["Phone"] = login.Phone;

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

        public ActionResult ForgotMyPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotMyPassword(string email = null)
        {
            var user=db.Admins.Where(x=>x.Email== email).SingleOrDefault();

            if (user != null)
            {
                Random random = new Random();
                int rndPass = random.Next(10035,999654);
                int rndPass2 = random.Next(10035, 999654);

                string newPassword = rndPass.ToString() + rndPass2.ToString();

                Admin admin = new Admin();
                user.Password = Crypto.Hash(newPassword, "MD5");
                db.SaveChanges();

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "strworktest@gmail.com";
                WebMail.Password = "Test123.";
                WebMail.SmtpPort = 587;
                WebMail.Send(email, "Admin panele yeni giriş şifreniz","Şirenizi değiştirmeyi unutmayınız!" + "<br/>" + "Şifreniz: " +newPassword);
                ViewBag.Danger = "Yeni Şifreniz gönderilmiştir.";
            }
            else
            {
                ViewBag.Danger = "Kayıtlı kullanıcı bulunamadı!";
            }

            return View();
        }

        public ActionResult CommentPartial()
        {
            ViewBag.CommentConf = db.Comments.Where(x => x.Confirmation == false).Count();
            return View(db.Comments.Where(x => x.Confirmation == false).ToList());
        }
        
        public ActionResult Admins()
        {
            return View(db.Admins.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Admin admin, string password)
        {
            if (ModelState.IsValid)
            {
                admin.Password=Crypto.Hash(password,"MD5");
                db.Admins.Add(admin);
                db.SaveChanges();

                return RedirectToAction("Admins");
            }

            return View(admin);
        }

        public ActionResult Edit(int id)
        {
            var admin=db.Admins.Where(x=>x.AdminId==id).SingleOrDefault();
            return View(admin);
        }

        [HttpPost]
        public ActionResult Edit(int id, Admin admin, string password)
        {
            if (ModelState.IsValid)
            {
                var adm = db.Admins.Where(x => x.AdminId == id).SingleOrDefault();
                if (password != adm.Password)
                {
                    adm.Password = Crypto.Hash(password, "MD5");
                }
                adm.Phone = admin.Phone;
                adm.Job= admin.Job; 
                adm.FullName= admin.FullName;
                adm.Email = admin.Email;
                adm.Auth = admin.Auth;

                db.SaveChanges();
                return RedirectToAction("Admins");
            }

            return View(admin);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Admins");
        }
    }
}